namespace GenericDecisions.ObliqueDecisionTree
{
    using DecisionTable;
    using System.Collections.Generic;

    public class ObliqueTreeNode<T>
    {
        public Condition<T> Condition { get; set; }
        public DecisionAction<T> Action { get; set; }
        public List<ObliqueTreeNode<T>> Children { get; set; } = new List<ObliqueTreeNode<T>>();
        public ObliqueTreeNode<T> DefaultChild { get; set; }

        public ObliqueTreeNode(Condition<T> condition)
        {
            Condition = condition;
        }

        public ObliqueTreeNode(DecisionAction<T> action)
        {
            Action = action;
        }

        public bool IsLeaf => Action != null;

        public void AddChild(ObliqueTreeNode<T> child)
        {
            Children.Add(child);
        }

        public void SetDefaultChild(ObliqueTreeNode<T> child)
        {
            DefaultChild = child;
        }

        public void Traverse(T context)
        {
            if (IsLeaf)
            {
                Action?.Execute(context);
            }
            else
            {
                bool conditionMet = false;
                foreach (var child in Children)
                {
                    if (child.Condition?.Predicate(context) == true)
                    {
                        child.Traverse(context);
                        conditionMet = true;
                        break;
                    }
                }
                // If no condition is met, traverse the default child if it exists
                if (!conditionMet && DefaultChild != null)
                {
                    DefaultChild.Traverse(context);
                }
            }
        }
    }
}