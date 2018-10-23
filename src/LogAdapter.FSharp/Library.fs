namespace LogAdapter.FSharp

module Logger=
    type LogDebug = string -> unit
    type LogError = string * exn -> unit
 
    let log (logger:LogDebug) s values = logger s
    let logf (logger:LogDebug) formatString values= Printf.kprintf (log logger) formatString values
    
    let logError (logger:LogError) s exn= logger (s, exn)
    let logErrorf (logger:LogError) formatString exn= Printf.kprintf (logError logger) formatString exn
