using Microsoft.AspNetCore.Hosting;
using Store.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Store.Common.Entities
{
    public class CategoriaEntity : IAudit
    {
        private readonly IHostingEnvironment _hostingEnv;

        [Display(Name = "Código")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Nombre { get; set; }

        [Display(Name = "Imagen")]
        public Guid ImageId { get; set; }

        [Display(Name = "Imagen")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? Path.Combine(_hostingEnv.WebRootPath, "img", "noimages.png")

            : Path.Combine(_hostingEnv.WebRootPath, "img\\cat", String.Format("{{0}}", ImageId));

        public ProductoEntity Producto { get; set; }

        public DateTimeOffset FechaC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserC { get; set; }
        public Guid UserM { get; set; }
    }
}