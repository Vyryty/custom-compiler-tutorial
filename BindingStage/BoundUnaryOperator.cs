﻿using custom_compiler_tutorial.LexerStage;

namespace custom_compiler_tutorial.BindingStage
{
    public sealed class BoundUnaryOperator
    {
        public SyntaxKind SyntaxKind { get; }
        public BoundUnaryOperatorKind Kind { get; }
        public Type OperandType { get; }
        public Type Type { get; }

        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType, Type resultType)
        {
            SyntaxKind = syntaxKind;
            Kind = kind;
            OperandType = operandType;
            Type = resultType;
        }

        private BoundUnaryOperator(SyntaxKind syntaxKind, BoundUnaryOperatorKind kind, Type operandType): this(syntaxKind, kind, operandType, operandType) {}

        private static BoundUnaryOperator[] operators =
        {
            new(SyntaxKind.BangToken, BoundUnaryOperatorKind.LogicalNegation, typeof(bool)),

            new(SyntaxKind.PlusToken, BoundUnaryOperatorKind.Identity, typeof(int)),
            new(SyntaxKind.MinusToken, BoundUnaryOperatorKind.Negation, typeof(int))
        };

        public static BoundUnaryOperator Bind(SyntaxKind syntaxKind, Type operandType)
        {
            foreach (BoundUnaryOperator op in operators)
            {
                if (op.SyntaxKind == syntaxKind && op.OperandType == operandType) return op;
            }

            return null;
        }
    }
}
