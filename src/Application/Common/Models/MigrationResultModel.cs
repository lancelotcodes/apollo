using System.Collections.Generic;

namespace apollo.Application.Common.Models
{
    public class MigrationResultModel
    {
        public bool Succeeded { get; set; }
        public int Uploaded { get; set; }
        public int Total { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
