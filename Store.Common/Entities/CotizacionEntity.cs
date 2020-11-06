using Store.Common.Enums;
using Store.Common.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Common.Entities
{
    public class CotizacionEntity : IAudit
    {
        public int Id { get; set; }

        [DisplayName("Fecha de cotización")]
        public DateTimeOffset FechaCotizacion { get; set; }

        public ProveedorEntity Proveedor { get; set; }

        public Estados Estado { get; set; }

        [DisplayName("Costo total")]
        [Column(TypeName = "decimal(18,5)")]
        public decimal Costo { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal IVA { get; set; }

        public decimal Total => Costo + IVA;
        public DateTimeOffset FechaC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserC { get; set; }
        public Guid UserM { get; set; }
    }
}