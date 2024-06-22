namespace GenericDecisions.DecisionTree
{
    using DecisionTable;
    using System.Collections.Generic;

    public class TreeNode<T>
    {
        public Condition<T> Condition { get; set; }
        public DecisionAction<T> Action { get; set; }
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();

        public TreeNode(Condition<T> condition)
        {
            Condition = condition;
        }

        public TreeNode(DecisionAction<T> action)
        {
            Action = action;
        }

        public bool IsLeaf => Action != null;

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
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
                // If no condition is met and there are children, traverse the first child
                if (!conditionMet && Children.Count > 0)
                {
                    Children[0].Traverse(context);
                }
            }
        }
    }
}