using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;

namespace WebApplication5.Controllers
{
    public class DifficultyLevelsController : Controller
    {
        private readonly DbcoursesContext _context;

        public DifficultyLevelsController(DbcoursesContext context)
        {
            _context = context;
        }

        // GET: DifficultyLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.DifficultyLevels.ToListAsync());
        }

        // GET: DifficultyLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var difficultyLevel = await _context.DifficultyLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (difficultyLevel == null)
            {
                return NotFound();
            }

            return View(difficultyLevel);
        }

        // GET: DifficultyLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DifficultyLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DifLevel")] DifficultyLevel difficultyLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(difficultyLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(difficultyLevel);
        }

        // GET: DifficultyLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var difficultyLevel = await _context.DifficultyLevels.FindAsync(id);
            if (difficultyLevel == null)
            {
                return NotFound();
            }
            return View(difficultyLevel);
        }

        // POST: DifficultyLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DifLevel")] DifficultyLevel difficultyLevel)
        {
            if (id != difficultyLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(difficultyLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DifficultyLevelExists(difficultyLevel.Id))
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
            return View(difficultyLevel);
        }

        // GET: DifficultyLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var difficultyLevel = await _context.DifficultyLevels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (difficultyLevel == null)
            {
                return NotFound();
            }

            return View(difficultyLevel);
        }

        // POST: DifficultyLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var difficultyLevel = await _context.DifficultyLevels.FindAsync(id);
            if (difficultyLevel != null)
            {
                _context.DifficultyLevels.Remove(difficultyLevel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DifficultyLevelExists(int id)
        {
            return _context.DifficultyLevels.Any(e => e.Id == id);
        }
    }
}
