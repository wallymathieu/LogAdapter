namespace LogAdapter.FSharp

module Logger=
    /// Log level
    module Level=
        let error =5
        let warn =4
        let info = 3
        let debug = 2
        let verbose = 1
    type LogMessage = int * string * exn * obj
    type Logger = LogMessage -> unit
    
    let log (logger:Logger) s values = logger (Level.debug, s, null, values)
    let logf (logger:Logger) formatString values= Printf.kprintf (log logger) formatString values
    
    let logError (logger:Logger) exn s values = logger (Level.error, s, exn, values)
    let logErrorf (logger:Logger) exn formatString values= Printf.kprintf (log logger) exn formatString values
