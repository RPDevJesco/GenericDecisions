namespace GenericDecisions.DecisionList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DecisionList<T>
    {
        private List<Decision<T>> decisions = new List<Decision<T>>();

        public void AddDecision(string name, T value, int priority)
        {
            var decision = new Decision<T>(name, value, priority);
            decisions.Add(decision);
        }

        public void RemoveDecision(string name)
        {
            decisions.RemoveAll(d => d.Name == name);
        }

        public Decision<T> GetHighestPriorityDecision()
        {
            return decisions.OrderByDescending(d => d.Priority).FirstOrDefault();
        }

        public void PrintDecisions()
        {
            foreach (var decision in decisions)
            {
                Console.WriteLine(decision);
            }
        }
    }
}