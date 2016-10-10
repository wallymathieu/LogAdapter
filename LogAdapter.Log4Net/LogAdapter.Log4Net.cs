using System;
using log4net;
using log4net.Core;
using log4net.Repository;
namespace LogAdapter.Log4Net
{
    public partial class LogAdapter
    {
        public LogAdapter(string loggerName) : this(LogManager.GetRepository(), loggerName)
        {
        }
        public LogAdapter(ILoggerRepository logger, string loggerName) : this(
                info: (msg, exn, obj) => logger.Log(ToLogEvent(Level.Info, loggerName, msg, obj, exn, logger)),
                debug: (msg, exn, obj) => logger.Log(ToLogEvent(Level.Debug, loggerName, msg, obj, exn, logger)),
                warn: (msg, exn, obj) => logger.Log(ToLogEvent(Level.Warn, loggerName, msg, obj, exn, logger)),
                error: (msg, exn, obj) => logger.Log(ToLogEvent(Level.Error, loggerName, msg, obj, exn, logger)),
            fatal: (msg, exn, obj) => logger.Log(ToLogEvent(Level.Fatal, loggerName, msg, obj, exn, logger)))
        {
        }
        private static LoggingEvent ToLogEvent(Level level, string loggerName,string msg, object fields, Exception exn,ILoggerRepository logger)
        {
            var l = new LoggingEvent(typeof(LogAdapter), logger, loggerName, level, msg, exn);
            // should destruct fields into properties here:
            l.Properties["fields"] = fields;
            return l;
        }

    }
}
