// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r "paket: groupref FakeBuild //"

#load "./.fake/build.fsx/intellisense.fsx"

open System.IO
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.DotNet.Testing
open Fake.Tools
open Fake.Api
open Fake.DotNet

// File system information 
let solutionFile  = "./LogAdapter.sln"

// Default target configuration
let configuration = "Release"

let testProjects = ["./tests/Tests"] 


// --------------------------------------------------------------------------------------
// END TODO: The rest of the file includes standard build steps
// --------------------------------------------------------------------------------------


// --------------------------------------------------------------------------------------
// Clean build results

Target.create "clean" (fun _ ->
    Shell.cleanDirs ["bin"; "temp"]
)


Target.create "Restore" (fun _ ->
    solutionFile
    |> DotNet.restore id
)

Target.create "Build" (fun _ ->
    let buildMode = Environment.environVarOrDefault "buildMode" configuration
    let setParams (defaults:MSBuildParams) =
        { defaults with
            Verbosity = Some(Quiet)
            Targets = ["Build"]
            Properties =
                [
                    "Optimize", "True"
                    "DebugSymbols", "True"
                    "Configuration", buildMode
                ]
         }
    MSBuild.build setParams solutionFile
)

Target.create "test_only" (fun _ ->
    testProjects
    |> Seq.iter (fun proj -> DotNet.test (fun p ->
        { p with ResultsDirectory = Some __SOURCE_DIRECTORY__ }) proj)
)


Target.create "pack" (fun _ ->
    Paket.pack(fun p -> 
        { p with
            OutputPath = "bin"})
)

Target.create "push" (fun _ ->
    Paket.push(fun p -> 
        { p with
            WorkingDir = "bin" })
)


Target.create "test" ignore
// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override

Target.create "all" ignore
  
"clean"
  ==> "build"
  ==> "test"
  ==> "all"

"test_only"
 ==> "test"

Target.runOrDefault "test"
