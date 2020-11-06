using Store.Common.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Common.Entities
{
    public class CotizacionDetalleEntity : IAudit
    {
        public int Id { get; set; }
        public CotizacionEntity Cotizacion { get; set; }
        public int Cantidad { get; set; }
        public ProductoEntity Producto { get; set; }

        [DisplayName("Costo Unitario")]
        public decimal CostoU => Cantidad == 0 ? 0 : Costo / Cantidad;

        [DisplayName("Costo total")]
        [Column(TypeName = "decimal(18,5)")]
        public decimal Costo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal IVA { get; set; }

        public DateTimeOffset FechaC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserC { get; set; }
        public Guid UserM { get; set; }
    }
}