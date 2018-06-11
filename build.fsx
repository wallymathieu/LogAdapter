// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------
#r "paket: groupref netcorebuild //"
open Fake.DotNet.NuGet
#load ".fake/build.fsx/intellisense.fsx"

open System
open System.IO
open Fake.Core
open Fake.IO.Globbing.Operators
open Fake.IO.FileSystemOperators
open Fake.DotNet
open Fake.IO
open Fake.Tools

// File system information 
let solutionFile  = "./LogAdapter.sln"

// Pattern specifying assemblies to be tested using NUnit
let testAssemblies = "**/bin/*/Tests.dll"

// --------------------------------------------------------------------------------------
// END TODO: The rest of the file includes standard build steps
// --------------------------------------------------------------------------------------

// Helper active pattern for project types
let (|Fsproj|Csproj|Vbproj|) (projFileName:string) = 
    match projFileName with
    | f when f.EndsWith("fsproj") -> Fsproj
    | f when f.EndsWith("csproj") -> Csproj
    | f when f.EndsWith("vbproj") -> Vbproj
    | _                           -> failwith (sprintf "Project file %s not supported. Unknown project type." projFileName)


// --------------------------------------------------------------------------------------
// Clean build results

Target.create "clean" (fun _ ->
    Shell.cleanDirs ["bin"; "temp";] 
    solutionFile
    |> MSBuild.build (fun opts ->
        { opts with
            RestorePackagesFlag = true
            Targets = ["Clean"]
            Verbosity = Some MSBuildVerbosity.Minimal
            Properties =
              [ "Platform", "Any CPU"
                "Verbosity", "Minimal"
                "Configuration", "Release"
              ]
        })
    |> ignore
)

Target.create "build" (fun _ ->
    solutionFile
    |> MSBuild.build (fun opts ->
        { opts with
            RestorePackagesFlag = true
            Targets = ["Rebuild"]
            Verbosity = Some MSBuildVerbosity.Minimal
            Properties =
              [ "Platform", "Any CPU"
                "Verbosity", "Minimal"
                "Configuration", "Release"
              ]
        })
    |> ignore
)

open Microsoft.FSharp.Core

Target.create "test_only" (fun _ ->
    DotNet.test id "./Tests/Tests.csproj"
)

Target.create "test" Target.DoNothing

// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override

Target.create "all" Target.DoNothing

open Fake.Core.TargetOperators

"clean"
  ==> "build"
  ==> "test"
  ==> "all"

"test_only"
 ==> "test"


Target.runOrDefault "test"
