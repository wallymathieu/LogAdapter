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
    using System.Linq;
    using System.Reflection;
    using LogMessage = Action<string, Exception, object>;
    public partial class LogAdapter
    {
        /// <summary>
        /// https://code.logos.com/blog/2008/07/casting_delegates.html
        /// </summary>
        private static class DelegateUtility
        {
            public static T Cast<T>(Delegate source) where T : class
            {
                return Cast(source, typeof(T)) as T;
            }

            public static Delegate Cast(Delegate source, Type type)
            {
                if (source == null)
                    return null;

                Delegate[] delegates = source.GetInvocationList();
                if (delegates.Length == 1)
                    return Delegate.CreateDelegate(type,
                        delegates[0].Target, delegates[0].Method);

                Delegate[] delegatesDest = new Delegate[delegates.Length];
                for (int nDelegate = 0; nDelegate < delegates.Length; nDelegate++)
                    delegatesDest[nDelegate] = Delegate.CreateDelegate(type,
                        delegates[nDelegate].Target, delegates[nDelegate].Method);
                return Delegate.Combine(delegatesDest);
            }

            public static bool SameApi(MethodInfo source, MethodInfo target)
            {
                Func<ParameterInfo, Tuple<string, Type>> map = 
                    p => Tuple.Create(p.Name, p.ParameterType);
                var sourceParams = source.GetParameters().Select(map).ToList();
                var targetParams = target.GetParameters().Select(map).ToList();
                if (sourceParams.Count != targetParams.Count)
                    return false;
                return !sourceParams.Zip(targetParams,Tuple.Create)
                            .Any(t=>!t.Item1.Equals(t.Item2));
            }
        }

        readonly LogMessage _info;
        readonly LogMessage _debug;
        readonly LogMessage _warn;
        readonly LogMessage _error;
        readonly LogMessage _fatal;

        public LogAdapter(
            LogMessage fatal = null,
            LogMessage error = null,
            LogMessage warn = null,
            LogMessage debug = null,
            LogMessage info = null
                          )
        {
            _info = info ?? Empty;
            _debug = debug ?? Empty;
            _warn = warn ?? Empty;
            _error = error ?? Empty;
            _fatal = fatal ?? Empty;
        }

        void Empty(string message, Exception exception, object fields)
        {
        }

        private delegate void Logger(Exception expn = null,
                    object fields = null,
                    string fatal = null,
                    string error = null,
                    string warn = null,
                    string debug = null,
                    string info = null);


        public T CastMethodTo<T>() where T : class
        {
            Logger log = Log;
            return DelegateUtility.Cast<T>(log);
        }
        public bool IsValid<T>()
        {
            Logger log = Log;
            return DelegateUtility.SameApi(typeof(T).GetMethod("Invoke"), log.Method);
        }

        public void Log(Exception expn = null,
                        object fields = null,
                        string fatal = null,
                        string error = null,
                        string warn = null,
                        string debug = null,
                        string info = null
                       )
        {
            if (error != null)
            {
                _error(error, expn, fields);
                return;
            }
            if (info != null)
            {
                _info(info, expn, fields);
                return;
            }
            if (debug != null)
            {
                _debug(debug, expn, fields);
                return;
            }
            if (warn != null)
            {
                _warn(warn, expn, fields);
                return;
            }
            if (fatal != null)
            {
                _fatal(fatal, expn, fields);
                return;
            }
            _error("Error!", expn, fields);
        }
    }
}
