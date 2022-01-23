using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Antlr4.Runtime;

namespace OrbHall.OnionScript.Compiler
{
    internal class ErrorReporter : IAntlrErrorListener<int>, IAntlrErrorListener<IToken>
    {
        private readonly ScriptCompiler Compiler;
        private readonly string ScriptName;

        public ErrorReporter(ScriptCompiler compiler, string scriptName)
        {
            Compiler = compiler;
            ScriptName = scriptName;
        }

        // This is for lexer errors
        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine,
            string msg, RecognitionException e)
        {
            
        }

        // This is for parser errors
        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine,
            string msg, RecognitionException e)
        {
            
        }
    }
}
