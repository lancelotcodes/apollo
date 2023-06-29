namespace apollo.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Email { get; }
        string UserId { get; }
    }
}
