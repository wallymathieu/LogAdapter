using System;
using LogAdapter;

namespace Tests
{
    public class MyClass
    {
        public delegate void Logger(Exception expn = null,
                            object fields = null,
                            string fatal = null,
                            string error = null,
                            string warn = null,
                            string debug = null,
                            string info = null);
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
            _logger(error: "fail");
            try
            {

            }
            catch (Exception ex)
            {
                _logger(ex, error: "fail");
            }
            return new SomeValue();
        }
    }
}
