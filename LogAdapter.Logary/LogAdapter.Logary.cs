using Logary;
using Logary.Configuration;
using Logary.Targets;
namespace LogAdapter.Logary
{
    public partial class LogAdapter
    {
        public LogAdapter(LogManager logManager, string name) : this(logManager.GetLogger(name))
        {
        }
        public LogAdapter(global::Logary.Logger logger) : this(
                info: (msg, exn, fields) => logger.LogEvent(LogLevel.Info, msg, exn: exn, fields: fields),
                debug: (msg, exn, fields) => logger.LogEvent(LogLevel.Debug, msg, exn: exn, fields: fields),
                warn: (msg, exn, fields) => logger.LogEvent(LogLevel.Warn, msg, exn: exn, fields: fields),
                error: (msg, exn, fields) => logger.LogEvent(LogLevel.Error, msg, exn: exn, fields: fields),
                fatal: (msg, exn, fields) => logger.LogEvent(LogLevel.Fatal, msg, exn: exn, fields: fields)
        )
        {
        }
    }
}
