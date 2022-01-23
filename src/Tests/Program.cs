using OrbHall.OnionScript;
using OrbHall.OnionScript.Compiler;

var env = new ScriptEnvironment();
env.Compiler = ScriptCompiler.Create(env, compiler =>
{
    compiler.Log = Console.WriteLine;
    compiler.LogWarning = Console.WriteLine;
    compiler.LogError = Console.WriteLine;
});
env.RegisterBuiltinFunction("print", (string msg) => Console.WriteLine(msg));
env.RegisterBuiltinFunction("format", (string fmt, object p1) => Console.WriteLine(string.Format(fmt, p1)));

env.Compiler.Compile("hello_world.onion");
env.BeginExecute("__main");