using custom_compiler_tutorial.ParserStage;

namespace custom_compiler_tutorial.LexerStage
{
    public sealed class Lexer
    {
        private readonly string text;
        private int position;
        private List<string> diagnostics = new();
        public IEnumerable<string> Diagnostics => diagnostics;

        private char Current => Peek(0);
        private char Lookahead => Peek(1);

        private char Peek(int offset)
        {
            int index = position + offset;
            if (index >= text.Length) return '\0';

            return text[index];
        }

        public Lexer(string text)
        {
            this.text = text;
        }

        public void Next()
        {
            position++;
        }

        public SyntaxToken Lex()
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

            if (char.IsLetter(Current))
            {
                int start = position;
                while (char.IsLetter(Current)) Next();
                int length = position - start;
                string text = this.text.Substring(start, length);
                SyntaxKind kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }

            switch (Current)
            {
                case '+':
                    return new(SyntaxKind.PlusToken, position++, "+", null);
                case '-':
                    return new(SyntaxKind.MinusToken, position++, "-", null);
                case '*':
                    return new(SyntaxKind.StarToken, position++, "*", null);
                case '/':
                    return new(SyntaxKind.SlashToken, position++, "/", null);
                case '(':
                    return new(SyntaxKind.OpenParenthesisToken, position++, "(", null);
                case ')':
                    return new(SyntaxKind.CloseParenthesisToken, position++, ")", null);

                case '!':
                    if (Lookahead == '=') return new(SyntaxKind.BangEqualsToken, position += 2, "!=", null);
                    return new(SyntaxKind.BangToken, position ++, "!", null);
                case '&':
                    if (Lookahead == '&') return new(SyntaxKind.AmpersandAmpersandToken, position += 2, "&&", null);
                    break;
                case '|':
                    if (Lookahead == '|') return new(SyntaxKind.PipePipeToken, position += 2, "||", null);
                    break;
                case '=':
                    if (Lookahead == '=') return new(SyntaxKind.EqualsEqualsToken, position += 2, "==", null);
                    break;
            }

            diagnostics.Add($"ERROR: Bad character input '{Current}' at index {position}");
            return new SyntaxToken(SyntaxKind.BadToken, position++, text.Substring(position - 1, 1), null);
        }
    }
}
