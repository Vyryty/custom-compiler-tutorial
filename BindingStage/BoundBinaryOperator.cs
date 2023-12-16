using custom_compiler_tutorial.LexerStage;

namespace custom_compiler_tutorial.BindingStage
{
    public sealed class BoundBinaryOperator
    {
        public SyntaxKind SyntaxKind { get; }
        public BoundBinaryOperatorKind Kind { get; }
        public Type LeftType { get; }
        public Type RightType { get; }
        public Type Type { get; }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType, Type rightType, Type resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            LeftType = leftType;
            RightType = rightType;
            Type = resultType;
        }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type)
            : this(syntaxKind, kind, type, type, type) { }

        private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type operandType, Type resultType)
            : this(syntaxKind, kind, operandType, operandType, resultType) { }

        private static BoundBinaryOperator[] operators =
        {
            new(SyntaxKind.AmpersandAmpersandToken, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
            new(SyntaxKind.PipePipeToken, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),

            new(SyntaxKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
            new(SyntaxKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
            new(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
            new(SyntaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),
            new(SyntaxKind.BangEqualsToken, BoundBinaryOperatorKind.NotEquals, typeof(int), typeof(bool)),
            new(SyntaxKind.EqualsEqualsToken, BoundBinaryOperatorKind.Equals, typeof(int), typeof(bool)),
            new(SyntaxKind.BangEqualsToken, BoundBinaryOperatorKind.NotEquals, typeof(bool)),
            new(SyntaxKind.EqualsEqualsToken, BoundBinaryOperatorKind.Equals, typeof(bool)),
        };

        public static BoundBinaryOperator Bind(SyntaxKind syntaxKind, Type leftType, Type rightType)
        {
            foreach (BoundBinaryOperator op in operators)
            {
                if (op.SyntaxKind == syntaxKind && op.LeftType == leftType && op.RightType == rightType) return op;
            }

            return null;
        }
    }
}
