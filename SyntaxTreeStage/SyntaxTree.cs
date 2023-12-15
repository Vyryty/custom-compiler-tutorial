using custom_compiler_tutorial.LexerStage;
using custom_compiler_tutorial.ParserStage;

namespace custom_compiler_tutorial.SyntaxTreeStage
{
    public sealed class SyntaxTree
    {
        public ExpressionSyntax Root { get; }
        public SyntaxToken EndOfFileToken { get; }
        public IReadOnlyList<string> Diagnostics { get; }

        public SyntaxTree(IEnumerable<string> diagnostics, ExpressionSyntax root, SyntaxToken endOfFileToken)
        {
            Root = root;
            EndOfFileToken = endOfFileToken;
            Diagnostics = diagnostics.ToArray();
        }

        public static SyntaxTree Parse(string text)
        {
            Parser parser = new(text);
            return parser.Parse();
        }
    }
}
