using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Mappings;
using apollo.Application.Common.Models;
using apollo.Infrastructure.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NinjaNye.SearchExtensions;
using Shared.Domain.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _authContext;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IConfiguration configuration,
            ApplicationDbContext authContext,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _authContext = authContext;
            _configuration = configuration;
            _emailService = emailService;
        }

        const string SENDER = "olivia.tan@kmcmaggroup.com";
        const string SENDER_NAME = "KMC Savills";
        const string NEW_INVESTOR_ACCOUNT = "d-1301ef943fe540ef84ab34d061483ea0";
        const string ADMIN_ACCOUNT = "d-aa7c8d219d1d45858c7ea2986e81594b";

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Email == userId);

            return user.UserName;
        }

        public async Task<IEnumerable<UserProfileDTO>> GetUsersInList(List<string> emails)
        {
            var users = _userManager.Users.Search(i => i.Email.ToLower()).Containing(emails.ToArray());
            var userProfiles = await users.Select(u => new UserProfileDTO
            {
                Id = u.Id,
                ProfilePicture = u.ProfilePicture,
                FirstName = !string.IsNullOrEmpty(u.FirstName) ? u.FirstName : "Unknown",
                LastName = !string.IsNullOrEmpty(u.LastName) ? u.LastName : "Unknown",
                Email = u.Email
            }).ToListAsync();

            return userProfiles;
        }

        public async Task<IEnumerable<UserProfileDTO>> GetUsersInListByUserId(List<string> userIds)
        {
            var users = _userManager.Users.Search(i => i.Id.ToLower()).Containing(userIds.ToArray());

            var userProfiles = await users.Select(u => new UserProfileDTO
            {
                Id = u.Id,
                ProfilePicture = u.ProfilePicture,
                FirstName = !string.IsNullOrEmpty(u.FirstName) ? u.FirstName : "Unknown",
                LastName = !string.IsNullOrEmpty(u.LastName) ? u.LastName : "Unknown",
                Email = u.Email
            }).ToListAsync();

            return userProfiles;
        }

        public async Task<Result> CreateUserAsync(string userName, string email, string firstName, string lastName, string role)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
                FirstName = firstName,
                LastName = lastName
            };

            Random rnd = new Random();

            var randomNumber = rnd.Next(1000, 9999);

            var randomPassword = $"wH!sT3@{randomNumber}";

            var result = await _userManager.CreateAsync(user, randomPassword);

            //add role
            if (result.Succeeded)
            {
                await AssignToRole(user.Id, role);

                EmailContentModel emailModel = new EmailContentModel
                {
                    FromEmail = SENDER,
                    FromName = SENDER_NAME,
                    ToEmail = $"{user.Email}",
                    ToName = $"{user.FirstName} {user.LastName}",
                    TemplateId = "",
                    DynamicObject = new
                    {
                        FirstName = user.FirstName,
                        ClientAppUrl = $"{_configuration["ClientAppUrl"]}",
                        DefaultPassword = randomPassword
                    }
                };

                //send email notification
                if (role == "Investor")
                {
                    emailModel.TemplateId = NEW_INVESTOR_ACCOUNT;
                }
                else
                {
                    emailModel.TemplateId = ADMIN_ACCOUNT;
                }

                BackgroundJob.Enqueue(() => _emailService.SendEmail(emailModel));
            }
            else
            {
                user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    await AssignToRole(user.Id, role);
                }
            }

            return result.ToApplicationResult();
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<LoginResult> GetToken(string email, string password)
        {
            List<Claim> claims = new List<Claim>();

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(email);
            }

            string token = string.Empty;

            if (user == null) return null;

            var canLogin = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (canLogin.Succeeded)
            {
                token = CreateToken(user, out claims);
            }
            else
            {
                throw new GenericException("Email or Password is incorrect");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var userProfile = await GetUserProfile(user.Email);

            return new LoginResult
            {
                Token = token,
                User = userProfile,
                Roles = userRoles.ToArray()
            };
        }


        public async Task<string> GenerateRefreshToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                //Create Refresh Token
                var refreshToken = new RefreshToken()
                {
                    UserId = user.Id,
                    AddedDate = DateTime.UtcNow,
                    ExpiryDate = DateTime.UtcNow.AddDays(7),
                    IsRevoked = false,
                    Token = RandomString(25)
                };

                await _authContext.AspNetRefreshTokens.AddAsync(refreshToken);
                await _authContext.SaveChangesAsync();


                return refreshToken.Token;
            }

            return String.Empty;
        }

        public async Task<LoginResult> RefreshToken(string userId, string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            List<Claim> claims = new List<Claim>();

            //check if refresh token is valid
            var validateRefreshToken = await _authContext.AspNetRefreshTokens.SingleOrDefaultAsync(i => i.UserId == userId && i.Token == refreshToken);
            if (validateRefreshToken == null)
                return null;

            var userExist = await _authContext.Users.SingleOrDefaultAsync(i => i.Id == userId);
            if (userExist == null)
                return null;

            var dateTimeNow = DateTime.UtcNow;

            if (!(validateRefreshToken.ExpiryDate > dateTimeNow && !validateRefreshToken.IsRevoked))
                return null;

            var user = await _userManager.FindByIdAsync(validateRefreshToken.UserId);

            var created_token = CreateToken(user, out claims);

            var userRoles = await _userManager.GetRolesAsync(user);


            //Remove refresh token
            _authContext.AspNetRefreshTokens.Remove(validateRefreshToken);
            await _authContext.SaveChangesAsync();


            var userProfile = await GetUserProfileById(userId);

            return new LoginResult
            {
                Token = created_token,
                User = userProfile,
                Roles = userRoles.ToArray()
            };
        }

        private string CreateToken(ApplicationUser user, out List<Claim> claims)
        {
            var roles = _userManager.GetRolesAsync(user).Result;

            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = _configuration["JWTSecretKey"];

            var key = Encoding.ASCII.GetBytes(secretKey);

            //create claims based on roles / and application permission
            claims = new List<Claim>();

            //add role-based claims
            claims.AddRange(RoleBaseClaims(user, roles.ToArray()).AsEnumerable());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(180D),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private List<Claim> RoleBaseClaims(ApplicationUser user, string[] roles)
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("UserId", user.Id),
                };

            if (roles.Any())
                claims.AddRange(roles.Select(a => new Claim(ClaimTypes.Role, a)));

            return claims;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<UserProfileDTO> GetUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToArray();

                var claims = await _userManager.GetClaimsAsync(user);

                return new UserProfileDTO
                {
                    Id = user.Id,
                    ProfilePicture = user.ProfilePicture,
                    FirstName = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName : "Unknown",
                    LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName : "Unknown",
                    Email = user.Email,
                    PhoneNumber = !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : "Unknown",
                    IsVerified = user.IsVerified,
                    IsLocked = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.Now ? true : false,
                    Role = roles.Any() ? roles.FirstOrDefault() : String.Empty,
                    Claims = claims.Select(c => new ClaimsDTO
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<Result> ChangePassword(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new Exception("User doesn't exist");

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (result.Succeeded)
            {
                if (user.IsVerified == false)
                {
                    user.IsVerified = true;

                    _authContext.Users.Update(user);

                    await _authContext.SaveChangesAsync();
                }
            }


            return result.ToApplicationResult();
        }

        public async Task<LoginResult> GoogleLogin(string firstname, string lastname, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            try
            {
                if (user == null)
                {
                    throw new UnauthorizedUserException("Registration Failed");
                }

                user = await _userManager.FindByEmailAsync(email);

                var created_token = CreateToken(user, out List<Claim> claims);

                var userRoles = await _userManager.GetRolesAsync(user);

                var userProfile = await GetUserProfile(email);

                return new LoginResult
                {
                    Token = created_token,
                    User = userProfile,
                    Roles = userRoles.ToArray()
                };
            }

            catch (Exception ex)
            {
                throw new GenericException(ex.Message);
            }
        }

        public async Task<bool> CreateUserRole(string role)
        {
            var userRole = new IdentityRole(role);

            try
            {
                await _roleManager.CreateAsync(userRole);
                return true;
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);

            }

        }

        public async Task<bool> AssignToRole(string userId, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                var roleExist = await _roleManager.RoleExistsAsync(role);

                if (roleExist)
                {
                    //assign default role
                    await _userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper()
                    });

                    await _userManager.AddToRoleAsync(user, role);
                }

                return true;
            }
            catch (Exception err)
            {
                throw new GenericException(err.Message);
            }
        }
        public async Task<PaginatedList<UserProfileDTO>> GetAllUsers(string roleFilter, string filter, int pageNumber, int pageSize)
        {
            var userResults = (from user in _authContext.Users
                               join userRole in _authContext.UserRoles
                               on user.Id equals userRole.UserId
                               join role in _authContext.Roles
                               on userRole.RoleId equals role.Id
                               where role.Name.Contains(roleFilter) &&
                               (user.FirstName.ToLower().Contains(filter.ToLower()) || user.LastName.ToLower().Contains(filter.ToLower()) || user.Email.ToLower().Contains(filter.ToLower()))
                               select new UserProfileDTO
                               {
                                   Id = user.Id,
                                   ProfilePicture = user.ProfilePicture,
                                   FirstName = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName : "Unknown",
                                   LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName : "Unknown",
                                   Email = user.Email,
                                   PhoneNumber = !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : "Unknown",
                                   IsVerified = user.IsVerified,
                                   IsLocked = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.Now ? true : false,
                                   Role = role.Name
                               });

            return await userResults.ToPaginatedListAsync(pageNumber, pageSize);

        }

        public async Task<IEnumerable<UserProfileBaseDTO>> GetAllUsers(string roleFilter, string filter)
        {
            var userResults = (from user in _authContext.Users
                               join userRole in _authContext.UserRoles
                               on user.Id equals userRole.UserId
                               join role in _authContext.Roles
                               on userRole.RoleId equals role.Id
                               where role.Name.Contains(roleFilter) &&
                               (user.FirstName.ToLower().Contains(filter.ToLower()) || user.LastName.ToLower().Contains(filter.ToLower()) || user.Email.ToLower().Contains(filter.ToLower()))
                               select new UserProfileBaseDTO
                               {
                                   Id = user.Id,
                                   ProfilePicture = user.ProfilePicture,
                                   FirstName = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName : "Unknown",
                                   LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName : "Unknown",
                                   Email = user.Email,
                                   PhoneNumber = !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : "Unknown",
                               });

            return await userResults.ToListAsync();

        }

        public async Task<UserProfileDTO> GetUserProfileById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToArray();

                var claims = await _userManager.GetClaimsAsync(user);

                return new UserProfileDTO
                {
                    Id = user.Id,
                    ProfilePicture = user.ProfilePicture,
                    FirstName = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName : "Unknown",
                    LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName : "Unknown",
                    Email = user.Email,
                    PhoneNumber = !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : "Unknown",
                    IsVerified = user.IsVerified,
                    IsLocked = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTimeOffset.Now ? true : false,
                    Role = roles.Any() ? roles.FirstOrDefault() : string.Empty,
                    Claims = claims.Select(c => new ClaimsDTO
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                };
            }
            else
            {
                return null;
            }
        }


        public async Task<GenerateResetPasswordResult> GetPasswordResetToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return null;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var emailTemplate = _configuration["EmailTemplates:ACCOUNT_RESET_PASSWORD"];

            EmailContentModel emailData = new EmailContentModel
            {
                FromEmail = SENDER,
                FromName = SENDER_NAME,
                ToEmail = user.Email,
                ToName = $"{user.FirstName} {user.LastName}",
                TemplateId = emailTemplate,
                DynamicObject = new ResetPasswordModel
                {
                    FirstName = user.FirstName,
                    ResetLink = $"{_configuration["ClientAppUrl"]}/auth?action=resetPassword&userId={user.Id}&code={token}"
                }
            };

            BackgroundJob.Enqueue(() => _emailService.SendEmail(emailData));

            return new GenerateResetPasswordResult
            {
                Token = token,
                UserId = user.Id
            };
        }


        public async Task<Result> ResetPassword(string userId, string password, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw new Exception("Invalid request");

            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (result.Succeeded)
            {
                user.LockoutEnabled = false;
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }

            return result.ToApplicationResult();
        }

        public async Task<Result> LockAccount(string email, DateTimeOffset? lockUntil)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (!user.LockoutEnabled)
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
            }

            var result = await _userManager.SetLockoutEndDateAsync(user, lockUntil);

            return result.ToApplicationResult();
        }

        public async Task<Result> UnlockAccount(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now - TimeSpan.FromMinutes(1));

            return result.ToApplicationResult();
        }

        public async Task<bool> DeleteRefreshToken(string userId, string refreshToken)
        {
            var token = await _authContext.AspNetRefreshTokens.FirstOrDefaultAsync(i => i.UserId == userId && i.Token == refreshToken);

            if (token == null) return false;

            _authContext.AspNetRefreshTokens.Remove(token);

            await _authContext.SaveChangesAsync();

            return true;
        }
    }
}
