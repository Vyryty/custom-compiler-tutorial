using custom_compiler_tutorial.LexerStage;

namespace custom_compiler_tutorial.ParserStage
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}
