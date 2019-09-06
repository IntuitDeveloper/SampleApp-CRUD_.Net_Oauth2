using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;
using Serilog.Sinks;
using Serilog.Core;
using Serilog.Events;

namespace Intuit.Ipp.Test
{
    public static class SeriLogger
    {
        //public string applog { get; set; }
        public static Logger log { get; set; }
        static SeriLogger()
        { 
            log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Trace()
                .WriteTo.Debug()
                .WriteTo.Console()
                .CreateLogger();

            
            log.Information("Logger is initialized");
        }


        

        //public static void LogSeriLogMessage()
        //{
        //    log = new LoggerConfiguration()
        //        .WriteTo.Trace()
        //        .CreateLogger();
        //    log.Write(LogEventLevel.Verbose, applog);
        //}
    }
}