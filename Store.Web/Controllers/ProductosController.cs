using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Common.Entities;
using Store.Common.Extension;
using Store.Web.Data;
using Store.Web.Helpers;
using Store.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public ProductosController(DataContext context, ICombosHelper combosHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductoEntities
                .Include(p => p.Categoria)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoEntity = await _context.ProductoEntities
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoEntity == null)
            {
                return NotFound();
            }

            return View(productoEntity);
        }

        public IActionResult Create()
        {
            ProductoViewModel model = new ProductoViewModel
            {
                Categorias = _combosHelper.GetComboCategorias(),
                IsActive = true,
                Unidades = 0,
                Costo = 0,
                Precio = 0
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductoEntity producto = await _converterHelper.ToProductoAsync(model, true);
                    _context.Add(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "El código del producto ya existe.");
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

            model.Categorias = _combosHelper.GetComboCategorias();
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductoEntity producto = await _context.ProductoEntities
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            ProductoViewModel model = _converterHelper.ToProductViewModel(producto);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductoEntity producto = await _converterHelper.ToProductoAsync(model, false);

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "El código del producto ya existe.");
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

            model.Categorias = _combosHelper.GetComboCategorias();
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoEntity = await _context.ProductoEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productoEntity == null)
            {
                return NotFound();
            }

            return View(productoEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productoEntity = await _context.ProductoEntities.FindAsync(id);
            _context.ProductoEntities.Remove(productoEntity);

            await _context.SaveChangesAsync();
            _context.Auditoria();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoEntityExists(string id)
        {
            return _context.ProductoEntities.Any(e => e.Id == id);
        }
    }
}