using System;

namespace Editor_Prototype.Util
{
    public class RegionAddedEventArgs : EventArgs
    {
        public RegionAddedEventArgs(DocumentRegion region)
        {
            this.Region = region;
        }

        public DocumentRegion Region { get; private set; }
    }
}