namespace custom_compiler_tutorial.LexerStage
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        IdentifierToken,
        AmpersandAmpersandToken,
        BangToken,

        // Keywords
        TrueKeyword,
        FalseKeyword,

        // Expressions
        BinaryExpression,
        LiteralExpression,
        ParenthesizedExpression,
        UnaryExpression,
        PipePipeToken,
        BangEqualsToken,
        EqualsEqualsToken,
    }
}
