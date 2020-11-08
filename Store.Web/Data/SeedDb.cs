using Store.Common.Enums;
using Store.Web.Data.Entities;
using Store.Web.Helpers;
using System.Threading.Tasks;

namespace Store.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Alfredo",
                                 "Linares",
                                 "alfredolinares@hotmail.com",
                                 "+503 6048 4100",
                                 "Santa Ana",
                                 TipoUsuario.Admin);
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(TipoUsuario.Admin.ToString());
            await _userHelper.CheckRoleAsync(TipoUsuario.Usuario.ToString());
            await _userHelper.CheckRoleAsync(TipoUsuario.Proveedor.ToString());
        }

        private async Task<UserEntity> CheckUserAsync(string nombres,
                                                      string apellidos,
                                                      string email,
                                                      string phone,
                                                      string direccion,
                                                      TipoUsuario tipoUsuario)
        {
            UserEntity user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    Nombres = nombres,
                    Apellidos = apellidos,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Direccion = direccion,
                    TipoUsuario = tipoUsuario
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, tipoUsuario.ToString());
            }

            return user;
        }
    }
}