using custom_compiler_tutorial.BindingStage;
using custom_compiler_tutorial.SyntaxTreeStage;
using System.Collections.Immutable;

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

            ImmutableArray<Diagnostic> diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToImmutableArray();
            if (diagnostics.Any()) return new(diagnostics, null);

            Evaluator evaluator = new(boundExpression, variables);
            object value = evaluator.Evaluate();
            return new(ImmutableArray<Diagnostic>.Empty, value);
        }
    }
}
