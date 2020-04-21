namespace UpdateAccounts.Test
  module Model1Tests=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open UpdateAccounts
    open UpdateAccounts.Types
    open UpdateAccounts.Lenses
    open UpdateAccounts.Persist
    open UpdateAccounts.Core
    open UpdateAccounts.Core.Util
    open UpdateAccounts.Core.Compiler
    open UpdateAccounts.Core.Tokens
    open Expecto
    open Util
    open Logary // needed at bottom to give right "Level" lookup for logging
    open System.CodeDom.Compiler

    // Tag-list for the logger is namespace, project name, file name
    let moduleLogger = logary.getLogger (PointName [| "EA"; "Test";  "EATest"; "LineID2Tests" |])
    // For folks on anal mode, log the module being entered.  


    [<Tests>]
    let tests =
      testList "complex model testing here" [

      ]