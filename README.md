# LogAdapter [![Build Status](https://travis-ci.org/wallymathieu/LogAdapter.svg?branch=master)](https://travis-ci.org/wallymathieu/LogAdapter) [![Build status](https://ci.appveyor.com/api/projects/status/o6k8vkok337gt4by/branch/master?svg=true)](https://ci.appveyor.com/project/wallymathieu/logadapter/branch/master)

Adapter for library logging (for current and future logging frameworks). LogAdapter is not a logging framework. It is intended to be used instead of a logger in order to let the consumer of a library choose how to log things.

## Note

This is not a library.

## Goal

Avoid taking on a dependency on a logging library for a library. The intent is that in your application code you can take a dependency on a specific version of a logging framework, and create a LogAdapter implementation for that specific version. A library should not determine what kind of logging you choose for your application.

## Installation C#

Copy the following code to your library:

```c#
    using LogError = Action<string, Exception>;
    using LogDebug = Action<string>;
```

Then when consuming the library create your adapter for this method.

## Usage

```c#
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

    public SomeValue Get(int id) 
    {
        _logDebug("get");
        try
        {
            // do stuff ...
            return new SomeValue{ SomeThing=something };
        }
        catch (Exception ex)
        {
            _logError("fail", ex);
            return SomeValue.Failure();
        }
    }
}
```

## Alternatives

LogAdapter is intended to be a minimal logging abstraction. I.e. the goal is not to be a more complete abstraction like for instance:

 - [Logary.Facade](https://github.com/logary/logary#using-logary-in-a-library)
 - [Microsoft.Extensions.Logging](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.logging?view=aspnetcore-2.0) 
 - [LibLog](https://github.com/damianh/LibLog) 

This has some pros and cons. If you use this non-library (we assume that you don't use any of the nuget packages provided), then it's easier to plug in any logging abstraction. The downside is that you don't get more than minimal features in your logging. If you look at the type signature used by LogAdapter, it assumes that you use c#. Logary Facade provides and LibLog provides more complete abstractions at the expense of making it more difficult to plug in random logging framework. Microsoft.Extensions.Logging ties your code to specific logging abstractions and requires you to keep up to date with the major versions of that library.

## Why shouldn't you use this approach?

In f# you might have [less need](http://blog.ploeh.dk/2017/02/02/dependency-rejection/) to use an object oriented dependency injection.
