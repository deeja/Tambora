namespace Tambora
{
    using System;

    public class PackageExplodedArgs: EventArgs
    {
        public PackageItem[] Items { get; set; }
    }

}