namespace ValidateMerchant.Core
  module Util=
    open System
    open SystemTypeExtensions
    open SystemUtilities
    open CommandLineHelper
    open ValidateMerchant
    open ValidateMerchant.Types
    open ValidateMerchant.Lenses
    open ValidateMerchant.Persist
    open ValidateMerchant.Core.Tokens


(*    type CompilerRuleType =
        |FileBeginType of AllowedNextLinesType
        |FileEndType of AllowedNextLinesType
        |EmptyLineType of AllowedNextLinesType
        |FreeFormTextType of AllowedNextLinesType
        |CompilerSectionDirectiveType of AllowedNextLinesType
        |CompilerNamespaceDirectiveType of AllowedNextLinesType
        |CompilerTagDirectiveType of AllowedNextLinesType
        |CompilerSectionDirectiveWithItemType of AllowedNextLinesType
        |CompilerNamespaceDirectiveWithItemType of AllowedNextLinesType
        |CompilerTagDirectiveWithItemType of AllowedNextLinesType
        |CompilerJoinTypeWithItemType of AllowedNextLinesType
        |NewSectionItemType of AllowedNextLinesType
        |NewJoinedItemType of AllowedNextLinesType
        |NewItemJoinCombinationType of AllowedNextLinesType
    and AllowedNextLinesType = AllowedNextLines of (EasyAMLineTypes list)

    val CompilerRules:CompilerRuleType list
    val isThisLineAllowedNext: EasyAMLineTypes->lineTypeToTest:EasyAMLineTypes->bool
*)


(*    type InputUnitType = {
      Info:System.IO.FileInfo
      FileContents:string[]
      }*)
