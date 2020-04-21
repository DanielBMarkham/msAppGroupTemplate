namespace ValidateAccount.Test
  module Model1Tests=
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
    open ValidateAccount.Core.Tokens
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