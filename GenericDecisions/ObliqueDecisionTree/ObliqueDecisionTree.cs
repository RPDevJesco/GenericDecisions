namespace GenericDecisions.ObliqueDecisionTree
{
    public class ObliqueDecisionTree<T>
    {
        public ObliqueTreeNode<T> Root { get; set; }

        public ObliqueDecisionTree(ObliqueTreeNode<T> root)
        {
            Root = root;
        }

        public void EvaluateAndExecute(T context)
        {
            Root?.Traverse(context);
        }
    }
}