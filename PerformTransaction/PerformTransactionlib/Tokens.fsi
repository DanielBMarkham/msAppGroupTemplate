namespace PerformTransaction.Core
  module Tokens=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open PerformTransaction
    open PerformTransaction.Types
    open PerformTransaction.Lenses
    open PerformTransaction.Persist
    open Logary // needed at bottom to give right "Level" lookup for logging
    //open System.Linq
    open System.Text.RegularExpressions
    open System.Collections.Concurrent

(*    val getRegExesForACommand:EasyAMCommandType->RegexMatcherType[]
    val findFirstLineTypeMatch:string->RegexMatcherType 
    val findFirstCommandTypeMatch:string->LineMatcherType 
    val matchLineWithRecommendedCommand:string->LineMatcherType
*)