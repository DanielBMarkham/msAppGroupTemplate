namespace UpdateAccounts.Compiler
  module Util=
    open System
    open System.IO
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open UpdateAccounts
    open UpdateAccounts.Types
    open UpdateAccounts.Lenses
    open UpdateAccounts.Persist
    open UpdateAccounts.Utilities
    open UpdateAccounts.Core
    open UpdateAccounts.Core.Util
    open UpdateAccounts.Core.Compiler
    open Logary // needed at bottom to give right "Level" lookup for logging
    open System.Numerics

    // * Opens have gotten fewer. Still working through my code-build-debug cycle
    // * Have yet to use the debugger (!)

    // Tag-list for the logger is namespace, project name, file name
    let moduleLogger = logary.getLogger (PointName [| "EA"; "Compiler"; "EA"; "Util" |])
    // For folks on anal mode, log the module being entered.  NounVerb Proper Case
    logEvent Verbose "Module enter...." moduleLogger






    // INPUT STUFF
    let getStringOrEmpty (str:string []) indx = try str.[indx] with |_->""
    let getFloatOrEmpty (str:string []) indx = try Double.Parse(str.[indx]) with |_->0.0


    type ValidatedProgramInputLine =
      {
        temp:int
      }

    type ProcessInputLineFunction =
      string->string->ValidatedProgramInputLine option
    type ProcessInputFileFunction =
      string->(string*seq<ValidatedProgramInputLine>) option
//    type ProcessDataOutput = 
 //     ValidatedProgramInput * seq<FlattenedTask> * seq<FlattenedTask>

    type TransformFileLinesFunction =
      string->string[]->seq<ValidatedProgramInputLine option>
//    type ProcessInputFunction =
//      string[]->ValidatedProgramInput


    let processInputLine:ProcessInputLineFunction =
      (fun strippedSourceFileName fileLine ->
        try // The file line reads, but does not parse
          let retTemp=0
          Some {
                temp=retTemp
               }
        with |_->None
      )

    let transformFileLines:TransformFileLinesFunction =
      (fun strippedSourceFileName fileLines->
        let ret:seq<ValidatedProgramInputLine option> = 
          fileLines |> Seq.map(fun line->
            processInputLine strippedSourceFileName line
            )
        ret  
      )
    let processInputFile:ProcessInputFileFunction =
      (fun fileName->
        try // the entire file fails the read
          let dotFound = if fileName.LastIndexOf(".") = -1 then 0 else fileName.LastIndexOf(".")
          let lastFileSeparator = if fileName.LastIndexOf(Path.DirectorySeparatorChar) =(-1) then -1 else fileName.LastIndexOf(Path.DirectorySeparatorChar)
          let strippedSourceFileName = fileName.Substring(lastFileSeparator+1,dotFound-lastFileSeparator-1)

          let fileLines= File.ReadAllLines(fileName)
          let transformedFileLines = 
            transformFileLines strippedSourceFileName fileLines
            |> Seq.choose(fun line->line)
          Some (strippedSourceFileName, transformedFileLines)
        with |_->None
      )



    type IncomingUnitType = {
      Info:System.IO.FileInfo
      FileContents:string[]
      }

    type ValidatedProgramInput =
      {
        config:appConfigType
        temp:int
      }
    type ProcessInputFunction =
      (appConfigType*IncomingUnitType[])->ValidatedProgramInput
    let processInput:ProcessInputFunction =
      (fun (appConfig,incomingStream)->
        // step 1: pull in command parms and any OS data they point to
        let reportFilename= (DateTime.Today.ToShortDateString()).Replace("/","-") + "-report.csv"
        //let sourceDirectory=try appConfigType with |_->Environment.CurrentDirectory + string Path.DirectorySeparatorChar
        let sourceDirectory=Environment.CurrentDirectory + string Path.DirectorySeparatorChar
        //let targetDirectory=try argv.[2] with |_->Environment.CurrentDirectory + string Path.DirectorySeparatorChar
        let targetDirectory=Environment.CurrentDirectory + string Path.DirectorySeparatorChar
        let sourceFiles=try Directory.GetFiles(sourceDirectory, "*.txt") with |_->[||]
        // step 2: process each incoming file/hunk of input data
        let processedInputFiles = 
          try // catch something if the entire list read fails
            sourceFiles |> Seq.map(fun eachFile->processInputFile eachFile)
              |> Seq.choose(fun possibleFile->possibleFile)
          with |_->Seq.empty
        {config=appConfig; temp=0}
      )


    type GetIncomingUnitsToProcessFunction=appConfigType->(appConfigType * IncomingUnitType[])
    let getIncomingUnitsToProcess:GetIncomingUnitsToProcessFunction = (fun opts ->
      logEvent Logary.Debug "Method inputStuff beginning....." moduleLogger
      let didTheUserProvideAnyCLIFiles=opts.FileListFromCommandLine.Length>0
      let isThereAFileStreamGBeingPipedToUs=(opts.IncomingStream |> Seq.length)>0
      let areKeystrokesQueuedUpAndWaitingToBeProcessed = try System.Console.KeyAvailable with |_->false
      logEvent Verbose ("Method inputStuff FILES REFERENCED ON CLI: " + opts.FileListFromCommandLine.Length.ToString()) moduleLogger
      let incomingPipedFileContents = opts.IncomingStream |>Seq.toArray
      let streamFileParm:IncomingUnitType={Info=getFakeFileInfo(); FileContents=incomingPipedFileContents}
      let loadAllCLIFiles:IncomingUnitType[]=
        let incomingCLIContents =
            opts.FileListFromCommandLine |>
                Array.map(fun x->
                let (fileName:string),(fileInfo:System.IO.FileInfo)=x
                let fileContents=
                    try System.IO.File.ReadAllLines(fileName) with |_->[||]
                logEvent Verbose ("Method inputStuff file " + fileName + " line count = " + fileContents.Length.ToString()) moduleLogger
                (fileInfo,fileContents)
                )
        let collapsedMap:IncomingUnitType[]=incomingCLIContents |> Array.map(fun (x,y)->{Info=x;FileContents=y})
        collapsedMap
      let compilationUnitsToReturn:IncomingUnitType[] =
          match isThereAFileStreamGBeingPipedToUs, didTheUserProvideAnyCLIFiles
            with
                | true, true->
                    logEvent Verbose "Method inputStuff FileStream: YES, Extra CLI files: YES." moduleLogger
                    let ret = loadAllCLIFiles |> Array.append [|streamFileParm|]
                    ret
                | true,false->
                    logEvent Verbose "Method inputStuff FileStream: YES, Extra CLI files: NO." moduleLogger
                    [|streamFileParm|]
                | false,true->
                    logEvent Verbose "Method inputStuff FileStream: NO, Extra CLI files: YES." moduleLogger
                    loadAllCLIFiles
                | false,false->
                    logEvent Verbose "Method inputStuff FileStream: NO, Extra CLI files: NO." moduleLogger
                    [||]
      logEvent Logary.Debug "..... Method inputStuff ending. Normal Path." moduleLogger
      (opts, compilationUnitsToReturn)
    )


    type ProcessedProgramOutput =
      {
        config:appConfigType
        temp:int
      }
    type ProcessOutputFunction =
      ProcessedProgramOutput -> int

    // OUTPUT STUFF
    let processOutput:ProcessOutputFunction = 
      (fun (programOutput) ->
        try
          ()
        with |_->() |> ignore
        0  
      )

    type ProcessDataFunction = 
      ValidatedProgramInput -> ProcessedProgramOutput
    // IMPORTANT BUSINESS WORK
    let processData:ProcessDataFunction = 
      (fun cleanedInputData->
        {
          config=cleanedInputData.config
          temp=0
        }
      )



(*

[<EntryPoint>]
let main argv =
    printfn "Hello World from TaskBarrell!"
    argv |> processInput |> processData |> processOutput
*)


    let newMain (argv:string[]) (compilerCancelationToken:System.Threading.CancellationTokenSource) (manualResetEvent:System.Threading.ManualResetEventSlim) (incomingStream:seq<string>) (ret:int byref) =
      try
        logEvent Verbose "Method newMain beginning....." moduleLogger
        //logEvent Logary.Debug ("Method newMain incomingStuff lineCount = " + (incomingStream |> Seq.length).ToString()) moduleLogger

        // Error is the new Out. Write here so user can pipe places
        //Console.Error.WriteLine "I am here. yes."
       // incomingStream |> Seq.iter(fun x->Console.Error.Write(x))
        let mutable ret=loadappConfigFromCommandLine argv incomingStream |> getIncomingUnitsToProcess |> processInput |> processData |> processOutput
        // I'm done (since I'm a single-threaded function, I know this)
        // take a few seconds to catch up, then you may run until you quit
        logEvent Verbose "..... Method newMain ending. Normal Path." moduleLogger
        compilerCancelationToken.Token.WaitHandle.WaitOne(3000) |> ignore
        manualResetEvent.Set()
        ()
      with
          | :? System.NotSupportedException as nse->
              logEvent Logary.Debug ("..... Method newMain ending. NOT SUPPORTED EXCEPTION = " + nse.Message) moduleLogger
              ()
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
    // For folks on anal mode, log the module being exited.  NounVerb Proper Case
    logEvent Verbose "....Module exit" moduleLogger










(*


    let doStuff:RunCompilationType = (fun (opts, compilationUnitArray)->
      logEvent Logary.Debug ("Method doStuff beginning..... " + compilationUnitArray.Length.ToString() + " Compilation Units coming in with " + (compilationUnitArray |> Seq.sumBy(fun x->x.FileContents.Length)).ToString() + " total lines") moduleLogger
      let libraryLogger:Logger= (logary.getLogger (PointName [| "app"; "Core"; "Compiler"; "appLib"; "Compiler" |]))
      let l2 (lvl:LogLevel) (str:string) = logEvent lvl str libraryLogger
      l2 Debug "..... Setting Remote Logger." 
      setLogger (l2)
      let ret:CompilationResultType=app.Core.Compiler.Compile(compilationUnitArray)
      logEvent Logary.Debug ("..... Method doStuff ending. Normal Path. " + ret.Results.Length.ToString() + " lines in Results" ) moduleLogger
      (opts,ret)
    )

    let outputStuff:WriteOutCompiledModelType = (fun (opts, transformedModel)->
      logEvent Logary.Debug "Method outputStuff beginning....." moduleLogger
      // Our first test -- actually part of prod code -- should compile the results again with same result
      // If the loop-through check fails, we're not writing out
      let initialOutput=transformedModel
      let newFakeInfo=getFakeFileInfo()
      let newFileContents:string[] = 
        initialOutput.Results 
        |> Array.filter(fun x->isCompilationLineAFileMarker x = false)
        |> Array.map(fun x->x.LineText)
      logEvent Logary.Verbose ("Method outputStuff newFileContents length = " + newFileContents.Length.ToString()) moduleLogger
      let newParm:CompilationUnitType[]=[|{Info=newFakeInfo; FileContents=newFileContents}|]
      let opts,secondTimeThrough=(opts,newParm) |> doStuff
      logEvent Logary.Verbose ("Method outputStuff secondTimeThrough length = " + secondTimeThrough.Results.Length.ToString()) moduleLogger
      // ROUND-TRIP TEST DOESN'T MAKE SENSE WITH THE CODE IN THIS HALF-ASSED STATE
      // TURN BACK ON LATER
      //if initialOutput<>secondTimeThrough
      //  then
      //      logEvent Logary.Error ("..... Method outputStuff loopback FAIL" ) moduleLogger
      //      failwith "MODEL LOOPBACK FAILURE"
      //  else
      let totalCharacters = transformedModel.Results |> Array.sumBy(fun x->x.LineText.Length)
      logEvent Logary.Debug ("..... Method outputStuff totalCharacters = " + totalCharacters.ToString()) moduleLogger
      // THINGS GET WRITTEN OUT HERE
      transformedModel.Results |> removeFileMarkersFromStream |> Array.iteri(fun i x->
        Console.Error.WriteLine(x.LineText)
        )
      logEvent Logary.Verbose ("Method outputStuff transformedModel length = " + transformedModel.Results.Length.ToString()) moduleLogger

      let tempFeedbackSB = new System.Text.StringBuilder(65535)
      transformedModel.Results|>Array.iteri(fun i x->
        let linenum = i.ToString() + "(" + x.LineNumber.ToString() + ")."
        let l2 = linenum + "         ".Substring(linenum.Length)
        let t1 = l2 + "" + (match x.Type with |LineType lt->lt.ToString() |CommandMatch cm->cm.LineType.ToString())
        let t2 = t1 + "                           | ".Substring(t1.Length)
        tempFeedbackSB.Append(t2 + " " + x.LineText + "\r\n") |> ignore
        )
      Console.Out.WriteLine()
      Console.Out.WriteLine(tempFeedbackSB.ToString())
      logEvent Logary.Debug "..... Method outputStuff ending. Normal Path." moduleLogger
      0 //  it's always successful as far as the O/S is concerned
    )

*)

//logEvent Debug "Method XXXXX beginning....." moduleLogger
//logEvent Debug "..... Method XXXXX ending. Normal Path." moduleLogger