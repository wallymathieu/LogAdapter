using System;
namespace LogAdapter
{
    public delegate void Logger(Exception expn = null,
                                object fields = null,
                                string fatal = null,
                                string error = null,
                                string warn = null,
                                string debug = null,
                                string info = null);
}
