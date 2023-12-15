using custom_compiler_tutorial.LexerStage;
using custom_compiler_tutorial.SyntaxTreeStage;

namespace custom_compiler_tutorial.ParserStage
{
    public class Parser
    {
        private readonly SyntaxToken[] tokens;
        private int position = 0;
        private List<string> diagnostics = new();
        public IEnumerable<string> Diagnostics => diagnostics;

        public Parser(string text)
        {
            List<SyntaxToken> tokens = new();
            Lexer lexer = new(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();
                if (token.Kind != SyntaxKind.WhitespaceToken && token.Kind != SyntaxKind.BadToken) tokens.Add(token);
            }
            while (token.Kind != SyntaxKind.EndOfFileToken);

            this.tokens = tokens.ToArray();
            diagnostics.AddRange(lexer.Diagnostics);
        }

        private SyntaxToken NextToken()
        {
            SyntaxToken current = Current;
            position++;
            return current;
        }

        private SyntaxToken Peek(int offset)
        {
            int index = position + offset;
            if (index >= tokens.Length) return tokens[tokens.Length - 1];
            return tokens[index];
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind) return NextToken();

            diagnostics.Add($"Error: Unexpected token <{Current.Kind}>, expected <{kind}>");
            return new SyntaxToken(kind, Current.position, null, null);
        }

        public SyntaxTree Parse()
        {
            ExpressionSyntax expression = ParseTerm();
            SyntaxToken endOfFileToken = Match(SyntaxKind.EndOfFileToken);

            return new SyntaxTree(diagnostics, expression, endOfFileToken);
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        private ExpressionSyntax ParseTerm()
        {
            ExpressionSyntax left = ParseFactor();

            while (Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.MinusToken)
            {
                SyntaxToken operatorToken = NextToken();
                ExpressionSyntax right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParseFactor()
        {
            ExpressionSyntax left = ParsePrimaryExpression();

            while (Current.Kind == SyntaxKind.StarToken || Current.Kind == SyntaxKind.SlashToken)
            {
                SyntaxToken operatorToken = NextToken();
                ExpressionSyntax right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if (Current.Kind == SyntaxKind.OpenParenthesisToken)
            {
                SyntaxToken left = NextToken();
                ExpressionSyntax expression = ParseExpression();
                SyntaxToken right = Match(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            SyntaxToken numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }

        private SyntaxToken Current => Peek(0);
    }
}
