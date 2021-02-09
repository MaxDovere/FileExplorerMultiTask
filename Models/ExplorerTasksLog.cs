using System;

using System.Collections.Generic;

#nullable disable

namespace FileExplorerMultiTask.Models
{
    public partial class ExplorerTasksLog
    {
        public int Id { get; set; }
        public Guid Keyid { get; set; }
        public int HResult { get; set; }
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModifierTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public override string ToString()
        {
            return this.GetType().Name; 
        }
    }
}
