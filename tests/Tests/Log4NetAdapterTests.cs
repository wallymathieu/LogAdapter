using Xunit;
using log4net;
using log4net.Core;
using SomeClassLibrary;

namespace Tests
{
    public class Log4NetAdapterTests
    {
        public ILogger GetLogger() => LogManager.CreateRepository("test").GetLogger("logger");

        [Fact]
        public void Test()
        {
            var log = GetLogger();
            var c = new MyClass(
                        logDebug:msg=>log.Log(typeof(Log4NetAdapterTests), Level.Debug, msg, null),
                        logError:(msg,exn)=>log.Log(typeof(Log4NetAdapterTests), Level.Error, msg, exn)
                );
            c.Get(1);
        }
    }
}
