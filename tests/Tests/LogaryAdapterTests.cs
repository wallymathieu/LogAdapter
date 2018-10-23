using Logary;
using Logary.Configuration;
using Logary.Targets;
using Logary.CSharp;
using NodaTime;
using SomeClassLibrary;
using Xunit;
using Console = System.Console;
namespace Tests
{
    public class LogaryAdapterTests
    {
        readonly Logger _logger;

        public LogaryAdapterTests()
        {
            var clock =  NodaTime.SystemClock.Instance;
            var logMan = LogaryFactory.New("lib", 
                c => 
                    c.Target<TextWriter.Builder>("console1",
                       conf1 => conf1.Target.WriteTo(Console.Out, Console.Error).MinLevel(LogLevel.Verbose)
                    )
            );
            _logger= logMan.Result.GetLogger("Library");
        }


        [Fact(Skip = "For some reason the not null marked instance SystemClock.Instance is null ...")]
        public void Test()
        {
            var c = new MyClass(
                logDebug:msg=>_logger.LogEvent(LogLevel.Debug, msg),
                logError:(msg,exn)=>_logger.LogEvent(LogLevel.Error, msg, exn:exn)
                );
            c.Get(1);
        }
    }
}
