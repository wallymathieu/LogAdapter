namespace LogAdapter.FSharp

module Logger=
    /// Log level
    type Level=
        |Error =5
        |Warn =4
        |Info = 3
        |Debug = 2
        |Verbose = 1
    type LogMessage = Level * string * exn * obj
    type Logger = LogMessage -> unit
    
    let log (logger:Logger) s values = logger (Level.Debug, s, null, values)
    let logf (logger:Logger) formatString values= Printf.kprintf (log logger) formatString values
    
    let logError (logger:Logger) exn s values = logger (Level.Error, s, exn, values)
    let logErrorf (logger:Logger) exn formatString values= Printf.kprintf (log logger) exn formatString values
