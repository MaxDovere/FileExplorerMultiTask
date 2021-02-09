using System;

using System.Collections.Generic;

#nullable disable

namespace FileExplorerMultiTask.Models
{
    public partial class ExplorerObjects
    {
        public int Id { get; set; }
        public Guid Keyid { get; set; }
        public string Fullpath { get; set; }
        public string Drive { get; set; }
        public string Name { get; set; }
        public int IsDirectory { get; set; }
        public long Length{ get; set; }
        public string Attributes { get; set; }
        public string DirectoryName { get; set; }
        public string Extension { get; set; }
        public DateTime? FileCreationTime { get; set; }
        public DateTime? FileLastAccessTime { get; set; }
        public DateTime? FileLastWriteTime { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModifierTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public override string ToString()
        {
            return this.GetType().Name;
        }

    }
}
