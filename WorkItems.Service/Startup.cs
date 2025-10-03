using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Serilog;
using WorkItems.Service.Data;
using WorkItems.Service.Utilities;

[assembly: OwinStartup(typeof(WorkItems.Service.Startup))]

namespace WorkItems.Service
{
    public class Startup
    {
        private static WorkItemBackgroundWorker? _backgroundWorker;

        public void Configuration(IAppBuilder app)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(
                    "logs\\service-.log",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 10_000_000,
                    rollOnFileSizeLimit: true)
                .CreateLogger();

            // Add custom middleware for API key and correlation ID
            app.Use<CorrelationIdMiddleware>();
            app.Use<ApiKeyMiddleware>();

            // Configure Web API
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Use JSON formatting
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            app.UseWebApi(config);

            // Start background worker (interval: 60s, stale threshold: 5min)
            _backgroundWorker = new WorkItemBackgroundWorker(
                TimeSpan.FromSeconds(60),
                TimeSpan.FromMinutes(5)
            );

            // Seed the database with demo data (safe and idempotent)
            using (var db = new WorkItemsDbContext())
            {
                db.Database.CreateIfNotExists(); // Ensures DB is created
                db.EnsureSeedData();             // Seeds demo data if needed
            }
        }

        // Add a method to dispose the worker on shutdown
        public static void Shutdown()
        {
            _backgroundWorker?.Dispose();
        }
    }
}