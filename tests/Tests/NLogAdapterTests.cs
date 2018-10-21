using System;
using NLog;
using Xunit;

namespace Tests
{
    public class NLogAdapterTests
    {
        LogEventInfo ToLogEvent(LogLevel info, Exception exn, string msg, object fields)
        {
            var l = new LogEventInfo(info, "lib", message: msg, exception: exn, formatProvider: null, parameters:new object[0]);
            // should destruct fields into properties here:
            l.Properties["fields"] = fields;
            return l;
        }
        [Fact]
        public void Test() 
        {
            var log = new LogAdapter.NLog.LogAdapter();
            var c = new MyClass(log.CastMethodTo<MyClass.Logger>());
        }
   }
}
