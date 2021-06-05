using System;

namespace BookStore.Application.Services
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
