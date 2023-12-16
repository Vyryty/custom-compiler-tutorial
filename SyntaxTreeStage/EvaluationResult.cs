using custom_compiler_tutorial.CompilationStage;

namespace custom_compiler_tutorial.SyntaxTreeStage
{
    public sealed class EvaluationResult
    {
        public IReadOnlyList<Diagnostic> Diagnostics { get; }
        public object Value { get; }

        public EvaluationResult(IEnumerable<Diagnostic> diagnostics, object value)
        {
            Diagnostics = diagnostics.ToArray();
            Value = value;
        }
    }
}
