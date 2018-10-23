using System;
using NLog;
using SomeClassLibrary;
using Xunit;

namespace Tests
{
    public class NLogAdapterTests
    {
        [Fact]
        public void Test() 
        {
            var log = LogManager.GetLogger("test");
            var c = new MyClass(
                logDebug:msg=>log.Debug(msg),
                logError:(msg,exn)=>log.Error(exn, msg)
            );
            c.Get(1);
        }
   }
}
