using System;
using System.IO;
using JetBrains.Annotations;

namespace OrbHall.OnionScript
{
    /// <summary>
    /// The Script Environment is the host for an instance of OnionScript.
    /// Scripts are loaded into it and then executed.
    /// </summary>
    [PublicAPI]
    public class ScriptEnvironment
    {
        /// <summary>
        /// The size of the stack for function calls.
        /// Exceeding this stack size will cause an exception to be thrown.
        /// </summary>
        public int StackSize { get; init; } = 256;

        /// <summary>
        /// Callback method for opening imported script files from code.
        /// </summary>
        /// <param name="scriptName">The name of the script being imported.</param>
        /// <param name="parentScriptName">The name of the script that requested the import</param>
        /// <param name="sourceType">The mode to read this file in.</param>
        /// <returns>A <see cref="Stream"/> to the data for the file, or null if it could not be opened for any reason.</returns>
        public delegate Stream? OpenScriptFileCallback(string scriptName, string parentScriptName, out SourceType sourceType);

        /// <summary>
        /// Implementation of the script compiler. If this is not set up, only pre-compiled scripts can be loaded.
        /// </summary>
        public IScriptCompiler? Compiler { get; set; }

        /// <summary>
        /// The callback used to open script files being imported from code.
        /// How you choose to resolve the script name is up to you.
        /// Your implementation should return null if the script could not be found/loaded for any reason.
        /// </summary>
        public OpenScriptFileCallback OpenScriptFileFunc { get; set; }

        /// <summary>
        /// Creates a new instance of a <see cref="ScriptEnvironment"/>.
        /// </summary>
        public ScriptEnvironment()
        {
            OpenScriptFileFunc = DefaultOpenScriptFileImpl;
        }

        public void AddScript(CompiledScript script)
        {

        }

        public void BeginExecute(string functionName)
        {

        }

        #region Builtin Function Registration

        /// <summary>
        /// Registers a c# Action that can be called from OnionScript code via the given function name.
        /// </summary>
        /// <param name="functionName">The script-side name of the function.</param>
        /// <param name="functionImplementation">The actual method invoked in C# for this function.</param>
        public void RegisterBuiltinFunction(string functionName, Action functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn>(string functionName, Func<TReturn> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<T1>(string functionName, Action<T1> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn, T1>(string functionName, Func<TReturn, T1> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<T1, T2>(string functionName, Action<T1, T2> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn, T1, T2>(string functionName, Func<TReturn, T1, T2> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<T1, T2, T3>(string functionName, Action<T1, T2, T3> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn, T1, T2, T3>(string functionName, Func<TReturn, T1, T2, T3> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<T1, T2, T3, T4>(string functionName, Action<T1, T2, T3, T4> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn, T1, T2, T3, T4>(string functionName, Func<TReturn, T1, T2, T3, T4> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<T1, T2, T3, T4, T5>(string functionName, Action<T1, T2, T3, T4, T5> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn, T1, T2, T3, T4, T5>(string functionName, Func<TReturn, T1, T2, T3, T4, T5> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<T1, T2, T3, T4, T5, T6>(string functionName, Action<T1, T2, T3, T4, T5, T6> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn, T1, T2, T3, T4, T5, T6>(string functionName, Func<TReturn, T1, T2, T3, T4, T5, T6> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<T1, T2, T3, T4, T5, T6, T7>(string functionName, Action<T1, T2, T3, T4, T5, T6, T7> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn, T1, T2, T3, T4, T5, T6, T7>(string functionName, Func<TReturn, T1, T2, T3, T4, T5, T6, T7> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<T1, T2, T3, T4, T5, T6, T7, T8>(string functionName, Action<T1, T2, T3, T4, T5, T6, T7, T8> functionImplementation)
        {

        }

        /// <inheritdoc cref="RegisterBuiltinFunction"/>
        public void RegisterBuiltinFunction<TReturn, T1, T2, T3, T4, T5, T6, T7, T8>(string functionName, Func<TReturn, T1, T2, T3, T4, T5, T6, T7, T8> functionImplementation)
        {

        }

        #endregion

        private Stream? DefaultOpenScriptFileImpl(string scriptName, string oldScriptName, out SourceType sourceType)
        {
            sourceType = Compiler is not null ? SourceType.SourceCode : SourceType.PrecompiledBinary;
            var path = Path.Combine(Path.GetDirectoryName(oldScriptName) ?? string.Empty, scriptName);
            return !File.Exists(path) ? null : File.OpenRead(path);
        }
    }
}