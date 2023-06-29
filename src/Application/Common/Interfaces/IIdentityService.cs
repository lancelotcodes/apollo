using apollo.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apollo.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<LoginResult> GetToken(string email, string password);

        Task<string> GetUserNameAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<Result> CreateUserAsync(string userName, string email, string firstName, string lastName, string role);
        Task<Result> DeleteUserAsync(string userId);
        Task<LoginResult> RefreshToken(string userId, string refreshToken);
        Task<bool> DeleteRefreshToken(string userId, string refreshToken);
        Task<string> GenerateRefreshToken(string userId);
        Task<GenerateResetPasswordResult> GetPasswordResetToken(string email);
        Task<UserProfileDTO> GetUserProfile(string email);
        Task<UserProfileDTO> GetUserProfileById(string userId);
        Task<Result> ChangePassword(string email, string currentPassword, string newPassword);
        Task<LoginResult> GoogleLogin(string firstname, string lastname, string email);
        Task<IEnumerable<UserProfileDTO>> GetUsersInList(List<string> emails);
        Task<bool> CreateUserRole(string role);
        Task<bool> AssignToRole(string userId, string role);
        Task<PaginatedList<UserProfileDTO>> GetAllUsers(string roleFilter, string filter, int pageNumber, int pageSize);
        Task<IEnumerable<UserProfileDTO>> GetUsersInListByUserId(List<string> userIds);
        Task<Result> ResetPassword(string userId, string password, string token);
        Task<Result> LockAccount(string email, DateTimeOffset? lockUntil);
        Task<Result> UnlockAccount(string email);
        Task<IEnumerable<UserProfileBaseDTO>> GetAllUsers(string roleFilter, string filter);
    }
}
