using System;

using System.Collections.Generic;

#nullable disable

namespace FileExplorerMultiTask.Models
{
    public partial class ExplorerTasks
    {
        public int Id { get; set; }
        public Guid Keyid { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public int ThreadNum { get; set; }
        public int NIterations { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? ModifierTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public int Actived { get; set; }
        public override string ToString()
        {
            return this.GetType().ToString().Substring(("FileExplorerMultiTask.Models.").Length, this.GetType().ToString().Length - ("FileExplorerMultiTask.Models.").Length); //"[dbo].[ExploperObject]"
        }
    }
}
