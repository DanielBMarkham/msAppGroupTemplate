namespace IncomingCharges.Test
  module LineIdentificationTests=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open IncomingCharges
    open IncomingCharges.Types
    open IncomingCharges.Lenses
    open IncomingCharges.Persist
    open IncomingCharges.Core
    open IncomingCharges.Core.Util
    open IncomingCharges.Core.Compiler
    open IncomingCharges.Core.Tokens
    open Expecto
    open Util
    open Logary // needed at bottom to give right "Level" lookup for logging
    // Tag-list for the logger is namespace, project name, file name
    let moduleLogger = logary.getLogger (PointName [| "EA"; "Test";  "EATest"; "Util" |])
    // For folks on anal mode, log the module being entered.  NounVerb Proper Case
    logEvent Verbose "Module enter...." moduleLogger


    
    [<Tests>]
    let tests =
      testList "TranslateIncomingIntoOneStream" [
        testCase "Empty file gets bookends" <| fun _ ->
          let newParm=[|{Info=FakeFile1; FileContents=[||]}|]
          let result=[]// processing goes here 
          Expect.isTrue (true) "True is true"

      ]
    // you may wantg to put some sample file data here for a mock
    let BIG_OLD_SAMPLE_TEXT="""
#dontknow
The dogs ate the cat
[ ] I love my cat
    [] I love dogs
NAMESPACE=Dogs
 #"ankles over the hardtack" 
-----TO-DO------
[ ]Yes, I will have some turkey
I love USER STORIES!
- I love dogs
  &"this is a bunch of text with an equals = in it"="Bunch of other text"
  &"The moon dance is not for everybody = == oreos especially" = "Not a joke"
TO-DO-----
-I love dogs
TO-DO: Wash the dishes
  #dogs
NAMESPACEMonkeyButt
&sprint=9
Weasel hate TO-DO My weasel
#nose #@
@no I do not like hovercrafts
##@
#@dfa
Unicorns are all around us TO-DO
  USER STORIES
# I saw the darkness
TO-DOTO-DO d
Unicorns are all around us TO-DO
USER STORIES
My horns ran occupied by jello #toosad #toobad
NAMESPACE dogs in the tree
"""

      //findFirstL

//logEvent Verbose "Method XXXXX beginning....." moduleLogger
//logEvent Verbose "..... Method XXXXX ending. Normal Path." moduleLogger
