using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;
using Serilog.Sinks;
using Serilog.Core;
using Serilog.Events;
using System.IO;
using System.Diagnostics;

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
                .WriteTo.RollingFile(@"C:\Documents\Serilog_log\Log-{Date}.txt")
                .CreateLogger();

            
            log.Information("Logger is initialized");
        }
        private static Stream AddListener(string path)
        {
            string filename = path + "TraceLog-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            Stream myFile = null;
            if (File.Exists(filename))
                myFile = new FileStream(filename, FileMode.Append);
            else
                myFile = new FileStream(filename, FileMode.Create);
            TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile);
            Trace.Listeners.Add(myTextListener);
            Trace.AutoFlush = true;
            return myFile;
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