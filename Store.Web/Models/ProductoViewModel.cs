using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Models
{
    public class ProductoViewModel:ProductoEntity
    {
        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría.")]
        [Required]
        public int CategoriaId { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }

    }
}
