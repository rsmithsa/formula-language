﻿//-----------------------------------------------------------------------
// <copyright file="CsWrapper.fs" company="Richard Smith">
//     Copyright (c) Richard Smith. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Formula.Parser.Integration

module CsWrapper =
    open System
    open FParsec.CharParsers

    open Formula.Parser.Parser
    open Formula.Parser.Interpreter

    let private dictionaryToMap (dictionary : System.Collections.Generic.IDictionary<_,_>) = 
        dictionary 
        |> Seq.map (|KeyValue|)  
        |> Map.ofSeq

    let ParseFormula input =
        let result = parseFormulaString input
        match result with
        | Success (ast, a, b) ->
            ast
        | Failure (msg, a, b) ->
            raise (ArgumentException(msg, "input"))

    let InterpretFormula input (variables: System.Collections.Generic.IDictionary<string,double>) =
        let result = parseFormulaString input
        match result with
        | Success (ast, userState, endPos) ->
            interpretFormula ast (dictionaryToMap variables)
        | Failure (msg, error, userState) ->
            raise (ArgumentException(msg, "input"))