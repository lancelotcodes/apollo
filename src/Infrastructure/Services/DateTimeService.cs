using apollo.Application.Common.Interfaces;
using System;

namespace apollo.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
