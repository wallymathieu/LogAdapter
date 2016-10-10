# LogAdapter [![Build Status](https://travis-ci.org/wallymathieu/LogAdapter.svg?branch=master)](https://travis-ci.org/wallymathieu/LogAdapter) [![Build status](https://ci.appveyor.com/api/projects/status/o6k8vkok337gt4by/branch/master?svg=true)](https://ci.appveyor.com/project/wallymathieu/logadapter/branch/master)

- LogAdapter [![NuGet](http://img.shields.io/nuget/v/LogAdapter.svg)](https://www.nuget.org/packages/LogAdapter/) 
- LogAdapter.NLog [![NuGet](http://img.shields.io/nuget/v/LogAdapter.NLog.svg)](https://www.nuget.org/packages/LogAdapter.NLog/)
- LogAdapter.Log4Net [![NuGet](http://img.shields.io/nuget/v/LogAdapter.Log4Net.svg)](https://www.nuget.org/packages/LogAdapter.Log4Net/)
- LogAdapter.Logary [![NuGet](http://img.shields.io/nuget/v/LogAdapter.Logary.svg)](https://www.nuget.org/packages/LogAdapter.Logary/)

Adapter for library logging (for current and future logging frameworks). LogAdapter is not a logging framework. It is intended to be used instead of a logger in order to let the consumer of a library choose how to log things.

## Goal

Avoid taking on a dependency on a logging library for a library. The intent is that in your application code you can take a dependency on a specific version of a logging framework, and create a LogAdapter implementation for that specific version. A library should not determine what kind of logging you choose for your application.

## Installation

Copy the following code to your library:

```
public delegate void Logger(Exception expn = null,
                                object fields = null,
                                string fatal = null,
                                string error = null,
                                string warn = null,
                                string debug = null,
                                string info = null);
```

Then when consuming the library create your adapter for this method.

## Usage

```
    public class MyClass
    {
        private readonly Logger _logger;
        public MyClass(Logger logger)
        {
            _logger = logger;
        }

        public SomeValue Get(int id) 
        {
            try
            {
                // do stuff ...
                return new SomeValue{ SomeThing=something };
            }
            catch (Exception ex)
            {
                _logger(ex, error: "fail");
                return SomeValue.Failure();
            }
        }
    }
```
