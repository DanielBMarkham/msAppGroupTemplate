namespace IncomingCharges.Test
  module SmokeTests=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open IncomingCharges
    open IncomingCharges.Types
    open IncomingCharges.Lenses
    open IncomingCharges.Persist
    open IncomingCharges.Core
    open IncomingCharges.Core.Compiler
    open Expecto
    open Util
    open Logary // needed at bottom to give right "Level" lookup for logging
    // Tag-list for the logger is namespace, project name, file name
    let moduleLogger = logary.getLogger (PointName [| "EA"; "Test";  "EATest"; "Util" |])
    // For folks on anal mode, log the module being entered.  NounVerb Proper Case
    logEvent Verbose "Module enter...." moduleLogger

    [<Tests>]
    let tests =
      testList "Smoke" [
        testCase "Empty file return empty" <| fun _ ->
          let newParm=[|{Info=FakeFile1; FileContents=[||]}|]
          let result=[] // processing goes here
          Expect.isTrue (result.Length=0) "Empty input producing an output"

      
      ]

      // WHAT TO DO WITH FILE MARKERS HERE?

//logEvent Verbose "Method XXXXX beginning....." moduleLogger
//logEvent Verbose "..... Method XXXXX ending. Normal Path." moduleLogger
