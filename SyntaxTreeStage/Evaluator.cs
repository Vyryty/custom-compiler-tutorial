using custom_compiler_tutorial.LexerStage;
using custom_compiler_tutorial.ParserStage;

namespace custom_compiler_tutorial.SyntaxTreeStage
{
    public class Evaluator
    {
        private readonly ExpressionSyntax root;

        public Evaluator(ExpressionSyntax root)
        {
            this.root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(root);
        }

        private int EvaluateExpression(ExpressionSyntax node)
        {
            if (node is NumberExpressionSyntax n) return (int)n.NumberToken.value;
            if (node is BinaryExpressionSyntax b)
            {
                int left = EvaluateExpression(b.Left);
                int right = EvaluateExpression(b.Right);

                switch (b.OperatorToken.Kind)
                {
                    case SyntaxKind.PlusToken:
                        return left + right;
                    case SyntaxKind.MinusToken:
                        return left - right;
                    case SyntaxKind.StarToken:
                        return left * right;
                    case SyntaxKind.SlashToken:
                        return left / right;
                    default:
                        throw new Exception($"Unexpected binary operator {b.OperatorToken.Kind}");
                }
            }

            if (node is ParenthesizedExpressionSyntax p) return EvaluateExpression(p.Expression);

            throw new Exception($"Unexpected node {node.Kind}");
        }
    }
}
