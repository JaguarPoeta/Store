using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Common.Entities;
using Store.Common.Extension;
using Store.Web.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly DataContext _context;

        public ProveedoresController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProveedorEntity proveedorEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Auditoria();
                    _context.Add(proveedorEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        if (dbUpdateException.InnerException.Message.Contains("Nombre"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe un proveedor con este nombre.");
                        }
                        if (dbUpdateException.InnerException.Message.Contains("NRC"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe un proveedor con este NRC.");
                        }
                        if (dbUpdateException.InnerException.Message.Contains("NIT"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe un proveedor con este NIT.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(proveedorEntity);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var proveedorEntity = await _context.ProveedorEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedorEntity == null)
            {
                return NotFound();
            }
            _context.Auditoria();
            _context.ProveedorEntities.Remove(proveedorEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {

            var proveedorEntity = await _context.ProveedorEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedorEntity == null)
            {
                return NotFound();
            }

            return View(proveedorEntity);
        }

        public async Task<IActionResult> Edit(string id)
        {

            var proveedorEntity = await _context.ProveedorEntities.FindAsync(id);
            if (proveedorEntity == null)
            {
                return NotFound();
            }
            return View(proveedorEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProveedorEntity proveedorEntity)
        {
            if (id != proveedorEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Auditoria();
                    _context.Update(proveedorEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        if (dbUpdateException.InnerException.Message.Contains("Nombre"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe un proveedor con este nombre.");
                        }
                        if (dbUpdateException.InnerException.Message.Contains("NRC"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe un proveedor con este NRC.");
                        }
                        if (dbUpdateException.InnerException.Message.Contains("NIT"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe un proveedor con este NIT.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedorEntity);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProveedorEntities.ToListAsync());
        }

        private bool ProveedorEntityExists(string id)
        {
            return _context.ProveedorEntities.Any(e => e.Id == id);
        }
    }
}