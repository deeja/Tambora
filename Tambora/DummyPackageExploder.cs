namespace Tambora
{
    using System;
    using System.ComponentModel;
    using System.Security.Cryptography;
    using System.Threading;
    using System.Threading.Tasks;

    using Tambora.PackageExploder;

    public sealed class DummyPackageExploder : IPackageExploder
    {
        public event EventHandler<PackageProcessingArgs> ProcessingStarted;
        public event EventHandler ProcessingFinished;

        public async Task<PackageItem[]> ExplodePackage(string fileName)
        {
            OnProcessingStarted(fileName);

            Thread.Sleep(4000);
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
            this.OnProcessingFinished();
            return new[] { item };
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