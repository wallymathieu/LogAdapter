using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Log4NetAdapterTests
    {
        public LogAdapter.Log4Net.LogAdapter GetAdapter()
        {
            return new LogAdapter.Log4Net.LogAdapter("test");
        }

        [Test]
        public void Test()
        {
            var log = GetAdapter();
            var c = new MyClass(log.CastMethodTo<MyClass.Logger>());
        }
    }
}
