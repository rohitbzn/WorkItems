using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using WorkItems.Service.Data;
using WorkItems.Shared;

namespace WorkItems.Service.Utilities
{
    public class WorkItemBackgroundWorker : IDisposable
    {
        private readonly Timer _timer;
        private readonly TimeSpan _interval;
        private readonly TimeSpan _staleThreshold;
        private bool _isRunning;
        private bool _disposed;

        public WorkItemBackgroundWorker(TimeSpan interval, TimeSpan staleThreshold)
        {
            _interval = interval;
            _staleThreshold = staleThreshold;
            _timer = new Timer(async _ => await DoWorkAsync(), null, _interval, _interval);
        }

        private async Task DoWorkAsync()
        {
            if (_isRunning) return;
            _isRunning = true;
            try
            {
                using (var db = new WorkItemsDbContext())
                {
                    var cutoff = DateTime.UtcNow - _staleThreshold;
                    var items = await db.WorkItems
                        .Where(w => w.Status == WorkItemStatus.New && w.UpdatedAt < cutoff)
                        .ToListAsync();

                    foreach (var item in items)
                    {
                        item.Status = WorkItemStatus.Stale;
                        item.UpdatedAt = DateTime.UtcNow;
                    }

                    if (items.Count > 0)
                    {
                        await db.SaveChangesAsync();
                        Log.Information("BackgroundWorker: Marked {Count} work items as Stale.", items.Count);
                    }
                    else
                    {
                        Log.Information("BackgroundWorker: No work items to mark as Stale.");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "BackgroundWorker: Error during background processing.");
            }
            finally
            {
                _isRunning = false;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _timer.Dispose();
            _disposed = true;
        }
    }
}