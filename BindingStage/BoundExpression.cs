namespace custom_compiler_tutorial.BindingStage
{
    public abstract class BoundExpression : BoundNode
    {
        public abstract Type Type { get; }
    }
}
