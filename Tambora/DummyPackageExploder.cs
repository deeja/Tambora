namespace Tambora
{
    using System;
    using System.ComponentModel;
    using System.Security.Cryptography;
    using System.Threading;

    using Tambora.PackageExploder;

    public sealed class DummyPackageExploder : IPackageExploder
    {
        public event EventHandler<PackageExplodedArgs> PackageExploded;

        public event EventHandler<PackageProcessingArgs> ProcessingStarted;
        public event EventHandler ProcessingFinished;

        public void ExplodePackage(string fileName)
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.OnProcessingStarted(fileName);
            worker.DoWork += this.OnWorkerOnDoWork;
            worker.RunWorkerAsync();
        }

        private void OnWorkerOnDoWork(object sender, DoWorkEventArgs args)
        {
            Thread.Sleep(4000);
            this.OnProcessingFinished();
            PackageItem item = null;

            for (int i = 0; i < 5; i++)
            {
                var lastItem = item;
                item = new PackageItem()
                {
                    Name = $"package item {i}",
                    Items = item == null ? new PackageItem[0] : new[] { lastItem }
                };
            }

            this.OnPackageExploded(new PackageExplodedArgs()
            {
                Items = new[] { item }
            });

        }

        private void OnPackageExploded(PackageExplodedArgs e)
        {
            this.PackageExploded?.Invoke(this, e);
        }

        private void OnProcessingStarted(string filename)
        {
            this.ProcessingStarted?.Invoke(this, new PackageProcessingArgs() { Filename = filename });
        }

        private void OnProcessingFinished()
        {
            this.ProcessingFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}