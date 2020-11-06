using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Common.Entities;
using Store.Common.Extension;
using Store.Web.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DataContext _context;

        public ProductosController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductoEntities.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoEntity productoEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoEntity);
                _context.Auditoria();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productoEntity);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoEntity = await _context.ProductoEntities.FindAsync(id);
            if (productoEntity == null)
            {
                return NotFound();
            }
            return View(productoEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ProductoEntity productoEntity)
        {
            if (id != productoEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoEntity);
                    _context.Auditoria();
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoEntityExists(productoEntity.Id))
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
            return View(productoEntity);
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