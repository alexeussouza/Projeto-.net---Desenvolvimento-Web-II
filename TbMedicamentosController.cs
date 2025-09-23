using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto1_IF.Models;

// Alexandre de Souza Eustaquio
namespace Projeto1_IF
{
    public class TbMedicamentosController : Controller
    {
        private readonly db_IFContext _context;

        public TbMedicamentosController(db_IFContext context)
        {
            _context = context;
        }

        // GET: TbMedicamentos
        public async Task<IActionResult> Index()
        {
            var db_IFContext = _context.TbMedicamento.Include(t => t.IdCategoriaMedicamentoNavigation);
            return View(await db_IFContext.ToListAsync());
        }

        // GET: TbMedicamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMedicamento = await _context.TbMedicamento
                .Include(t => t.IdCategoriaMedicamentoNavigation)
                .FirstOrDefaultAsync(m => m.IdMedicamento == id);
            if (tbMedicamento == null)
            {
                return NotFound();
            }

            return View(tbMedicamento);
        }

        // GET: TbMedicamentos/Create
        public IActionResult Create()
        {
            ViewData["IdCategoriaMedicamento"] = new SelectList(_context.TbCategoriaMedicamento, "IdCategoriaMedicamento", "Nome");
            return View();
        }

        // POST: TbMedicamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMedicamento,IdCategoriaMedicamento,Nome,Bula,BulaArquivo")] TbMedicamento tbMedicamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbMedicamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoriaMedicamento"] = new SelectList(_context.TbCategoriaMedicamento, "IdCategoriaMedicamento", "Nome", tbMedicamento.IdCategoriaMedicamento);
            return View(tbMedicamento);
        }

        // GET: TbMedicamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMedicamento = await _context.TbMedicamento.FindAsync(id);
            if (tbMedicamento == null)
            {
                return NotFound();
            }
            ViewData["IdCategoriaMedicamento"] = new SelectList(_context.TbCategoriaMedicamento, "IdCategoriaMedicamento", "Nome", tbMedicamento.IdCategoriaMedicamento);
            return View(tbMedicamento);
        }

        // POST: TbMedicamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMedicamento,IdCategoriaMedicamento,Nome,Bula,BulaArquivo")] TbMedicamento tbMedicamento)
        {
            if (id != tbMedicamento.IdMedicamento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbMedicamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbMedicamentoExists(tbMedicamento.IdMedicamento))
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
            ViewData["IdCategoriaMedicamento"] = new SelectList(_context.TbCategoriaMedicamento, "IdCategoriaMedicamento", "Nome", tbMedicamento.IdCategoriaMedicamento);
            return View(tbMedicamento);
        }

        // GET: TbMedicamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbMedicamento = await _context.TbMedicamento
                .Include(t => t.IdCategoriaMedicamentoNavigation)
                .FirstOrDefaultAsync(m => m.IdMedicamento == id);
            if (tbMedicamento == null)
            {
                return NotFound();
            }

            return View(tbMedicamento);
        }

        // POST: TbMedicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbMedicamento = await _context.TbMedicamento.FindAsync(id);
            if (tbMedicamento != null)
            {
                _context.TbMedicamento.Remove(tbMedicamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbMedicamentoExists(int id)
        {
            return _context.TbMedicamento.Any(e => e.IdMedicamento == id);
        }
    }
}
