namespace custom_compiler_tutorial.BindingStage
{
    public sealed class BoundLiteralExpression : BoundExpression
    {
        public override Type Type => Value.GetType();

        public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;
        public object Value { get; }

        public BoundLiteralExpression(object value)
        {
            Value = value;
        }
    }
}
