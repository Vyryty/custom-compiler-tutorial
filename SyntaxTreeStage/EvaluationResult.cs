using custom_compiler_tutorial.CompilationStage;
using System.Collections.Immutable;

namespace custom_compiler_tutorial.SyntaxTreeStage
{
    public sealed class EvaluationResult
    {
        public ImmutableArray<Diagnostic> Diagnostics { get; }
        public object Value { get; }

        public EvaluationResult(ImmutableArray<Diagnostic> diagnostics, object value)
        {
            Diagnostics = diagnostics;
            Value = value;
        }
    }
}
