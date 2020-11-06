using Store.Common.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.Common.Entities
{
    public class ProveedorEntity: IAudit
    {
        [DisplayName("Código del proveedor")]
        [MaxLength(20, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Id { get; set; }

        [MaxLength(50, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [MaxLength(50, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [MaxLength(8, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        [DisplayName("Teléfono")] 
        public string Telefono { get; set; }

        [MaxLength(10, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        public string NRC { get; set; }

        [MaxLength(14, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        public string NIT { get; set; }

        public DateTimeOffset FechaC { get; set; }
        public Guid UserC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserM { get; set; }
    }
}