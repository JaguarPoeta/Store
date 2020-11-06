using Store.Common.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Common.Entities
{
    public class ProductoEntity : IAudit
    {
        [DisplayName("Código")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        public string Id { get; set; }

        [MaxLength(50, ErrorMessage = "El {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El Campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [DisplayName("Existencia")]
        public int Unidades { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [DisplayName("Costo total")]
        [Column(TypeName = "decimal(18,5)")]
        public decimal Costo { get; set; }

        [DisplayName("Costo Unitario")]
        public decimal CostoU => Unidades == 0 ? 0 : Costo / Unidades;

        public DateTimeOffset FechaC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserC { get; set; }
        public Guid UserM { get; set; }
    }
}