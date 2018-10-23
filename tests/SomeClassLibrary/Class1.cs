using System;

namespace SomeClassLibrary
{
   
    using LogError = Action<string, Exception>;
    using LogDebug = Action<string>;
    public class MyClass
    {
        private readonly LogError _logError;
        private readonly LogDebug _logDebug;
        public MyClass(LogError logError, LogDebug logDebug)
        {
            _logError = logError;
            _logDebug = logDebug;
        }
        public class SomeValue
        {
            
        }

        public SomeValue Get(int id) 
        {
            // do stuff
            _logDebug("fail");
            try
            {
                throw new Exception("Some logic failed");
            }
            catch (Exception ex)
            {
                _logError("fail", ex);
            }
            return new SomeValue();
        }
    }
}