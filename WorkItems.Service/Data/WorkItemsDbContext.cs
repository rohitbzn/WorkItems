using System;
using System.Data.Entity;
using System.Linq;
using WorkItems.Shared;

namespace WorkItems.Service.Data
{
    public partial class WorkItemsDbContext : DbContext
    {
        public WorkItemsDbContext() : base("name=WorkItemsDb")
        {
        }

        public virtual DbSet<WorkItem> WorkItems { get; set; }

        public void EnsureSeedData()
        {
            if (!WorkItems.Any())
            {
                WorkItems.Add(new WorkItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Demo: Set up project",
                    Status = WorkItemStatus.New,
                    Priority = WorkItemPriority.High,
                    UpdatedAt = DateTime.UtcNow
                });
                WorkItems.Add(new WorkItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Demo: Write documentation",
                    Status = WorkItemStatus.InProgress,
                    Priority = WorkItemPriority.Medium,
                    UpdatedAt = DateTime.UtcNow
                });
                SaveChanges();
            }
        }
    }
}