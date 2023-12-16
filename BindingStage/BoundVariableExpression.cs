using custom_compiler_tutorial.CompilationStage;

namespace custom_compiler_tutorial.BindingStage
{
    public sealed class BoundVariableExpression : BoundExpression
    {
        public override Type Type => Variable.Type;
        public override BoundNodeKind Kind => BoundNodeKind.VariableExpression;

        public VariableSymbol Variable { get; }

        public BoundVariableExpression(VariableSymbol variable)
        {
            Variable = variable;
        }
    }
}
