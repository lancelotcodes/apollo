using apollo.Application.Common.Exceptions;
using apollo.Application.Common.Interfaces;
using apollo.Application.Common.Models;
using apollo.Infrastructure.Models;
using apollo.Presentation.Controllers.Base;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shared.Domain.Common;
using Shared.DTO.Response;
using System;
using System.Threading.Tasks;

namespace apollo.Infrastructure.Controllers
{
    public class AuthController : APIBaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;

        public AuthController(IIdentityService identityService, IConfiguration configuration)
        {
            _identityService = identityService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> LogIn([FromBody] LoginModel model)
        {
            var response = new Response<LoginResult>();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _identityService.GetToken(model.UserName, model.Password);

                    if (result != null)
                    {
                        //Cookie
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.UtcNow.AddDays(7),
                            SameSite = SameSiteMode.None,
                            Secure = true
                        };

                        var refreshToken = await _identityService.GenerateRefreshToken(result.User.Id);

                        if (!string.IsNullOrEmpty(refreshToken))
                        {
                            Response.Cookies.Append("X-Access-Token", result.Token, cookieOptions);
                            Response.Cookies.Append("X-Refresh-Token", refreshToken, cookieOptions);
                            Response.Cookies.Append("X-Username", result.User.Id, cookieOptions);
                        }
                        response.Data = result;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                else
                {
                    throw new GenericException("Username or password is required.");
                }
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [HttpPost("google")]
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleAuth(string token)
        {
            var response = new Response<LoginResult>();
            try
            {
                var googleClientId = _configuration["GoogleClientID"];

                var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] {
                       $"{googleClientId}"
                    }
                });

                var result = await _identityService.GoogleLogin(googleUser.GivenName, googleUser.FamilyName, googleUser.Email);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SameSite = SameSiteMode.None,
                    Secure = true
                };

                var refreshToken = await _identityService.GenerateRefreshToken(result.User.Id);

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    Response.Cookies.Append("X-Access-Token", result.Token, cookieOptions);
                    Response.Cookies.Append("X-Refresh-Token", refreshToken, cookieOptions);
                    Response.Cookies.Append("X-Username", result.User.Id, cookieOptions);
                }

                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [HttpPost("log-out")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> LogOut()
        {
            var response = new Response<object>();
            try
            {
                if (!(Request.Cookies.TryGetValue("X-Refresh-Token", out string refreshToken) && Request.Cookies.TryGetValue("X-Username", out string user))) throw new UnauthorizedAccessException();


                await _identityService.DeleteRefreshToken(user, refreshToken);

                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }

            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [HttpPost("change-password")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var response = new Response<Result>();
            try
            {
                if (model.Password != model.ConfirmPassword) throw new GenericException("Confirm password doesn't match new password.");

                var result = await _identityService.ChangePassword(User.Identity.Name, model.CurrentPassword, model.Password);

                response.Data = result;

            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [AllowAnonymous]
        [HttpGet("reset-password")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResetPasswordLink(string email)
        {
            var response = new Response<object>();
            try
            {
                var result = await _identityService.GetPasswordResetToken(email);

                if (result.UserId == null) throw new NotFoundException("Account", "email");
                response.Message = "A reset password link has been sent to your email";
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword([FromBody] Models.ResetPasswordModel model)
        {
            var response = new Response<Result>();
            try
            {
                if (model.Password == model.ConfirmPassword)
                {
                    var result = await _identityService.ResetPassword(model.UserId, model.Password, model.Token);

                    if (!result.Succeeded)
                        throw new BadRequestException(result.ToString());

                    response.Data = result;
                }
                else
                {
                    throw new GenericException("Password and confirm password don't match.");
                }
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }


        [HttpGet("me")]
        [ProducesResponseType(typeof(UserProfileDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProfile()
        {
            var response = new Response<UserProfileDTO>();
            try
            {
                var user = await _identityService.GetUserProfile(User.Identity.Name);
                response.Data = user;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin")]
        [HttpGet("users")]
        [ProducesResponseType(typeof(PaginatedList<UserProfileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers(int pageNumber = 1, int pageSize = 10, string roleName = "", string filter = "")
        {
            var response = new Response<PaginatedList<UserProfileDTO>>();
            try
            {
                var users = await _identityService.GetAllUsers(roleName, filter, pageNumber, pageSize);
                response.Data = users;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }


        [Authorize(Roles = "Superadmin")]
        [HttpGet("users/{userId}")]
        [ProducesResponseType(typeof(UserProfileDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var response = new Response<UserProfileDTO>();
            try
            {
                var user = await _identityService.GetUserProfileById(userId);

                if (user == null) throw new NotFoundException(nameof(ApplicationUser), userId);

                response.Data = user;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin")]
        [HttpPost("register")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var response = new Response<Result>();
            try
            {
                var result = await _identityService.CreateUserAsync(model.Username, model.Email, model.FirstName, model.LastName, model.Role);

                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> RefreshToken()
        {
            var response = new Response<LoginResult>();
            try
            {
                if (!(Request.Cookies.TryGetValue("X-Refresh-Token", out string refreshToken) && Request.Cookies.TryGetValue("X-Username", out string user))) return Unauthorized();

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SameSite = SameSiteMode.None,
                    Secure = true
                };

                var result = await _identityService.RefreshToken(user, refreshToken);

                if (result != null && !string.IsNullOrEmpty(result.Token))
                {

                    var newRefreshToken = await _identityService.GenerateRefreshToken(result.User.Id);

                    Response.Cookies.Append("X-Access-Token", result.Token, cookieOptions);
                    Response.Cookies.Append("X-Refresh-Token", newRefreshToken, cookieOptions);
                    Response.Cookies.Append("X-Username", result.User.Id, cookieOptions);

                    response.Data = result;
                }

                throw new UnauthorizedAccessException();
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin")]
        [HttpPost("roles")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddRole([FromQuery] string role)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _identityService.CreateUserRole(role);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }


        [Authorize(Roles = "Superadmin")]
        [HttpPost("lock")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> LockUser([FromBody] LockUserModel model)
        {
            var response = new Response<Result>();
            try
            {
                var result = await _identityService.LockAccount(model.Email, model.LockUntil);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }

        [Authorize(Roles = "Superadmin")]
        [HttpPost("unlock")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        public async Task<IActionResult> UnlockUser(string email)
        {
            var response = new Response<Result>();
            try
            {
                var result = await _identityService.UnlockAccount(email);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }


        [Authorize(Roles = "Superadmin")]
        [HttpPost("assign-to-role")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AssignUserToRole([FromBody] AssignToRoleModel model)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _identityService.AssignToRole(model.UserId, model.Role);
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.originalException = ex;
            }

            return HandleResponse(response);
        }
    }
}
