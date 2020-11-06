using Store.Common.Interfaces;
using System;

namespace Store.Common.Entities
{
    public class CompraEntity : IAudit
    {
        public int Id { get; set; }
        public DateTimeOffset FechaCompra { get; set; }
        public string NumeroFactura { get; set; }

        public decimal Costo { get; set; }

        public decimal IVA { get; set; }

        public DateTimeOffset FechaC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserC { get; set; }
        public Guid UserM { get; set; }
    }
}