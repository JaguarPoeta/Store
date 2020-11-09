using System.ComponentModel.DataAnnotations;

namespace Store.Web.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nombres { get; set; }

        [MaxLength(50)]
        [Required]
        public string Apellidos { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100)]
        public string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}