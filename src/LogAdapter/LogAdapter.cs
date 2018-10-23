using System;
# if CORE
namespace LogAdapter
#elif NLOG
namespace LogAdapter.NLog
#elif LOGARY
namespace LogAdapter.Logary
#elif LOG4NET
namespace LogAdapter.Log4Net
#else
namespace LogAdapter.Other       
#endif
{
    /***
     * Minimal log abstraction :
     */
    using LogError = Action<string, Exception>;
    using LogDebug = Action<string>;
    /*
     * The goal is to be able to 
     */
}
