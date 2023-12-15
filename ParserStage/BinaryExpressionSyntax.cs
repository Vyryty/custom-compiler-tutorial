﻿using custom_compiler_tutorial.LexerStage;

namespace custom_compiler_tutorial.ParserStage
{
    sealed class BinaryExpressionSyntax : ExpressionSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;
        public ExpressionSyntax Left { get; }
        public SyntaxNode OperatorToken { get; }
        public ExpressionSyntax Right { get; }

        public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}