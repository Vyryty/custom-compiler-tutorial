using custom_compiler_tutorial.CompilationStage;

namespace custom_compiler_tutorial.BindingStage
{
    public sealed class BoundAssignmentExpression : BoundExpression
    {
        public override Type Type => Expression.Type;
        public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;
        public VariableSymbol Variable { get; }
        public BoundExpression Expression { get; }

        public BoundAssignmentExpression(VariableSymbol variable, BoundExpression expression)
        {
            Variable = variable;
            Expression = expression;
        }
    }
}
