using System.Configuration;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

namespace WorkItems.Service
{
    public class WorkItemsService : ServiceBase
    {
        private IDisposable _webApp;
        private readonly string _baseAddress = ConfigurationManager.AppSettings["BaseAddress"] 
            ?? "http://localhost:9000/";

        protected override void OnStart(string[] args)
        {
            _webApp = WebApp.Start<Startup>(_baseAddress);
        }

        protected override void OnStop()
        {
            _webApp?.Dispose();
        }
    }
}