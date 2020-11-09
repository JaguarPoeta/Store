using Store.Common.Entities;
using Store.Common.Enums;
using Store.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Store.Web.Models
{
    public class CotizacionViewModel
    {
        public CotizacionViewModel()
        {
            Detalle = new List<CotizacionDetalleEntity>();
        }

        #region cabecera

        public int Id { get; set; }

        [DisplayName("Fecha")]
        public DateTime FechaCotizacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
        [DisplayName("Fecha de cotización")]
        public DateTime DateLocal => FechaCotizacion.ToLocalTime();

        public UserEntity User { get; set; }
        public Guid UserId { get; set; }

        [Display(Name = "Nombre del proveedor")]
        [MaxLength(100)]
        public string NombreProveedor { get; set; }

        public Estados Estados { get; set; }

        public string CabeceraProductoId { get; set; }
        public string CabeceraProductoNombre { get; set; }
        public int CabeceraProductoCantidad { get; set; }
        public decimal CabeceraProductoPrecio { get; set; }

        #endregion cabecera

        #region detalle

        public List<CotizacionDetalleEntity> Detalle { get; set; }

        #endregion detalle

        #region pie

        [DisplayName("Costo total")]
        public decimal Costo => Detalle == null ? 0 : Detalle.Sum(p => p.Costo);

        public decimal IVA => Detalle == null ? 0 : Detalle.Sum(p => p.IVA);

        public decimal Total => Costo + IVA;

        #endregion pie
    }
}