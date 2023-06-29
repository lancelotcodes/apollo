using System;

namespace apollo.Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTimeOffset Now { get; }
    }
}
