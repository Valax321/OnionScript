using System;
using System.Diagnostics;
using Antlr4.Runtime;
using JetBrains.Annotations;

namespace OrbHall.OnionScript.Compiler
{
    /// <summary>
    /// The script compiler implementation.
    /// </summary>
    [PublicAPI]
    public sealed class ScriptCompiler : IScriptCompiler
    {
        /// <summary>
        /// Create a new instance of a <see cref="ScriptCompiler"/>.
        /// </summary>
        /// <returns></returns>
        public static ScriptCompiler Create(ScriptEnvironment environment, Action<ScriptCompiler>? configuration = null)
        {
            var compiler = new ScriptCompiler(environment);
            configuration?.Invoke(compiler);
            return compiler;
        }

        /// <summary>
        /// Callback for logging informational messages.
        /// </summary>
        public Action<string>? Log { get; set; }

        /// <summary>
        /// Callback for logging warning messages.
        /// </summary>
        public Action<string>? LogWarning { get; set; }

        /// <summary>
        /// Callback for logging error messages.
        /// </summary>
        public Action<string>? LogError { get; set; }

        /// <summary>
        /// The script environment we're compiling into.
        /// </summary>
        public ScriptEnvironment Environment { get; }

        private ScriptCompiler(ScriptEnvironment environment)
        {
            Environment = environment;
        }

        /// <summary>
        /// Compiles the script with the given name into a <see cref="CompiledScript"/> that can be executed.
        /// </summary>
        /// <param name="filePath">The name/path of the script being compiled.</param>
        /// <returns>A <see cref="CompiledScript"/> object.</returns>
        /// <exception cref="CompilerException"></exception>
        public CompiledScript Compile(string filePath)
        {
            var script = new CompiledScript();
            CompileIntoScriptObject(filePath, string.Empty, script);
            return script;
        }

        internal void CompileIntoScriptObject(string filePath, string parentPath, CompiledScript script)
        {
            using var scriptStream = Environment.OpenScriptFileFunc(filePath, string.Empty, out var compiledScript);
            if (scriptStream is null)
                throw new CompilerException(
                    $"Could not compile {filePath}: Environment.OpenScriptFileFunc did not open a stream");

            var antlrStream = new AntlrInputStream(scriptStream);

            var lexer = new OnionScriptLexer(antlrStream);
            lexer.AddErrorListener(new ErrorReporter(this, filePath));
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new OnionScriptParser(tokenStream);
            parser.AddErrorListener(new ErrorReporter(this, filePath));
            var visitor = new OnionScriptVisitor(this, new CompilationContext
            {
                ScriptPath = filePath,
                ScriptObject = script
            });
            visitor.Visit(parser.script());
        }

        [Conditional("DEBUG")]
        internal void LogDebug(string msg)
        {
            Log?.Invoke(msg);
        }
    }
}
