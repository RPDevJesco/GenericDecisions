namespace GenericDecisions.DecisionTable
{
    using System.Collections.Generic;

    public class DecisionTable<T>
    {
        private List<DecisionRule<T>> rules = new List<DecisionRule<T>>();

        public void AddRule(List<Condition<T>> conditions, DecisionAction<T> action)
        {
            var rule = new DecisionRule<T>(conditions, action);
            rules.Add(rule);
        }

        public void EvaluateAndExecute(T context)
        {
            foreach (var rule in rules)
            {
                if (rule.Evaluate(context))
                {
                    rule.Execute(context);
                    break;
                }
            }
        }
    }
}