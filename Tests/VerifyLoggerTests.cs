using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class VerifyLoggerTests
    {
        LogAdapter.NLog.LogAdapter log;
        public VerifyLoggerTests()
        {
            log = new LogAdapter.NLog.LogAdapter();

        }
        class ClassWithDifferentOrderOfParams
        {
            public delegate void Logger(Exception expn = null,
                           object fields = null,
                           string error = null,
                           string warn = null,
                           string debug = null,
                           string info = null,
                           string fatal = null);
        }
        class ClassWithDifferentNumberOfParams
        {
            public delegate void Logger(Exception expn = null,
                           object fields = null,
                           string error = null,
                           string warn = null,
                           string debug = null,
                           string info = null);
        }

        [Test]
        public void It_should_recognize_valid_logger() 
        {
            Assert.True( log.IsValid<MyClass.Logger>());
        }
        [Test]
        public void It_should_recognize_invalid_logger()
        {
            Assert.False(log.IsValid<ClassWithDifferentOrderOfParams.Logger>());
            Assert.False(log.IsValid<ClassWithDifferentNumberOfParams.Logger>());
        }

    }

}
