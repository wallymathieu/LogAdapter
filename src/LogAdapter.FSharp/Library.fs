namespace LogAdapter.FSharp

module Logger=
    /// Log level
    type Level=
        |Debug = 0
        |Info = 1
        |Warn = 2
        |Error = 3
    type LogMessage = int * string * exn * obj
    type Logger = LogMessage -> unit
    
    let log (logger:Logger) s values = logger (int Level.Debug, s, null, values)
    let logf (logger:Logger) formatString values= Printf.kprintf (log logger) formatString values
    
    let logError (logger:Logger) exn s values = logger (int Level.Error, s, exn, values)
    let logErrorf (logger:Logger) exn formatString values= Printf.kprintf (log logger) exn formatString values
