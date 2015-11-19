namespace RecursiveDescentDemo.Implementation.Nodes
{
    public class ConstantExpression: Expression
    {

        public int Value { get; }

        public ConstantExpression(int value)
        {
            this.Value = value;
        }

    }
}
