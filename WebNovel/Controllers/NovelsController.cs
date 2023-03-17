using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNovel.Models;

namespace WebNovel.Controllers
{
    public class NovelsController : Controller
    {
        private readonly WebNovelContext _context;

        public NovelsController(WebNovelContext context)
        {
            _context = context;
        }

        // GET: Novels
        public async Task<IActionResult> Index()
        {
            var webNovelContext = _context.Novels.Include(n => n.Author).Include(n => n.User);
            return View(await webNovelContext.ToListAsync());
        }

        // GET: Novels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Novels == null)
            {
                return NotFound();
            }

            var novel = await _context.Novels
                .Include(n => n.Author)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NovelId == id);
            if (novel == null)
            {
                return NotFound();
            }

            return View(novel);
        }

        // GET: Novels/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Novels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NovelId,NovelTitle,NovelDescription,NovelCover,NovelDatePost,NovelView,AuthorId,UserId")] Novel novel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(novel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName", novel.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", novel.UserId);
            return View(novel);
        }

        // GET: Novels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Novels == null)
            {
                return NotFound();
            }

            var novel = await _context.Novels.FindAsync(id);
            if (novel == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", novel.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", novel.UserId);
            return View(novel);
        }

        // POST: Novels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NovelId,NovelTitle,NovelDescription,NovelCover,NovelDatePost,NovelView,AuthorId,UserId")] Novel novel)
        {
            if (id != novel.NovelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(novel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NovelExists(novel.NovelId))
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
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorId", novel.AuthorId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", novel.UserId);
            return View(novel);
        }

        // GET: Novels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Novels == null)
            {
                return NotFound();
            }

            var novel = await _context.Novels
                .Include(n => n.Author)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.NovelId == id);
            if (novel == null)
            {
                return NotFound();
            }

            return View(novel);
        }

        // POST: Novels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Novels == null)
            {
                return Problem("Entity set 'WebNovelContext.Novels'  is null.");
            }
            var novel = await _context.Novels.FindAsync(id);
            if (novel != null)
            {
                _context.Novels.Remove(novel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NovelExists(int id)
        {
          return _context.Novels.Any(e => e.NovelId == id);
        }
    }
}
