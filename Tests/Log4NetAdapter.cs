using System;
using log4net;
using log4net.Core;
using log4net.Repository;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Log4NetAdapter
    {
        ILoggerRepository _logger = LogManager.GetRepository();

        public Log4NetAdapter()
        {
        }
        public LogAdapter.LogAdapter GetAdapter()
        {
            return new LogAdapter.LogAdapter(
                info: (msg, exn, obj) => _logger.Log(ToLogEvent(Level.Info, msg,obj,exn)),
                debug: (msg, exn, obj) =>_logger.Log(ToLogEvent(Level.Debug, msg, obj, exn)),
                warn: (msg, exn, obj) =>_logger.Log(ToLogEvent(Level.Warn, msg, obj, exn)),
                error: (msg, exn, obj) =>_logger.Log(ToLogEvent(Level.Error, msg, obj, exn)),
                fatal: (msg, exn, obj) =>_logger.Log(ToLogEvent(Level.Fatal, msg, obj, exn))
            );
        }

        LoggingEvent ToLogEvent(Level level, string msg, object fields, Exception exn)
        {
            var l = new LoggingEvent(this.GetType(), _logger, "lib", level, msg, exn);
            // should destruct fields into properties here:
            l.Properties["fields"] = fields;
            return l;
        }
        [Test]
        public void Test()
        {
            var log = GetAdapter();
            var c = new MyClass(log.CastMethodTo<MyClass.Logger>());
        }
   }
}
