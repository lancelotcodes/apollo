using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Application.Common.Exceptions
{
    public class UnauthorizedUserException : Exception
    {
        public UnauthorizedUserException(string name)
            : base($"Unauthorized user:{name}")
        {
        }
    }
}
