using System;
using System.Collections.Generic;
using System.Text;

namespace OrbHall.OnionScript
{
    /// <summary>
    /// Interface for the script compiler implementation in OnionScript.Compiler
    /// </summary>
    public interface IScriptCompiler
    {
        CompiledScript Compile(string filePath);
    }
}
