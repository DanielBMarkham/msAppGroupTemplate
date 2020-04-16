namespace EA
  module Utilities=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open EA.Types
    open EA.Lenses
    open Logary

    // Tag-list for the logger is namespace, project name, file name
    let moduleLogger = logary.getLogger (PointName [| "EA"; "Utilities"; "EAAppUtilities" |])
    // For folks on anal mode, log the module being entered.  NounVerb Proper Case
    logEvent Verbose "Module enter...." moduleLogger
    Console.WriteLine "whoa"
    
    // For folks on anal mode, log the module being exited.  NounVerb Proper Case
    logEvent Verbose "....Module exit" moduleLogger