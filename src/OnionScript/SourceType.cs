namespace OrbHall.OnionScript
{
    /// <summary>
    /// Modes in which the <see cref="ScriptEnvironment"/> can load code.
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// Source code that has to be compiled before use.
        /// </summary>
        SourceCode,

        /// <summary>
        /// Pre-compiled source code that has been saved to binary.
        /// This is the faster option as the entire compilation step can be skipped.
        /// </summary>
        PrecompiledBinary
    }
}
