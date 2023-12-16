using custom_compiler_tutorial.BindingStage;
using custom_compiler_tutorial.SyntaxTreeStage;

namespace custom_compiler_tutorial.CompilationStage
{
    public sealed class Compilation
    {
        public SyntaxTree Syntax { get; }

        public Compilation(SyntaxTree syntax)
        {
            Syntax = syntax;
        }

        public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
        {
            Binder binder = new(variables);
            BoundExpression boundExpression = binder.BindExpression(Syntax.Root);

            IReadOnlyList<Diagnostic> diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();
            if (diagnostics.Any()) return new(diagnostics, null);

            Evaluator evaluator = new(boundExpression, variables);
            object value = evaluator.Evaluate();
            return new(Array.Empty<Diagnostic>(), value);
        }
    }
}
