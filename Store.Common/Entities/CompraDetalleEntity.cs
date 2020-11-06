using Store.Common.Interfaces;
using System;
using System.ComponentModel;

namespace Store.Common.Entities
{
    public class CompraDetalleEntity : IAudit
    {
        public int Id { get; set; }
        public CompraEntity Compra { get; set; }
        public int Cantidad { get; set; }
        public ProductoEntity Producto { get; set; }

        [DisplayName("Costo Unitario")]
        public decimal CostoU => Cantidad == 0 ? 0 : Costo / Cantidad;

        public decimal Costo { get; set; }

        public decimal IVA { get; set; }

        public DateTimeOffset FechaC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserC { get; set; }
        public Guid UserM { get; set; }
    }
}