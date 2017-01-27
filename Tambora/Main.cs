using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tambora
{
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public partial class Main : Form
    {
        private IPackageExploder packageExploder = new DummyPackageExploder();


        public Main()
        {
            this.InitializeComponent();
            this.packageExploder.ProcessingStarted += (s, e) => this.Invoke(() => this.ShowProcessing(e.Filename));
            this.packageExploder.ProcessingFinished += (s, e) => this.Invoke(this.HideProcessing);
            this.packageExploder.PackageExploded +=
                (sender, args) => this.Invoke(() => this.HandleExplodedPackage(args));
        }

        private void HideProcessing()
        {
            this.toolStripProgressBar1.Style = ProgressBarStyle.Continuous;
            this.toolStripStatusLabel1.Text = "";
        }

        private void ShowProcessing(string filename)
        {
            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            this.toolStripStatusLabel1.Text = $"Loading {filename}";
        }

        private void Invoke(Action methodInvoker)
        {
            ((Control)this).Invoke(methodInvoker);
        }

        private void HandleExplodedPackage(PackageExplodedArgs args)
        {
            treeView1.Nodes.Clear();
            TreeNode itemNode = new TreeNode("Items") { Checked = true };
            AddItemAndChildren(args.Items, itemNode);
            treeView1.Nodes.Add(itemNode);
            treeView1.ExpandAll();
        }

        private static void AddItemAndChildren(PackageItem[] items, TreeNode itemNode)
        {
            foreach (var item in items)
            {
                TreeNode next = new TreeNode(item.Name) { Tag = item, };
                AddItemAndChildren(item.Items, next);
                itemNode.Nodes.Add(next);
            }
        }

        private void about_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.packageExploder.ExplodePackage(this.openFileDialog1.SafeFileName);
            }
        }

        private void treeView1_ParentChanged(object sender, EventArgs e)
        {
            TreeNode o = sender as TreeNode;
        }


        Color disabledColour = Color.Orange;
        Color enabledColour = Color.Azure;

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            var treeNode = e.Node;
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = treeNode.Checked;
            }

            if (!treeNode.Checked)
            {
                SetDisabledColour(treeNode);
                return;
            }

            var currentParent = e.Node.Parent;
            while (true)
            {
                if (currentParent == null)
                {
                    this.SetEnabledColor(treeNode);
                    break;
                }

                if (currentParent.Checked)
                {
                    currentParent = currentParent.Parent;
                    continue;
                }

                this.SetDisabledColour(treeNode);
                break;
            }


        }

        private void SetDisabledColour(TreeNode treeNode)
        {
            treeNode.BackColor = this.disabledColour;
        }

        private void SetEnabledColor(TreeNode treeNode)
        {
            treeNode.BackColor = this.enabledColour;
        }
    }
}
