using custom_compiler_tutorial.LexerStage;
using System.Collections;

namespace custom_compiler_tutorial.CompilationStage
{
    public sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> diagnostics = new();
        public IEnumerator<Diagnostic> GetEnumerator() => diagnostics.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void AddRange(DiagnosticBag diagnostics)
        {
            this.diagnostics.AddRange(diagnostics);
        }

        public void ReportInvalidNumber(TextSpan span, string text, Type type)
        {
            string message = $"Error: The number {text} is not a valid {type}";
            Report(span, message);
        }

        public void ReportBadCharacter(int position, char character)
        {
            string message = $"Error: Bad character input: '{character}'";
            Report(new(position, 1), message);
        }

        private void Report(TextSpan span, string message)
        {
            Diagnostic diagnostic = new(span, message);
            diagnostics.Add(diagnostic);
        }

        public void ReportUnexpectedToken(TextSpan span, SyntaxKind actualKind, SyntaxKind expectedKind)
        {
            string message = $"Error: Unexpected token <{actualKind}>, expected <{expectedKind}>";
            Report(span, message);
        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
        {
            string message = $"Unary operator '{operatorText}' is not defined for type {operandType}";
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType)
        {
            string message = $"Binary operator '{operatorText}' is not defined for type {leftType} and {rightType}";
            Report(span, message);
        }

        public void ReportUndefinedName(TextSpan span, string name)
        {
            string message = $"Variable '{name}' has not been declared";
            Report(span, message);
        }
    }
}
