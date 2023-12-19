using custom_compiler_tutorial.CompilationStage;
using custom_compiler_tutorial.ParserStage;

namespace custom_compiler_tutorial.LexerStage
{
    public sealed class Lexer
    {
        private readonly string text;
        private readonly DiagnosticBag diagnostics = new();
        public DiagnosticBag Diagnostics => diagnostics;

        private int position;

        private int start;
        private SyntaxKind kind;
        private object value;

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

        public SyntaxToken Lex()
        {
            start = position;
            kind = SyntaxKind.BadToken;
            value = null;

            switch (Current)
            {
                case '\0':
                    kind = SyntaxKind.EndOfFileToken;
                    break;
                case '+':
                    kind = SyntaxKind.PlusToken;
                    position++;
                    break;
                case '-':
                    kind = SyntaxKind.MinusToken;
                    position++;
                    break;
                case '*':
                    kind = SyntaxKind.StarToken;
                    position++;
                    break;
                case '/':
                    kind = SyntaxKind.SlashToken;
                    position++;
                    break;
                case '(':
                    kind = SyntaxKind.OpenParenthesisToken;
                    position++;
                    break;
                case ')':
                    kind = SyntaxKind.CloseParenthesisToken;
                    position++;
                    break;

                case '!':
                    position++;
                    if (Current == '=')
                    {
                        kind = SyntaxKind.BangEqualsToken;
                        position++;
                    }
                    else
                    {
                        kind = SyntaxKind.BangToken;
                    }
                    break;
                case '&':
                    position++;
                    if (Current == '&')
                    {
                        kind = SyntaxKind.AmpersandAmpersandToken;
                        position++;
                    }
                    break;
                case '|':
                    position++;
                    if (Current == '|')
                    {
                        kind = SyntaxKind.PipePipeToken;
                        position++;
                    }
                    break;
                case '=':
                    position++;
                    if (Current == '=')
                    {
                        kind = SyntaxKind.EqualsEqualsToken;
                        position++;
                    }
                    else
                    {
                        kind = SyntaxKind.EqualsToken;
                    }
                    break;

                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    ReadNumberToken();
                    break;

                case ' ':
                case '\t':
                case '\n':
                case '\r':
                    ReadWhiteSpace();
                    break;

                default:
                    if (char.IsLetter(Current)) ReadIdentifierOrKeyword();
                    else if (char.IsWhiteSpace(Current)) ReadWhiteSpace();
                    else
                    {
                        diagnostics.ReportBadCharacter(position, Current);
                        position++;
                    }
                    break;
            }

            int length = position - start;
            string? text = SyntaxFacts.GetText(kind) ?? this.text.Substring(start, length);

            return new SyntaxToken(kind, start, text, value);
        }

        private void ReadWhiteSpace()
        {
            while (char.IsWhiteSpace(Current)) position++;

            kind = SyntaxKind.WhitespaceToken;
        }

        private void ReadIdentifierOrKeyword()
        {
            while (char.IsLetter(Current)) position++;
            int length = position - start;
            string text = this.text.Substring(start, length);
            kind = SyntaxFacts.GetKeywordKind(text);
        }

        private void ReadNumberToken()
        {
            while (char.IsDigit(Current)) position++;
            int length = position - start;
            string text = this.text.Substring(start, length);
            if (!int.TryParse(text, out int value)) diagnostics.ReportInvalidNumber(new(start, length), this.text, typeof(int));

            this.value = value;
            kind = SyntaxKind.NumberToken;
        }
    }
}
