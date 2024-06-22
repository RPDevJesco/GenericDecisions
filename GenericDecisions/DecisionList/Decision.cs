namespace GenericDecisions.DecisionList
{
    public class Decision<T>
    {
        public string Name { get; set; }
        public T Value { get; set; }
        public int Priority { get; set; }

        public Decision(string name, T value, int priority)
        {
            Name = name;
            Value = value;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"{Name}: {Value} (Priority: {Priority})";
        }
    }
}