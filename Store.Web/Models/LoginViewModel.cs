using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Usuario { get; set; }

        [Required]
        [MinLength(6)]
        public string Contraseña { get; set; }
        [Display(Name = "Recuérdame")]
        public bool Recuerdame { get; set; }

    }
}
