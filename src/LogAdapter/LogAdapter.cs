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
    using Logger = Action<int, string, Exception, object>;
    public enum LogAdapterLevel
    {
        Debug = 0,
        Info = 1,
        Warn =2,
        Error =3,
        Fatal=4
    }
    public static class LoggerExtensions
    {
      
        public static void Error(this Logger logger,string message, Exception exception=null, object fields=null) => 
            logger((int)LogAdapterLevel.Error, message, exception, fields);
        public static void Debug(this Logger logger,string message, Exception exception=null, object fields=null) => 
            logger((int)LogAdapterLevel.Debug, message, exception, fields);
        public static void Info(this Logger logger,Exception exception, string message, object fields=null) => 
            logger((int)LogAdapterLevel.Info, message, exception, fields);
        public static void Warn(this Logger logger,Exception exception, string message, object fields=null) => 
            logger((int)LogAdapterLevel.Warn, message, exception, fields);
        public static void Fatal(this Logger logger,Exception exception, string message, object fields=null) => 
            logger((int)LogAdapterLevel.Fatal, message, exception, fields);        
    }

  
    /* Depends to much on c# specifics :
     
    public delegate void LoggerDelegate(Exception exception = null,
        object fields = null,
        string fatal = null,
        string error = null,
        string warn = null,
        string debug = null,
        string info = null);
    
    public static class LoggerDelegateExtensions
    {
        public static void Error(this LoggerDelegate logger,string message, Exception exception=null, object fields=null) => 
            logger(error: message, exception: exception, fields: fields);
        public static void Debug(this LoggerDelegate logger,string message, Exception exception=null, object fields=null) => 
            logger(debug: message, exception: exception, fields: fields);
        public static void Info(this LoggerDelegate logger,Exception exception, string message, object fields=null) => 
            logger(info: message, exception: exception, fields: fields);
        public static void Warn(this LoggerDelegate logger,Exception exception, string message, object fields=null) => 
            logger(warn: message, exception: exception, fields: fields);
        public static void Fatal(this LoggerDelegate logger,Exception exception, string message, object fields=null) => 
            logger(fatal: message, exception: exception, fields: fields);        
    }
    */
}
