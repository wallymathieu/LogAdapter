using System;
using Logary;
using Logary.Configuration;
using Logary.Targets;
using NUnit.Framework;
using Console = System.Console;
namespace Tests
{
    [TestFixture]
    public class LogaryAdapter
    {
        readonly Logger _logger;

        public LogaryAdapter()
        {
            var logMan = LogaryFactory.New("lib", c => c.Target<TextWriter.Builder>("console1",
                                                                             conf1 =>
                                                                             conf1.Target.WriteTo(Console.Out, Console.Error)
                                                                                      .MinLevel(LogLevel.Verbose))
                                  );
            _logger= logMan.Result.GetLogger("Library");
        }

        public LogAdapter.LogAdapter GetAdapter() 
        {
            return new LogAdapter.LogAdapter(
                info: (msg, exn, fields) => _logger.LogEvent(LogLevel.Info, msg, exn: exn, fields:fields),
                debug: (msg, exn, fields) => _logger.LogEvent(LogLevel.Debug, msg, exn: exn, fields: fields),
                warn: (msg, exn, fields) => _logger.LogEvent(LogLevel.Warn, msg, exn: exn, fields: fields),
                error: (msg, exn, fields) => _logger.LogEvent(LogLevel.Error, msg, exn: exn, fields: fields),
                fatal: (msg, exn, fields) => _logger.LogEvent(LogLevel.Fatal, msg, exn: exn, fields: fields)
            );
        }
        [Test]
        public void Test()
        {
            var log = GetAdapter();
            var c = new MyClass(log.CastMethodTo<MyClass.Logger>());
        }
    }
}
