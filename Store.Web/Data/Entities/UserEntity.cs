using Microsoft.AspNetCore.Identity;
using Store.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Store.Web.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        [MaxLength(50)]
        [Required]
        public string Nombres { get; set; }

        [MaxLength(50)]
        [Required]
        public string Apellidos { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100)]
        public string Direccion { get; set; }

        [Display(Name = "Tipo de usuario")]
        public TipoUsuario TipoUsuario { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{Nombres} {Apellidos}";
    }
}