using System;
using LogAdapter;

namespace Tests
{
    using Logger = Action<int, string, Exception, object>;
    public class MyClass
    {
        private readonly Logger _logger;
        public MyClass(Logger logger)
        {
            _logger = logger;
        }
        public class SomeValue
        {
            
        }

        public SomeValue Get(int id) 
        {
            // do stuff
            _logger.Debug("fail");
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error("fail", ex);
            }
            return new SomeValue();
        }
    }
}
