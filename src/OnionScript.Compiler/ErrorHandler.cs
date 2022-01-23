using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;

namespace OrbHall.OnionScript.Compiler
{
    internal class ErrorHandler : IAntlrErrorStrategy
    {
        public void Reset(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public IToken RecoverInline(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public void Recover(Parser recognizer, RecognitionException e)
        {
            throw new NotImplementedException();
        }

        public void Sync(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public bool InErrorRecoveryMode(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public void ReportMatch(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public void ReportError(Parser recognizer, RecognitionException e)
        {
            throw new NotImplementedException();
        }
    }
}
