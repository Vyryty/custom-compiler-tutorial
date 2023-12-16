using custom_compiler_tutorial.LexerStage;

namespace custom_compiler_tutorial.ParserStage
{
    sealed class LiteralExpressionSyntax : ExpressionSyntax
    {
        public object Value { get; }

        public SyntaxToken LiteralToken { get; }

        public override SyntaxKind Kind => SyntaxKind.LiteralExpression;
        public override string Text => LiteralToken.Text;

        public LiteralExpressionSyntax(SyntaxToken literalToken) : this(literalToken, literalToken.Value) { }

        public LiteralExpressionSyntax(SyntaxToken literalToken, object value)
        {
            LiteralToken = literalToken;
            Value = value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}
