using System;
using log4net;
using log4net.Core;
using log4net.Repository;

namespace LogAdapter.Log4Net
{
    public class LogAdapter
    {
        private ILoggerRepository _logger;
        private readonly string _loggerName;

        public LogAdapter(string loggerName) : this(LogManager.GetRepository(loggerName), loggerName)
        {
        }

        public LogAdapter(ILoggerRepository logger, string loggerName)
        {
            this._logger = logger;
            _loggerName = loggerName;
        }

        public void Log(int level, string message, Exception exception, object fields) =>
            _logger.Log(ToLogEvent(ToLevel(level), _loggerName, message, fields, exception, _logger));

        private static Level ToLevel(int level)
        {
            switch ((LogAdapterLevel) level)
            {
                case LogAdapterLevel.Debug: return Level.Debug;
                case LogAdapterLevel.Info: return Level.Info;
                case LogAdapterLevel.Warn: return Level.Warn;
                case LogAdapterLevel.Error: return Level.Error;
                case LogAdapterLevel.Fatal: return Level.Fatal;
                default:return Level.Fatal;
            }
        }

        private static LoggingEvent ToLogEvent(Level level, string loggerName, string msg, object fields, Exception exn,
            ILoggerRepository logger)
        {
            var l = new LoggingEvent(typeof(LogAdapter), logger, loggerName, level, msg, exn);
            // should destruct fields into properties here:
            l.Properties["fields"] = fields;
            return l;
        }
    }
}