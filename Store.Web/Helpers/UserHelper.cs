using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Common.Enums;
using Store.Web.Data;
using Store.Web.Data.Entities;
using Store.Web.Models;
using System;
using System.Threading.Tasks;

namespace Store.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;

        public UserHelper(DataContext context, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, SignInManager<UserEntity> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<UserEntity> AddUserAsync(AddUserViewModel model, TipoUsuario tipoUsuario)
        {
            UserEntity user = new UserEntity
            {
                Direccion = model.Direccion,
                Email = model.Usuario,
                Nombres = model.Nombres,
                Apellidos = model.Apellidos,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Usuario,
                TipoUsuario = tipoUsuario
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Contraseña);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            UserEntity newUser = await GetUserAsync(model.Usuario);
            await AddUserToRoleAsync(newUser, user.TipoUsuario.ToString());
            return newUser;
        }

        public async Task AddUserToRoleAsync(UserEntity user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<UserEntity> GetUserAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity> GetUserAsync(Guid userId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }

        public async Task<bool> IsUserInRoleAsync(UserEntity user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Usuario,
                model.Contraseña,
                model.Recuerdame,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> UpdateUserAsync(UserEntity user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}