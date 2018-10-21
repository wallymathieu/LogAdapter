using System;
using Logary;
using Logary.Configuration;
using Logary.CSharp;
using Logary.Targets;
namespace LogAdapter.Logary
{
    public class LogAdapter
    {
        private Logger _logger;
        private enum LogAdapterLevel
        {
            Debug = 0,
            Info = 1,
            Warn =2,
            Error =3,
            Fatal=4
        }

        public LogAdapter(LogManager logManager, string name) : this(logManager.GetLogger(name))
        {
        }
        public LogAdapter(global::Logary.Logger logger)
        {
            this._logger = logger;
        }

        public void Log(int level, string message, Exception exception, object fields) =>
            _logger.LogEvent(ToLevel(level), message, exn: exception, fieldsObj: fields);

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

    }
}
