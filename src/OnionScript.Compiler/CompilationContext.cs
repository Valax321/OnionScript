using System;
using System.Collections.Generic;
using System.Text;

namespace OrbHall.OnionScript.Compiler
{
    internal readonly struct CompilationContext
    {
        public string ScriptPath { get; init; }
        public CompiledScript ScriptObject { get; init; }
    }
}
