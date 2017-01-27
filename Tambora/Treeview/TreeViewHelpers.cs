namespace Tambora.Treeview
{
    using System.Drawing;
    using System.Windows.Forms;

    using Tambora.PackageExploder;

    public static class TreeViewHelpers
    {
        public static void AddItemAndChildren(PackageItem[] items, TreeNode itemNode)
        {
            foreach (var item in items)
            {
                TreeNode next = new TreeNode(item.Name)
                                    {
                                        Tag = item,
                                        Checked = true,
                                        BackColor = EnabledColour
                                    };

                AddItemAndChildren(item.Items, next);
                itemNode.Nodes.Add(next);
            }
        }

        public static readonly Color DisabledColour = Color.Orange;

        public static readonly Color EnabledColour = Color.Azure;

        public static void SetChildrenState(TreeNodeCollection nodes, bool enabled)
        {
            foreach (TreeNode node in nodes)
            {
                var state = enabled && node.Checked;
                SetColourState(node, state);
                SetChildrenState(node.Nodes, state);
            }
        }

        public static void SetColourState(TreeNode treeNode, bool enabled)
        {
            treeNode.BackColor = enabled ? EnabledColour : DisabledColour;
        }

        public static void SetupTreeViewWithPackage(PackageItem[] packageItems, TreeView treeView)
        {
            treeView.Nodes.Clear();
            TreeNode itemNode = new TreeNode("Items") { Checked = true };
            AddItemAndChildren(packageItems, itemNode);
            treeView.Nodes.Add(itemNode);
            treeView.ExpandAll();
        }

        public static void UpdateCurrentAndChildNodes(TreeNode node)
        {
            bool state;
            if (node.Checked)
            {
                var currentParent = node.Parent;
                while (true)
                {
                    if (currentParent == null)
                    {
                        state = true;
                        break;
                    }

                    if (currentParent.Checked)
                    {
                        currentParent = currentParent.Parent;
                        continue;
                    }

                    state = false;
                    break;
                }
            }
            else
            {
                state = false;
            }

            TreeViewHelpers.SetColourState(node, state);
            TreeViewHelpers.SetChildrenState(node.Nodes, state);
        }
    }
}