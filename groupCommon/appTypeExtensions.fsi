namespace app
  module Types=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open Logary
    // * Started using my first FSI Signature file
    // * The Types problem and dlls - differences from Microservices
    type LogEventParms=LogLevel*string*Logary.Logger

    type appConfigType =
        {
        ConfigBase:ConfigBase
        FileListFromCommandLine:(string*System.IO.FileInfo)[]
        IncomingStream:seq<string>
        }
        with member PrintThis:unit->unit

    type appRConfigType =
        {
        ConfigBase:ConfigBase
        FileListFromCommandLine:(string*System.IO.FileInfo)[]
        IncomingStream:seq<string>
        }
        with member PrintThis:unit->unit

    val defaultappConfig:appConfigType
    val defaultappRConfig:appRConfigType

    val logEvent:LogLevel->string->Logary.Logger->unit
    val logary:Logary.LogManager

 