using custom_compiler_tutorial.CompilationStage;
using custom_compiler_tutorial.ParserStage;

namespace custom_compiler_tutorial.LexerStage
{
    public sealed class SyntaxToken : SyntaxNode
    {
        public override SyntaxKind Kind { get; }
        public int Position { get; }
        public override string Text { get; }
        public object Value { get; }
        public TextSpan Span => new(Position, Text.Length);

        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
