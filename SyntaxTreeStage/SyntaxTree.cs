using custom_compiler_tutorial.CompilationStage;
using custom_compiler_tutorial.LexerStage;
using custom_compiler_tutorial.ParserStage;
using System.Collections.Immutable;

namespace custom_compiler_tutorial.SyntaxTreeStage
{
    public sealed class SyntaxTree
    {
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndOfFileToken { get; }
        public ImmutableArray<Diagnostic> Diagnostics { get; }

        public SyntaxTree(ImmutableArray<Diagnostic> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken)
        {
            Root = root;
            EndOfFileToken = endOfFileToken;
            Diagnostics = diagnostics;
        }

        public static SyntaxTree Parse(string text)
        {
            Parser parser = new(text);
            return parser.Parse();
        }

        public static IEnumerable<SyntaxToken> ParseTokens(string text)
        {
            Lexer lexer = new(text);
            while (true)
            {
                SyntaxToken token = lexer.Lex();
                if (token.Kind == SyntaxKind.EndOfFileToken) break;

                yield return token;
            }
        }
    }
}
