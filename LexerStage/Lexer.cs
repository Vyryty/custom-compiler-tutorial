namespace custom_compiler_tutorial.LexerStage
{
    public class Lexer
    {
        private readonly string text;
        private int position;
        private List<string> diagnostics = new();
        public IEnumerable<string> Diagnostics => diagnostics;

        private char Current
        {
            get
            {
                if (position >= text.Length) return '\0';

                return text[position];
            }
        }

        public Lexer(string text)
        {
            this.text = text;
        }

        public void Next()
        {
            position++;
        }

        public SyntaxToken NextToken()
        {
            if (Current == '\0') return new SyntaxToken(SyntaxKind.EndOfFileToken, position, "\0", null);

            if (char.IsDigit(Current))
            {
                int start = position;
                while (char.IsDigit(Current)) Next();
                int length = position - start;
                string text = this.text.Substring(start, length);
                if (!int.TryParse(text, out int value)) diagnostics.Add($"ERROR: The number {text} is not a valid Int32");
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                int start = position;
                while (char.IsWhiteSpace(Current)) Next();
                int length = position - start;
                string text = this.text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if (Current == '+') return new SyntaxToken(SyntaxKind.PlusToken, position++, "+", null);
            if (Current == '-') return new SyntaxToken(SyntaxKind.MinusToken, position++, "-", null);
            if (Current == '*') return new SyntaxToken(SyntaxKind.StarToken, position++, "*", null);
            if (Current == '/') return new SyntaxToken(SyntaxKind.SlashToken, position++, "/", null);
            if (Current == '(') return new SyntaxToken(SyntaxKind.OpenParenthesisToken, position++, "(", null);
            if (Current == ')') return new SyntaxToken(SyntaxKind.CloseParenthesisToken, position++, ")", null);

            diagnostics.Add($"ERROR: Bad character input '{Current}' at index {position}");
            return new SyntaxToken(SyntaxKind.BadToken, position++, text.Substring(position - 1, 1), null);
        }
    }
}
