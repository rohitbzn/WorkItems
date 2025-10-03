using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Serilog;

namespace WorkItems.Service.Utilities
{
    public class CorrelationIdMiddleware : OwinMiddleware
    {
        private const string CorrelationIdHeader = "X-Correlation-Id";
        
        public CorrelationIdMiddleware(OwinMiddleware next) : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            var correlationId = context.Request.Headers[CorrelationIdHeader] ?? Guid.NewGuid().ToString();
            context.Set(CorrelationIdHeader, correlationId);
            context.Response.Headers.Set(CorrelationIdHeader, correlationId);

            using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
            {
                await Next.Invoke(context);
            }
        }
    }
}