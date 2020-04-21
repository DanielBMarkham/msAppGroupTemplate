namespace ValidateAccount.Test
  module Util=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open ValidateAccount
    open ValidateAccount.Types
    open ValidateAccount.Lenses
    open ValidateAccount.Persist
    open ValidateAccount.Core
    open ValidateAccount.Core.Util
    open ValidateAccount.Core.Compiler
    open Expecto
    open Logary // needed at bottom to give right "Level" lookup for logging
    // Tag-list for the logger is namespace, project name, file name
    let moduleLogger = logary.getLogger (PointName [| "EA"; "Test";  "EATest"; "Util" |])
    // For folks on anal mode, log the module being entered.  NounVerb Proper Case
    logEvent Verbose "Module enter...." moduleLogger

    /// Perform the translation across the client/dll boundary
    /// Yep, there are other ways to do this. I prefer to think of
    /// the boundary as another outside of the onion. Trust no one. :)
    //let EALibCompile (localCompilationUnits:EA.Types.EACompilationUnitType[]):EA.Types.EACompilationResultType =
    //  let libraryCompilationUnits:EA.Core.Compiler.CompilationUnitType[] =
    //    localCompilationUnits |> Array.map(fun x->{Info=x.Info; FileContents=x.FileContents})
    //  let libraryRet:EA.Core.Compiler.CompilationResultType=Compile(libraryCompilationUnits)
    //  let translatedRet:EACompilationResultType={MasterModelText=libraryRet.MasterModelText}
    //  translatedRet

    logEvent Verbose "Module Creating Fake Files For Testing Purposes" moduleLogger
    let FakeFile1:System.IO.FileInfo=SystemUtilities.getFakeFileInfo()
    let FakeFile2:System.IO.FileInfo=getFakeFileInfo()
    let FakeFile3:System.IO.FileInfo=getFakeFileInfo()
    let FakeFile4:System.IO.FileInfo=getFakeFileInfo()
    let FakeFile5:System.IO.FileInfo=getFakeFileInfo()
    let FakeFile6:System.IO.FileInfo=getFakeFileInfo()
    let FakeFile7:System.IO.FileInfo=getFakeFileInfo()


    // Need a better tagging/naming/organizing strategy
    [<Tests>]
    let tests =
      testList "file" [
        testCase "Empty files return empty" <| fun _ ->
          let newFakeInfo=getFakeFileInfo()
          let newFakeFileContents=[||]
          let newParm:InputUnitType[]=[|{Info=newFakeInfo; FileContents=newFakeFileContents}|]
          let result=[] // Processing goes here
          Expect.isTrue (result.Length=0) "Empty input producing an output"
      ]

    let newMain (argv:string[]) (compilerCancelationToken:System.Threading.CancellationTokenSource) (manualResetEvent:System.Threading.ManualResetEventSlim) (ret:int byref) =
        try
            logEvent Verbose "Method newMain beginning....." moduleLogger
            // Error is the new Out. Write here so user can pipe places
            //Console.Error.WriteLine "I am here. yes."
            // Returns a non-zero if any tests failed
            ret<-Expecto.Tests.runTestsInAssembly Expecto.Impl.ExpectoConfig.defaultConfig argv

            // I'm done (since I'm a single-threaded function, I know this)
            // take a few seconds to catch up, then you may run until you quit
            logEvent Verbose "Method newMain Deleting Fake Files." moduleLogger
            FakeFile1.Delete()
            FakeFile2.Delete()
            FakeFile3.Delete()
            FakeFile4.Delete()
            FakeFile5.Delete()
            FakeFile6.Delete()
            FakeFile7.Delete()
            logEvent Verbose "..... Method newMain ending. Normal Path." moduleLogger
            compilerCancelationToken.Token.WaitHandle.WaitOne(3000) |> ignore
            manualResetEvent.Set()
            ()
        with
            | :? UserNeedsHelp as hex ->
                defaultappRBaseOptions.PrintThis
                logEvent Logary.Debug "..... Method newMain ending. User requested help." moduleLogger
                manualResetEvent.Set()
                ()
            | ex ->
                logEvent Error "..... Method newMain ending. Exception." moduleLogger
                logEvent Error ("Program terminated abnormally " + ex.Message) moduleLogger
                logEvent Error (ex.StackTrace) moduleLogger
                if ex.InnerException = null
                    then
                        Logary.Message.eventFormat (Logary.Debug, "newMain Exit System Exception")|> Logger.logSimple moduleLogger
                        ret<-1
                        manualResetEvent.Set()
                        ()
                    else
                        System.Console.WriteLine("---   Inner Exception   ---")
                        System.Console.WriteLine (ex.InnerException.Message)
                        System.Console.WriteLine (ex.InnerException.StackTrace)
                        Logary.Message.eventFormat (Logary.Debug, "newMain Exit System Exception")|> Logger.logSimple moduleLogger
                        ret<-1
                        manualResetEvent.Set()
                        ()



//logEvent Verbose "Method XXXXX beginning....." moduleLogger
//logEvent Verbose "..... Method XXXXX ending. Normal Path." moduleLogger
