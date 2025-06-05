using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Data;
using TiendaOnline.Models.Domian;

namespace TiendaOnline.Controllers
{
    public class VentaController : Controller
    {
        private readonly TiendaOnlineContext _context;

        public VentaController(TiendaOnlineContext context)
        {
            _context = context;
        }

        // GET: Venta
        public async Task<IActionResult> Index()
        {
            var tiendaOnlineContext = _context.Ventas.Include(v => v.Usuario);
            return View(await tiendaOnlineContext.ToListAsync());
        }

        // GET: Venta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaModel = await _context.Ventas
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (ventaModel == null)
            {
                return NotFound();
            }

            return View(ventaModel);
        }

        // GET: Venta/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Clave");
            return View();
        }

        // POST: Venta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VentaId,Fecha,Total,UsuarioId")] VentaModel ventaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Clave", ventaModel.UsuarioId);
            return View(ventaModel);
        }

        // GET: Venta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaModel = await _context.Ventas.FindAsync(id);
            if (ventaModel == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Clave", ventaModel.UsuarioId);
            return View(ventaModel);
        }

        // POST: Venta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VentaId,Fecha,Total,UsuarioId")] VentaModel ventaModel)
        {
            if (id != ventaModel.VentaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaModelExists(ventaModel.VentaId))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "UsuarioId", "Clave", ventaModel.UsuarioId);
            return View(ventaModel);
        }

        // GET: Venta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ventaModel = await _context.Ventas
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (ventaModel == null)
            {
                return NotFound();
            }

            return View(ventaModel);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ventaModel = await _context.Ventas.FindAsync(id);
            if (ventaModel != null)
            {
                _context.Ventas.Remove(ventaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaModelExists(int id)
        {
            return _context.Ventas.Any(e => e.VentaId == id);
        }
    }
}

