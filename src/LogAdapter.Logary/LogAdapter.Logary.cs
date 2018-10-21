using Logary;
using Logary.Configuration;
using Logary.CSharp;
using Logary.Targets;
namespace LogAdapter.Logary
{
    public partial class LogAdapter
    {
        public LogAdapter(LogManager logManager, string name) : this(logManager.GetLogger(name))
        {
        }
        public LogAdapter(global::Logary.Logger logger) : this(
                info: (msg, exn, fields) => logger.LogEvent(LogLevel.Info, msg, exn: exn, fieldsObj: fields),
                debug: (msg, exn, fields) => logger.LogEvent(LogLevel.Debug, msg, exn: exn, fieldsObj: fields),
                warn: (msg, exn, fields) => logger.LogEvent(LogLevel.Warn, msg, exn: exn, fieldsObj: fields),
                error: (msg, exn, fields) => logger.LogEvent(LogLevel.Error, msg, exn: exn, fieldsObj: fields),
                fatal: (msg, exn, fields) => logger.LogEvent(LogLevel.Fatal, msg, exn: exn, fieldsObj: fields)
        )
        {
        }
    }
}
