using Microsoft.AspNetCore.Identity;
using Store.Common.Enums;
using Store.Web.Data.Entities;
using Store.Web.Models;
using System;
using System.Threading.Tasks;

namespace Store.Web.Helpers
{
    public interface IUserHelper
    {
        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task<UserEntity> AddUserAsync(AddUserViewModel model, TipoUsuario tipoUsuario);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);

        Task CheckRoleAsync(string roleName);

        Task<UserEntity> GetUserAsync(string email);

        Task<UserEntity> GetUserAsync(Guid userId);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(UserEntity user);
    }
}