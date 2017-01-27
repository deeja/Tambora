using System;
using System.Windows.Forms;

namespace Tambora
{
    using Tambora.PackageExploder;
    using Tambora.Treeview;

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
            TreeViewHelpers.SetupTreeViewWithPackage(args, treeView1);
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

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeViewHelpers.UpdateCurrentAndChildNodes(e.Node);
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            var parent = e.Node.Parent;
            e.Cancel = parent != null && !parent.Checked;
        }
    }
}