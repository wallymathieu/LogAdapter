using Logary;
using Logary.Configuration;
using Logary.Targets;
using Xunit;
using Console = System.Console;
namespace Tests
{
    public class LogaryAdapterTests
    {
        readonly Logger _logger;

        public LogaryAdapterTests()
        {
            var logMan = LogaryFactory.New("lib", c => c.Target<TextWriter.Builder>("console1",
                                                                             conf1 =>
                                                                             conf1.Target.WriteTo(Console.Out, Console.Error)
                                                                                      .MinLevel(LogLevel.Verbose))
                                  );
            _logger= logMan.Result.GetLogger("Library");
        }

        public LogAdapter.Logary.LogAdapter GetAdapter() 
        {
            return new LogAdapter.Logary.LogAdapter(_logger);
        }
        [Fact(Skip = "Field not found: 'NodaTime.SystemClock.Instance'")]
        public void Test()
        {
            var log = GetAdapter();
            var c = new MyClass(log.Log);
            c.Get(1);
        }
    }
}
