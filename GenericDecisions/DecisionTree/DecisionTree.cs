namespace GenericDecisions.DecisionTree
{
    public class DecisionTree<T>
    {
        public TreeNode<T> Root { get; set; }

        public DecisionTree(TreeNode<T> root)
        {
            Root = root;
        }

        public void EvaluateAndExecute(T context)
        {
            Root?.Traverse(context);
        }
    }
}