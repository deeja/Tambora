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

    public partial class Main : Form
    {
        IPackageExploder packageExploder = new DummyPackageExploder();


        public Main()
        {
            this.InitializeComponent();
            this.packageExploder.ProcessingStarted += (s, e) => this.Invoke(() => this.ShowProcessing(e.Filename));
            this.packageExploder.ProcessingFinished += (s, e) => this.Invoke(this.HideProcessing);
            this.packageExploder.PackageExploded += (sender, args) => this.Invoke(() => this.HandleExplodedPackage(args)) ;

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
            TreeNode itemNode = new TreeNode("Items")
                                    {
                                        Checked = true
                                    };
            AddItemAndChildren(args.Items, itemNode);
            treeView1.Nodes.Add(itemNode);
        }

        private static void AddItemAndChildren(PackageItem[] items, TreeNode itemNode)
        {
            foreach (var item in items)
            {
                TreeNode next = new TreeNode(item.Name)
                                    {
                                        Tag = item
                                    };
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
    }
}
