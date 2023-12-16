using custom_compiler_tutorial.CompilationStage;
using custom_compiler_tutorial.ParserStage;

namespace custom_compiler_tutorial.LexerStage
{
    public sealed class Lexer
    {
        private readonly string text;
        private int position;
        private DiagnosticBag diagnostics = new();
        public DiagnosticBag Diagnostics => diagnostics;

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

            int start = position;

            if (char.IsDigit(Current))
            {
                while (char.IsDigit(Current)) Next();
                int length = position - start;
                string text = this.text.Substring(start, length);
                if (!int.TryParse(text, out int value)) diagnostics.ReportInvalidNumber(new(start, length), text, typeof(int));
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                while (char.IsWhiteSpace(Current)) Next();
                int length = position - start;
                string text = this.text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
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
                    if (Lookahead == '=')
                    {
                        position += 2;
                        return new(SyntaxKind.BangEqualsToken, start, "!=", null);
                    }
                    return new(SyntaxKind.BangToken, position++, "!", null);
                case '&':
                    if (Lookahead == '&')
                    {
                        position += 2;
                        return new(SyntaxKind.AmpersandAmpersandToken, start, "&&", null);
                    }
                    break;
                case '|':
                    if (Lookahead == '|')
                    {
                        position += 2;
                        return new(SyntaxKind.PipePipeToken, start, "||", null);
                    }
                    break;
                case '=':
                    if (Lookahead == '=')
                    {
                        position += 2;
                        return new(SyntaxKind.EqualsEqualsToken, start, "==", null);
                    }
                    return new(SyntaxKind.EqualsToken, position++, "=", null);
            }

            diagnostics.ReportBadCharacter(position, Current);
            return new SyntaxToken(SyntaxKind.BadToken, position++, text.Substring(position - 1, 1), null);
        }
    }
}
