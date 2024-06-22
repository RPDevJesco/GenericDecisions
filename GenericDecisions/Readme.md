DecisionList

A DecisionList is a simple structure that maintains a list of decisions, each with an associated priority. The key features include:

    Adding Decisions: New decisions can be added to the list with a specified priority.
    Removing Decisions: Decisions can be removed from the list by their name.
    Retrieving Highest Priority Decision: The decision with the highest priority can be retrieved.
    Printing All Decisions: All decisions in the list can be printed.

Example:
```csharp
var decisionList = new DecisionList<string>();
decisionList.AddDecision("Buy Groceries", "Buy milk and eggs", 2);
decisionList.AddDecision("Complete Assignment", "Finish math assignment", 3);
decisionList.AddDecision("Exercise", "Go for a run", 1);
```

DecisionTable

A DecisionTable is a more structured way to represent a set of rules where each rule consists of conditions and an associated action. The key features include:

    Adding Rules: Rules are added to the table with a list of conditions and an action.
    Evaluating Context: The table evaluates a given context against its rules and executes the action of the first matching rule.

Example:
```csharp
var decisionTable = new DecisionTable<Dictionary<string, object>>();
decisionTable.AddRule(
    new List<Condition<Dictionary<string, object>>> { new Condition<Dictionary<string, object>>("Is adult", context => (int)context["age"] >= 18) },
    new DecisionAction<Dictionary<string, object>>("Allow Entry", context => Console.WriteLine("Entry Allowed"))
);
```

DecisionTree

A DecisionTree is a hierarchical structure where each node represents a decision point with conditions and corresponding actions. The tree allows traversal through nodes based on the evaluation of conditions until a leaf node (final action) is reached. The key features include:

    Nodes with Conditions and Actions: Nodes can represent conditions (internal nodes) or actions (leaf nodes).
    Traversal Based on Conditions: The tree is traversed by evaluating conditions at each node.

Example:

```csharp
var root = new TreeNode<Dictionary<string, object>>(new Condition<Dictionary<string, object>>("Is Gold Member", context => (string)context["membership"] == "Gold"));
root.AddChild(new TreeNode<Dictionary<string, object>>(new DecisionAction<Dictionary<string, object>>("Gold Discount", context => Console.WriteLine("Gold Member: 20% Discount"))));
```

ObliqueDecisionTree

An ObliqueDecisionTree is similar to a DecisionTree but supports more complex decision-making scenarios where decisions are not strictly binary. It can have multiple branches and can handle more complex conditions. The key features include:

    Complex Conditions: Supports more complex conditions for branching.
    Multiple Branches: Nodes can have multiple children representing different branches.
    Default Handling: Provides a mechanism to handle cases where no condition matches.

Example:
```csharp
var root = new ObliqueTreeNode<Dictionary<string, object>>(new Condition<Dictionary<string, object>>("Is Loyal Customer", context => (bool)context["isLoyalCustomer"]));
root.AddChild(new ObliqueTreeNode<Dictionary<string, object>>(new DecisionAction<Dictionary<string, object>>("Loyal Customer Discount", context => Console.WriteLine("Loyal Customer: 20% Discount"))));
root.SetDefaultChild(new ObliqueTreeNode<Dictionary<string, object>>(new DecisionAction<Dictionary<string, object>>("No Discount", context => Console.WriteLine("No Discount Available"))));
```

Differences

    Structure and Complexity:
        DecisionList: Simple list-based structure, suitable for linear decision-making based on priorities.
        DecisionTable: Tabular structure, suitable for rule-based systems where each rule has multiple conditions and an action.
        DecisionTree: Hierarchical tree structure, suitable for scenarios where decisions are made through a sequence of conditions.
        ObliqueDecisionTree: Enhanced tree structure, suitable for complex decision-making scenarios with multiple branches and complex conditions.

    Use Cases:
        DecisionList: Best for simple, linear prioritization tasks.
        DecisionTable: Best for rule-based decision-making with clear, distinct rules.
        DecisionTree: Best for hierarchical decision processes where each decision leads to another decision point.
        ObliqueDecisionTree: Best for complex scenarios where decisions are not strictly binary and require handling multiple conditions and branches.

    Decision Evaluation:
        DecisionList: Evaluates based on priority.
        DecisionTable: Evaluates all conditions in the rules to find a match.
        DecisionTree: Traverses the tree based on conditions until a leaf node is reached.
        ObliqueDecisionTree: Traverses the tree based on complex conditions and handles default actions if no conditions match.