namespace GenericDecisions.DecisionTable
{
    public class Condition<T>
    {
        public string Name { get; set; }
        public Func<T, bool> Predicate { get; set; }

        public Condition(string name, Func<T, bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}