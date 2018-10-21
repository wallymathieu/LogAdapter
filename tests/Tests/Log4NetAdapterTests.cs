using Xunit;

namespace Tests
{
    public class Log4NetAdapterTests
    {
        public LogAdapter.Log4Net.LogAdapter GetAdapter()
        {
            return new LogAdapter.Log4Net.LogAdapter("test");
        }

        [Fact]
        public void Test()
        {
            var log = GetAdapter();
            var c = new MyClass(log.Log);
        }
    }
}
