using custom_compiler_tutorial.ParserStage;

namespace custom_compiler_tutorial.LexerStage
{
    public class SyntaxToken : SyntaxNode
    {
        public override SyntaxKind Kind { get; }
        public int position;
        public string text;
        public object value;

        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            this.Kind = kind;
            this.position = position;
            this.text = text;
            this.value = value;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}
