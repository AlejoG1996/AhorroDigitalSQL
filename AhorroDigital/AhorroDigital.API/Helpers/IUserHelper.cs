using AhorroDigital.API.Data.Entities;
using AhorroDigital.API.Models;
using AhorroDigital.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace AhorroDigital.API.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<User> AddUserAsync(AddUserViewModel model, Guid imageId, UserType userType);
        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<User> GetUserAsync(Guid id);

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> DeleteUserAsync(User user);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<IdentityResult> ConfirmEmailAsync(User user, string token);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

        Task<SignInResult> ValidatePasswordAsync(User user, string password);
    }
}
