namespace GenericDecisions.DecisionTable
{
    using System.Collections.Generic;

    public class DecisionRule<T>
    {
        public List<Condition<T>> Conditions { get; set; }
        public DecisionAction<T> DecisionAction { get; set; }

        public DecisionRule(List<Condition<T>> conditions, DecisionAction<T> action)
        {
            Conditions = conditions;
            DecisionAction = action;
        }

        public bool Evaluate(T context)
        {
            foreach (var condition in Conditions)
            {
                if (!condition.Predicate(context))
                {
                    return false;
                }
            }
            return true;
        }

        public void Execute(T context)
        {
            DecisionAction.Execute(context);
        }
    }
}