using System;
using NLog;
using System.Linq;
using System.Reflection;

namespace LogAdapter.NLog
{
    public partial class LogAdapter
    {
        public LogAdapter(string loggerName, string name) : this(LogManager.GetLogger(name), loggerName)
        {
        }
        public LogAdapter(ILogger logger, string loggerName) :this(
            info: (msg, exn, fields) => logger.Log(ToLogEvent(LogLevel.Info, exn, msg, fields, loggerName)),
            debug: (msg, exn, fields) => logger.Log(ToLogEvent(LogLevel.Debug, exn, msg, fields, loggerName)),
            warn: (msg, exn, fields) => logger.Log(ToLogEvent(LogLevel.Warn, exn, msg, fields, loggerName)),
            error: (msg, exn, fields) => logger.Log(ToLogEvent(LogLevel.Error, exn, msg, fields, loggerName)),
            fatal: (msg, exn, fields) => logger.Log(ToLogEvent(LogLevel.Fatal, exn, msg, fields, loggerName)))
        {
        }

        private static LogEventInfo ToLogEvent(LogLevel info, Exception exn, string msg, object fields, string loggerName)
        {
            var l = new LogEventInfo(info, loggerName, message: msg, exception: exn, formatProvider: null, parameters: new object[0]);
            if (fields != null)
            {
                foreach (var item in fields.GetType().GetProperties( BindingFlags.Instance| BindingFlags.GetProperty|BindingFlags.Public)
                    .Where(p => p.CanRead && !p.GetIndexParameters().Any()))
                {
                    l.Properties[item.Name] = item.GetValue(fields,null);
                }
                foreach (var item in fields.GetType().GetFields(BindingFlags.Instance | BindingFlags.GetField | BindingFlags.Public))
                {
                    l.Properties[item.Name] = item.GetValue(item);
                }
            }
            return l;
        }
    }
}
