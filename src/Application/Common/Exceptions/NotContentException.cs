using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.Common.Exceptions
{
    public class NoContentException : Exception
    {
        public NoContentException(string entity) : base($"Entity \"{entity}\" has no records.")
        {
        }
    }
}
