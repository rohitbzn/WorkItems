using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;

namespace WorkItems.Service.Utilities
{
    public class ApiKeyMiddleware : OwinMiddleware
    {
        private const string ApiKeyHeader = "X-API-Key";
        private readonly string _expectedApiKey;

        public ApiKeyMiddleware(OwinMiddleware next) : base(next)
        {
            // Load API key from config (appSettings)
            _expectedApiKey = ConfigurationManager.AppSettings["ApiKey"] ?? throw new InvalidOperationException("API key is not configured.");
            if (string.IsNullOrEmpty(_expectedApiKey))
            {
                Log.Error("API key is not configured in appSettings.");
                throw new InvalidOperationException("API key is not configured.");
            }
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var apiKeyValues) || 
                apiKeyValues == null || 
                apiKeyValues.Length == 0 || 
                apiKeyValues[0] != _expectedApiKey)
            {
                Log.Warning("Unauthorized request: missing or invalid API key.");
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            await Next.Invoke(context);
        }
    }
}