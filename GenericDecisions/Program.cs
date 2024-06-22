using GenericDecisions.DecisionList;
using GenericDecisions.DecisionTable;
using GenericDecisions.DecisionTree;
using GenericDecisions.ObliqueDecisionTree;

namespace GenericDecisions
{
    public class Program
    {
        public static void Main()
        {
            DecisionListExample();
            Console.WriteLine("\n");
            DecisionTableExample();
            Console.WriteLine("\n");
            DecisionTreeExample();
            Console.WriteLine("\n");
            ObliqueDecisionTreeExample();
        }

        private static void DecisionListExample()
        {
            var decisionList = new DecisionList<string>();

            decisionList.AddDecision("Buy Groceries", "Buy milk and eggs", 2);
            decisionList.AddDecision("Complete Assignment", "Finish math assignment", 3);
            decisionList.AddDecision("Exercise", "Go for a run", 1);

            Console.WriteLine("All Decisions:");
            decisionList.PrintDecisions();

            var highestPriorityDecision = decisionList.GetHighestPriorityDecision();
            Console.WriteLine($"\nHighest Priority Decision: {highestPriorityDecision}");

            decisionList.RemoveDecision("Exercise");
            Console.WriteLine("\nDecisions after removing 'Exercise':");
            decisionList.PrintDecisions();
        }

        private static void DecisionTableExample()
        {
            Console.WriteLine("Starting DecisionTable: ");
            var decisionTable = new DecisionTable<Dictionary<string, object>>();

            var condition1 = new Condition<Dictionary<string, object>>(
                "Is adult", context => (int)context["age"] >= 18);
            var action1 = new DecisionAction<Dictionary<string, object>>(
                "Allow Entry", context => Console.WriteLine("Entry Allowed"));

            var condition2 = new Condition<Dictionary<string, object>>(
                "Is minor", context => (int)context["age"] < 18);
            var action2 = new DecisionAction<Dictionary<string, object>>(
                "Deny Entry", context => Console.WriteLine("Entry Denied"));

            decisionTable.AddRule(new List<Condition<Dictionary<string, object>>> { condition1 }, action1);
            decisionTable.AddRule(new List<Condition<Dictionary<string, object>>> { condition2 }, action2);

            var context = new Dictionary<string, object>
            {
                { "age", 20 }
            };

            decisionTable.EvaluateAndExecute(context);
            Console.WriteLine("End DecisionTable");
        }

        private static void DecisionTreeExample()
        {
            // Define conditions
            var conditionIsGoldMember = new Condition<Dictionary<string, object>>(
                "Is Gold Member", context => (string)context["membership"] == "Gold");
            var conditionIsSilverMember = new Condition<Dictionary<string, object>>(
                "Is Silver Member", context => (string)context["membership"] == "Silver");
            var conditionIsRegularMember = new Condition<Dictionary<string, object>>(
                "Is Regular Member", context => (string)context["membership"] == "Regular");
            var conditionPurchaseOver100 = new Condition<Dictionary<string, object>>(
                "Purchase over 100", context => (decimal)context["purchaseAmount"] > 100);
            var conditionPurchaseOver50 = new Condition<Dictionary<string, object>>(
                "Purchase over 50", context => (decimal)context["purchaseAmount"] > 50);

            // Define actions
            var actionGoldDiscount = new DecisionAction<Dictionary<string, object>>(
                "Gold Discount", context => Console.WriteLine("Gold Member: 20% Discount"));
            var actionSilverDiscountHigh = new DecisionAction<Dictionary<string, object>>(
                "Silver Discount High", context => Console.WriteLine("Silver Member: 15% Discount"));
            var actionSilverDiscountLow = new DecisionAction<Dictionary<string, object>>(
                "Silver Discount Low", context => Console.WriteLine("Silver Member: 10% Discount"));
            var actionRegularDiscountHigh = new DecisionAction<Dictionary<string, object>>(
                "Regular Discount High", context => Console.WriteLine("Regular Member: 5% Discount"));
            var actionNoDiscount = new DecisionAction<Dictionary<string, object>>(
                "No Discount", context => Console.WriteLine("No Discount"));

            // Build the tree
            var root = new TreeNode<Dictionary<string, object>>(conditionIsGoldMember);
            var silverNode = new TreeNode<Dictionary<string, object>>(conditionIsSilverMember);
            var regularNode = new TreeNode<Dictionary<string, object>>(conditionIsRegularMember);
            var purchaseOver100NodeSilver = new TreeNode<Dictionary<string, object>>(conditionPurchaseOver100);
            var purchaseOver50NodeSilver = new TreeNode<Dictionary<string, object>>(conditionPurchaseOver50);
            var purchaseOver100NodeRegular = new TreeNode<Dictionary<string, object>>(conditionPurchaseOver100);

            root.AddChild(new TreeNode<Dictionary<string, object>>(actionGoldDiscount));
            root.AddChild(silverNode);
            root.AddChild(regularNode);

            silverNode.AddChild(purchaseOver100NodeSilver);
            silverNode.AddChild(purchaseOver50NodeSilver);
            silverNode.AddChild(new TreeNode<Dictionary<string, object>>(actionNoDiscount));

            purchaseOver100NodeSilver.AddChild(new TreeNode<Dictionary<string, object>>(actionSilverDiscountHigh));
            purchaseOver50NodeSilver.AddChild(new TreeNode<Dictionary<string, object>>(actionSilverDiscountLow));

            regularNode.AddChild(purchaseOver100NodeRegular);
            regularNode.AddChild(new TreeNode<Dictionary<string, object>>(actionRegularDiscountHigh));
            regularNode.AddChild(new TreeNode<Dictionary<string, object>>(actionNoDiscount));

            purchaseOver100NodeRegular.AddChild(new TreeNode<Dictionary<string, object>>(actionRegularDiscountHigh));
            purchaseOver100NodeRegular.AddChild(new TreeNode<Dictionary<string, object>>(actionNoDiscount));

            var decisionTree = new DecisionTree<Dictionary<string, object>>(root);

            // Evaluate context
            var context = new Dictionary<string, object>
            {
                { "membership", "Gold" },
                { "purchaseAmount", 150m }
            };

            decisionTree.EvaluateAndExecute(context);

            context["membership"] = "Silver";
            context["purchaseAmount"] = 120m;
            decisionTree.EvaluateAndExecute(context);

            context["membership"] = "Regular";
            context["purchaseAmount"] = 80m;
            decisionTree.EvaluateAndExecute(context);

            context["membership"] = "Regular";
            context["purchaseAmount"] = 30m;
            decisionTree.EvaluateAndExecute(context);
        }
        
        private static void ObliqueDecisionTreeExample()
        {
            // Define complex conditions
            var conditionIsLoyalCustomer = new Condition<Dictionary<string, object>>(
                "Is Loyal Customer", context => (bool)context["isLoyalCustomer"]);
            var conditionHighSpender = new Condition<Dictionary<string, object>>(
                "High Spender", context => (decimal)context["totalSpent"] > 1000);
            var conditionHasCoupon = new Condition<Dictionary<string, object>>(
                "Has Coupon", context => (bool)context["hasCoupon"]);
            var conditionHolidaySeason = new Condition<Dictionary<string, object>>(
                "Holiday Season", context => (bool)context["isHolidaySeason"]);

            // Define actions
            var actionLoyalCustomerDiscount = new DecisionAction<Dictionary<string, object>>(
                "Loyal Customer Discount", context => Console.WriteLine("Loyal Customer: 20% Discount"));
            var actionHighSpenderDiscount = new DecisionAction<Dictionary<string, object>>(
                "High Spender Discount", context => Console.WriteLine("High Spender: 15% Discount"));
            var actionCouponDiscount = new DecisionAction<Dictionary<string, object>>(
                "Coupon Discount", context => Console.WriteLine("Coupon Applied: 10% Discount"));
            var actionHolidayDiscount = new DecisionAction<Dictionary<string, object>>(
                "Holiday Discount", context => Console.WriteLine("Holiday Season: 5% Discount"));
            var actionNoDiscount = new DecisionAction<Dictionary<string, object>>(
                "No Discount", context => Console.WriteLine("No Discount Available"));

            // Build the tree
            var root = new ObliqueTreeNode<Dictionary<string, object>>(conditionIsLoyalCustomer);
            var highSpenderNode = new ObliqueTreeNode<Dictionary<string, object>>(conditionHighSpender);
            var couponNode = new ObliqueTreeNode<Dictionary<string, object>>(conditionHasCoupon);
            var holidaySeasonNode = new ObliqueTreeNode<Dictionary<string, object>>(conditionHolidaySeason);
            var defaultNode = new ObliqueTreeNode<Dictionary<string, object>>(actionNoDiscount);

            root.AddChild(new ObliqueTreeNode<Dictionary<string, object>>(actionLoyalCustomerDiscount));
            root.AddChild(highSpenderNode);
            root.AddChild(couponNode);
            root.AddChild(holidaySeasonNode);
            root.SetDefaultChild(defaultNode);

            highSpenderNode.AddChild(new ObliqueTreeNode<Dictionary<string, object>>(actionHighSpenderDiscount));
            couponNode.AddChild(new ObliqueTreeNode<Dictionary<string, object>>(actionCouponDiscount));
            holidaySeasonNode.AddChild(new ObliqueTreeNode<Dictionary<string, object>>(actionHolidayDiscount));

            var obliqueDecisionTree = new ObliqueDecisionTree<Dictionary<string, object>>(root);

            // Evaluate context
            var context = new Dictionary<string, object>
            {
                { "isLoyalCustomer", true },
                { "totalSpent", 1500m },
                { "hasCoupon", false },
                { "isHolidaySeason", false }
            };

            obliqueDecisionTree.EvaluateAndExecute(context);

            context["isLoyalCustomer"] = false;
            context["totalSpent"] = 500m;
            context["hasCoupon"] = true;
            obliqueDecisionTree.EvaluateAndExecute(context);

            context["isLoyalCustomer"] = false;
            context["totalSpent"] = 200m;
            context["hasCoupon"] = false;
            context["isHolidaySeason"] = true;
            obliqueDecisionTree.EvaluateAndExecute(context);

            context["isLoyalCustomer"] = false;
            context["totalSpent"] = 200m;
            context["hasCoupon"] = false;
            context["isHolidaySeason"] = false;
            obliqueDecisionTree.EvaluateAndExecute(context);
        }
    }
}