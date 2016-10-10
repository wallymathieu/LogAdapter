using System;
using Logary;
using Logary.Configuration;
using Logary.Targets;
using NUnit.Framework;
using Console = System.Console;
namespace Tests
{
    [TestFixture]
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
        [Test]
        public void Test()
        {
            var log = GetAdapter();
            var c = new MyClass(log.CastMethodTo<MyClass.Logger>());
        }
    }
}
