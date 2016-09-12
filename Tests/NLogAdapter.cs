using System;
using NLog;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class NLogAdapter
    {
        ILogger _logger = LogManager.GetLogger("lib");
        public NLogAdapter()
        {
            
        }
        public LogAdapter.Logger GetAdapter()
        {
            return new LogAdapter.LogAdapter(
                info: (msg, exn, fields) => _logger.Log(ToLogEvent(LogLevel.Info, exn, msg ,fields)),
                debug: (msg, exn, fields) => _logger.Log(ToLogEvent(LogLevel.Debug,exn, msg,fields)),
                warn: (msg, exn, fields) => _logger.Log(ToLogEvent(LogLevel.Warn, exn, msg,fields)),
                error: (msg, exn, fields) => _logger.Log(ToLogEvent(LogLevel.Error, exn, msg,fields)),
                fatal: (msg, exn, fields) => _logger.Log(ToLogEvent(LogLevel.Fatal, exn, msg,fields))
            ).Log;
        }

        LogEventInfo ToLogEvent(LogLevel info, Exception exn, string msg, object fields)
        {
            var l = new LogEventInfo(info, "lib", message: msg, exception: exn, formatProvider: null, parameters:new object[0]);
            // should destruct fields into properties here:
            l.Properties["fields"] = fields;
            return l;
        }
        [Test]
        public void Test() 
        {
            var log = GetAdapter();
            var c = new MyClass((expn, fields, fatal, error, warn, debug, info) => log(expn,fields, fatal, error,warn,debug,info));
        }
   }
}
