using System.ComponentModel;

namespace Shared.Pagination
{
    public abstract class ListQueryBase
    {

        [DefaultValue(1)]
        public int PageNumber { get; set; }

        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}
