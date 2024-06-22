namespace GenericDecisions.DecisionTable
{
    public class DecisionAction<T>
    {
        public string Name { get; set; }
        public Action<T> Execute { get; set; }

        public DecisionAction(string name, Action<T> execute)
        {
            Name = name;
            Execute = execute;
        }
    }
}