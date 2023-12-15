using custom_compiler_tutorial.LexerStage;

namespace custom_compiler_tutorial.ParserStage
{
    sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken NumberToken { get; }

        public override SyntaxKind Kind => SyntaxKind.NumberToken;

        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}
