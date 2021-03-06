namespace app
  module Persist=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open app.Types
    open app.Lenses
    open app.Utilities
    open Logary
    // Tag-list for the logger is namespace, project name, file name
    let moduleLogger = logary.getLogger (PointName [| "EA"; "Persist"; "EAPersist" |])
    // For folks on anal mode, log the module being entered.  NounVerb Proper Case
    logEvent Verbose "Module enter...." moduleLogger
    Console.WriteLine "whoa"
    
    // For folks on anal mode, log the module being exited.  NounVerb Proper Case
    logEvent Verbose "....Module exit" moduleLogger
