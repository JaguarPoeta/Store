using Store.Common.Entities;
using Store.Common.Enums;
using Store.Common.Interfaces;
using Store.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Store.Web.Entities
{
    public class CotizacionEntity : IAudit
    {
        public int Id { get; set; }

        [DisplayName("Fecha de cotización")]
        public DateTime FechaCotizacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [DisplayName("Fecha de cotización")]
        public DateTime DateLocal => FechaCotizacion.ToLocalTime();

        public UserEntity User { get; set; }
        public DateTime FechaCompra { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [DisplayName("Fecha de compra")]
        public DateTime DateCompraLocal => FechaCompra.ToLocalTime();

        public Estados Estados { get; set; }

        public ICollection<CotizacionDetalleEntity> Detalle { get; set; }

        [DisplayName("Costo total")]
        public decimal Costo => Detalle == null ? 0 : Detalle.Sum(p => p.Costo);

        public decimal IVA => Detalle == null ? 0 : Detalle.Sum(p => p.IVA);

        public decimal Total => Costo + IVA;
        public DateTimeOffset FechaC { get; set; }
        public DateTimeOffset FechaM { get; set; }
        public Guid UserC { get; set; }
        public Guid UserM { get; set; }
    }
}