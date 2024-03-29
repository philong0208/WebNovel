﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNovel.Models;
using WebNovel.Models.ViewModels;

namespace WebNovel.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly WebNovelContext _context;

        public ChaptersController(WebNovelContext context)
        {
            _context = context;
        }

        // GET: Chapters
        public async Task<IActionResult> Index()
        {
            var webNovelContext = _context.Chapters.Include(c => c.Novel);
            return View(await webNovelContext.ToListAsync());
        }

        // GET: Chapters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chapters == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters
                .Include(c => c.Novel)
                .FirstOrDefaultAsync(m => m.ChapterId == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // GET: Novel/1/Chapters/Details/5
        public async Task<IActionResult> getDetailsByNovelId(int? novelId, int? chapterId)
        {
            if (chapterId == null || _context.Chapters == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters
                .Include(c => c.Novel)
                .FirstOrDefaultAsync(m => m.ChapterId == chapterId);
            if (chapter == null)
            {
                return NotFound();
            }

            return View("Details", chapter);
        }

        // GET: Chapters/Create
        public IActionResult Create(int novelId)
        {
            if (novelId == null)
            {
                return NotFound();
            }
            ViewData["NovelId"] = novelId;
            return View();
        }

        // POST: Chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateChapterViewModel chapterVM)
        {
            Chapter chapter = new Chapter();
            if (ModelState.IsValid)
            {
                chapter.ChapterTitle = chapterVM.ChapterTitle;
                chapter.ChapterContent = chapterVM.ChapterContent;
                chapter.NovelId = chapterVM.NovelId;

                _context.Add(chapter);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm chương mới thành công";
                return RedirectToAction("Details", "Novels", new { id = chapter.NovelId });
            }
            TempData["error"] = "Thêm tác giả thất bại";
            //ViewData["NovelId"] = new SelectList(_context.Novels, "NovelId", "NovelId", chapter.NovelId);
            return View(chapter);
        }

        // GET: Chapters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chapters == null)
            {
                return NotFound();
            }
            EditChapterViewModel chapterViewModel = new EditChapterViewModel();
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            chapterViewModel.ChapterId = chapter.ChapterId;
            chapterViewModel.ChapterTitle = chapter.ChapterTitle;
            chapterViewModel.ChapterContent = chapter.ChapterContent;
            return View(chapterViewModel);
        }

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditChapterViewModel chapterVM)
        {

            if (id != chapterVM.ChapterId)
            {
                return NotFound();
            }

            Chapter chapter = new Chapter();
            if (ModelState.IsValid)
            {
                chapter.ChapterTitle = chapterVM.ChapterTitle;
                chapter.ChapterContent = chapterVM.ChapterContent;
                try
                {
                    _context.Update(chapter);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Sửa nội dung chương thành công";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChapterExists(chapter.ChapterId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["error"] = "Sửa nội dung chương thất bại";
                return RedirectToAction("Details", "Novels", new { id = chapter.NovelId });
            }
            //ViewData["NovelId"] = new SelectList(_context.Novels, "NovelId", "NovelId", chapter.NovelId);
            return View(chapter);
        }

        // GET: Chapters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chapters == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters
                .Include(c => c.Novel)
                .FirstOrDefaultAsync(m => m.ChapterId == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // POST: Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chapters == null)
            {
                return Problem("Entity set 'WebNovelContext.Chapters'  is null.");
            }
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter != null)
            {
                _context.Chapters.Remove(chapter);
            }
            
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa chương thành công";
            return RedirectToAction(nameof(Index));
        }

        private bool ChapterExists(int id)
        {
          return (_context.Chapters?.Any(e => e.ChapterId == id)).GetValueOrDefault();
        }
    }
}
