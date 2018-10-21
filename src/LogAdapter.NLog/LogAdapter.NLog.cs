using System;
using NLog;
using System.Linq;
using System.Reflection;

namespace LogAdapter.NLog
{
    public class LogAdapter
    {
        private readonly ILogger _logger;
        private readonly string _loggerName;

        public LogAdapter(string loggerName, string name) : this(LogManager.GetLogger(name), loggerName)
        {
        }
        public LogAdapter(ILogger logger, string loggerName)
        {
            _logger = logger;
            _loggerName = loggerName;
        }

        public void Log(int level, string message, Exception exception, object fields) =>
            _logger.Log(ToLogEvent(ToLevel(level),  exception, message, fields, _loggerName));

        private static LogLevel ToLevel(int level)
        {
            switch ((LogAdapterLevel) level)
            {
                case LogAdapterLevel.Debug: return LogLevel.Debug;
                case LogAdapterLevel.Info: return LogLevel.Info;
                case LogAdapterLevel.Warn: return LogLevel.Warn;
                case LogAdapterLevel.Error: return LogLevel.Error;
                case LogAdapterLevel.Fatal: return LogLevel.Fatal;
                default:return LogLevel.Fatal;
            }
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
