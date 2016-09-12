# LogAdapter
Adapter for library logging (for current and future logging frameworks)

## Goal

Avoid taking on a dependency on a logging library for a library

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
