// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r @"packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Git
open Fake.AssemblyInfoFile
open Fake.ReleaseNotesHelper
open Fake.UserInputHelper
open System
open System.IO

// File system information 
let solutionFile  = "./LogAdapter.sln"

// Pattern specifying assemblies to be tested using NUnit
let testAssemblies = "**/bin/Debug/Tests.dll"

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

Target "clean" (fun _ ->
    CleanDirs ["bin"; "temp";] 
    !! solutionFile
    |> MSBuildReleaseExt "" [("Platform", "Any CPU")] "Clean"
    |> ignore
)

Target "build" (fun _ ->
    !! solutionFile
    |> MSBuildReleaseExt "" [("Platform", "Any CPU")] "Rebuild"
    |> ignore
)

Target "test_only" (fun _ ->
    !! testAssemblies
    |> NUnit (fun p ->
        { p with
            DisableShadowCopy = true
            TimeOut = TimeSpan.FromMinutes 20.
            OutputFile = "TestResults.xml" })
)



Target "pack" (fun _ ->
    Paket.Pack(fun p -> 
        { p with
            OutputPath = "bin"})
)

Target "push" (fun _ ->
    Paket.Push(fun p -> 
        { p with
            WorkingDir = "bin" })
)


Target "test" (fun _ -> ())
// --------------------------------------------------------------------------------------
// Run all targets by default. Invoke 'build <Target>' to override

Target "all" DoNothing
  
"clean"
  ==> "build"
  ==> "test"
  ==> "all"

"test_only"
 ==> "test"


RunTargetOrDefault "test"
