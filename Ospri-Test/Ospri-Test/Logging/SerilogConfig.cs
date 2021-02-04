using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OspriTest.Logging
{
    public class SerilogConfig
    {

        public static void SetupLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                //Add if we need to change logging after deployment.
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .CreateLogger();
        }
    }
}
