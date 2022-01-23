using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OrbHall.OnionScript.Compiler
{
    internal class OnionScriptVisitor : OnionScriptBaseVisitor<object>
    {
        private readonly ScriptCompiler Compiler;
        private readonly CompilationContext Context;

        public OnionScriptVisitor(ScriptCompiler compiler, CompilationContext context)
        {
            Compiler = compiler;
            Context = context;
        }

        public override object VisitImport_statement(OnionScriptParser.Import_statementContext context)
        {
            var import = context.text().Accept(this) as string;
            Compiler.LogDebug($"Importing {import}");
            Compiler.CompileIntoScriptObject(import!, Context.ScriptPath, Context.ScriptObject);

            return base.VisitImport_statement(context);
        }

        public override object VisitFunction_definition(OnionScriptParser.Function_definitionContext context)
        {
            Compiler.LogDebug($"Visiting function definition {context.identifier()[0].Accept(this)}");

            return base.VisitFunction_definition(context);
        }

        public override object VisitInteger(OnionScriptParser.IntegerContext context)
        {
            var integer = context.INTEGER().GetText();
            var format = NumberStyles.Integer;
            if (integer.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                format = NumberStyles.HexNumber;
                integer = integer.Substring(2);
            }

            return int.Parse(integer, format);
        }

        public override object VisitFloat(OnionScriptParser.FloatContext context)
        {
            var single = context.FLOAT().GetText();
            return float.Parse(single);
        }

        public override object VisitBoolean(OnionScriptParser.BooleanContext context)
        {
            var boolean = context.BOOLEAN().GetText();
            return bool.Parse(boolean);
        }

        public override object VisitIdentifier(OnionScriptParser.IdentifierContext context)
        {
            return context.IDENTIFIER().GetText();
        }

        public override object VisitText(OnionScriptParser.TextContext context)
        {
            return context.TEXT().GetText().Trim('"');
        }
    }
}
