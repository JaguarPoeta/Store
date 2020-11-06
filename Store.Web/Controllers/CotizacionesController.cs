using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Common.Entities;
using Store.Web.Data;

namespace Store.Web.Controllers
{
    public class CotizacionesController : Controller
    {
        private readonly DataContext _context;

        public CotizacionesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CotizacionEntities
                .Include(c=> c.Proveedor)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizacionEntity = await _context.CotizacionEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotizacionEntity == null)
            {
                return NotFound();
            }

            return View(cotizacionEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create([Bind("Id,FechaCotizacion,Estado,Costo,IVA,FechaC,FechaM,UserC,UserM")] CotizacionEntity cotizacionEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotizacionEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cotizacionEntity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizacionEntity = await _context.CotizacionEntities.FindAsync(id);
            if (cotizacionEntity == null)
            {
                return NotFound();
            }
            return View(cotizacionEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaCotizacion,Estado,Costo,IVA,FechaC,FechaM,UserC,UserM")] CotizacionEntity cotizacionEntity)
        {
            if (id != cotizacionEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotizacionEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotizacionEntityExists(cotizacionEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cotizacionEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizacionEntity = await _context.CotizacionEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotizacionEntity == null)
            {
                return NotFound();
            }

            return View(cotizacionEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cotizacionEntity = await _context.CotizacionEntities.FindAsync(id);
            _context.CotizacionEntities.Remove(cotizacionEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotizacionEntityExists(int id)
        {
            return _context.CotizacionEntities.Any(e => e.Id == id);
        }
    }
}
