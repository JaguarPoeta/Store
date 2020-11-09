using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Common.Entities;
using Store.Web.Data;
using Store.Web.Helpers;
using Store.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    [Authorize(Roles = "Proveedor")]
    public class CotizacionesController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public CotizacionesController(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CotizacionEntities
                  .Include(p => p.User)
                  .Include(p => p.Detalle)
                  .ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            Data.Entities.UserEntity usuario = await _userHelper.GetUserAsync(User.Identity.Name);

            var cotizacion = new CotizacionViewModel()
            {
                FechaCotizacion = DateTime.UtcNow,
                Estados = Common.Enums.Estados.Tramite,
                UserId = Guid.Parse(usuario.Id),
                NombreProveedor=usuario.FullName,
                Detalle = new List<CotizacionDetalleEntity>()
            };
            return View(cotizacion);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(CotizacionViewModel proveedorEntity)
        //{
        //}
    }
}