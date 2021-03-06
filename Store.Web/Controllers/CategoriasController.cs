﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Common.Entities;
using Store.Web.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriasController : Controller
    {
        private readonly DataContext _context;

        public CategoriasController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriaEntities.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEntity = await _context.CategoriaEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaEntity == null)
            {
                return NotFound();
            }

            return View(categoriaEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaEntity categoriaEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(categoriaEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        if (dbUpdateException.InnerException.Message.Contains("Nombre"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe la categoría con este nombre.");
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

            return View(categoriaEntity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEntity = await _context.CategoriaEntities.FindAsync(id);
            if (categoriaEntity == null)
            {
                return NotFound();
            }
            return View(categoriaEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaEntity categoriaEntity)
        {
            if (id != categoriaEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        if (dbUpdateException.InnerException.Message.Contains("Nombre"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe la categoría con este nombre.");
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
            return View(categoriaEntity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEntity = await _context.CategoriaEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoriaEntity == null)
            {
                return NotFound();
            }

            return View(categoriaEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaEntity = await _context.CategoriaEntities.FindAsync(id);
            _context.CategoriaEntities.Remove(categoriaEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaEntityExists(int id)
        {
            return _context.CategoriaEntities.Any(e => e.Id == id);
        }
    }
}