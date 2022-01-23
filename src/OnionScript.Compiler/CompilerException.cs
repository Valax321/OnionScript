using System;
using System.Collections.Generic;
using System.Text;

namespace OrbHall.OnionScript.Compiler
{
    /// <summary>
    /// Exception thrown when the compiler encounters an error.
    /// </summary>
    public class CompilerException : Exception
    {
        /// <inheritdoc cref="Exception(string)"/>
        public CompilerException(string message) : base(message)
        { }
    }
}
