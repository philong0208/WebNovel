using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebNovel.Models;
using WebNovel.Models.ViewModels;

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
            var webNovelContext = _context.Novels
                .Include(n => n.Author)
                .Include(n => n.User)
                .Include(n => n.Genres);

            if (User.Identity.IsAuthenticated) // Xác định đã có người đăng nhập hay chưa
            {
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value; // Lấy ID người đăng nhập hiện tại
                var currentUserRole = currentUser.FindFirst(ClaimTypes.Role).Value; // Lấy Role người đăng nhập hiện tại
                if(currentUserRole != "1")
                {
                    var userNovels = from n in _context.Novels.Include(n => n.Author).Include(n => n.User).Include(n => n.Genres)
                                     where n.UserId == currentUserID
                                     select n;
                    return View(await userNovels.ToListAsync());
                }
            }

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
                .Include(n => n.Genres)
                .Include(n => n.Chapters)
                .FirstOrDefaultAsync(m => m.NovelId == id);
            
            if (novel == null)
            {
                return NotFound();
            }
            try
            {
                novel.NovelView += 1;
                _context.Update(novel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
            }
            

            return View(novel);
        }

        // GET: Novels/Create
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(_context.Authors, "AuthorId", "AuthorName");
            
            ViewBag.GenreId = new SelectList(_context.Genres, "GenreId", "GenreName");
            return View();
        }

        // POST: Novels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNovelViewModel novelVM)
        {
            Novel novel = new Novel();
            try
            {
                if (ModelState.IsValid)
                {
                    novel.NovelTitle = novelVM.NovelTitle;
                    novel.NovelCover = ""; // Bằng cách nào đó không thể upload được file
                    novel.NovelDescription = novelVM.NovelDescription;
                    novel.AuthorId = novelVM.AuthorID;
                    
                    ClaimsPrincipal currentUser = this.User;
                    var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value; // Lấy ID người đăng nhập hiện tại
                    novel.UserId = currentUserID;

                    List<int> list = novelVM.GenreId;

                    foreach (var genreId in list)
                    {
                        var genre = new Genre();
                        genre.GenreId = genreId;
                        novel.Genres.Add(genre);
                    }
                    _context.Novels.Attach(novel).State = EntityState.Added;
                    _context.Add(novel);
                    TempData["success"] = "Thêm tiểu thuyết mới thành công";
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Thêm tiểu thuyết mới thất bại";
                    ModelState.AddModelError("", "Không thể thêm.Hãy thử lại, nếu vấn đề còn tồn tại, liên hệ người có chuyên môn.");
                }
            }
            catch (DbUpdateException)
            {
                TempData["error"] = "Thêm tiểu thuyết mới thất bại";
                ModelState.AddModelError("", "Không thể thêm. Hãy thử lại, nếu vấn đề còn tồn tại, liên hệ người có chuyên môn.");
            }
            ViewBag.AuthorId = new SelectList(_context.Authors, "AuthorId", "AuthorName", novelVM.AuthorID);
            
            ViewBag.GenreId = new SelectList(_context.Genres, "GenreId", "GenreName");
            TempData["error"] = "Thêm tiểu thuyết mới thất bại";
            return View(novel);
        }

        // GET: Novels/Edit/5
        [Authorize]
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
            var novelVM = new EditNovelViewModel();
            novelVM.NovelID = novel.NovelId;
            novelVM.NovelTitle = novel.NovelTitle;
            novelVM.NovelDescription = novel.NovelDescription;
            novelVM.AuthorID = novel.AuthorId;
            ViewData["AuthorId"] = new SelectList(_context.Authors, "AuthorId", "AuthorName", novel.AuthorId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName");
            // Chỗ này để GET UserID
            return View(novelVM);
        }

        // POST: Novels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditNovelViewModel novelVM)
        { /* Nên tạo mới View Model để thực hiện input/edit trên view. 
            Không nên dùng entity vì khi kiểm tra ModelState các trường chưa input/edit sẽ bị null dẫn đến invalid ModelState
            */
            if (id != novelVM.NovelID)
            {
                return NotFound();
            }
            Novel novel = new Novel();
            //novel = _context.Novels.Find(novelVM.NovelID);
            /* Vì dùng ModelState nên phải lấy data vào entity rồi edit.
            Nếu không, các trường chưa có thông tin sẽ bị null*/
            novel = _context.Novels.Include("Genres").Single(n => n.NovelId == novelVM.NovelID); // Lấy genre của novels
           
            if (ModelState.IsValid)
            {
                novel.NovelTitle = novelVM.NovelTitle;
                novel.NovelDescription = novelVM.NovelDescription;
                novel.AuthorId = novelVM.AuthorID;
                List<int> listGenre = new List<int>();
                listGenre = novelVM.GenreID;

                foreach (var genre in novel.Genres.ToList())
                {// Xóa những genre không match với listGenre mới
                    if (!listGenre.Contains(genre.GenreId))
                    {
                        novel.Genres.Remove(genre);
                    }
                }

                foreach (var newGenreId in listGenre)
                {// Thêm genre mới mà không nằm trong list Genre cũ
                    if(!novel.Genres.Any(g => g.GenreId == newGenreId))
                    {
                        var genre = new Genre();
                        genre.GenreId = newGenreId;
                        _context.Genres.Attach(genre); // Không biết tại sao dòng này xài được mà dòng dưới không xài được
                        novel.Genres.Add(genre);
                    }
                }

                try
                {
                    //_context.Novels.Attach(novel).State = EntityState.Added; // Dòng dưới
                    _context.Update(novel);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Sửa tiểu thuyết thành công";
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
            
            TempData["success"] = "Sửa tiểu thuyết thành công";
            return View(novelVM);
        }

        // GET: Novels/Delete/5
        [Authorize]
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
            //var novel = await _context.Novels.FindAsync(id);
            var novel = _context.Novels.Include("UserComments").Single(n => n.NovelId == id);
            if (novel != null)
            {
                if(novel.UserComments.Count > 0)
                {
                    TempData["error"] = "Không thể xóa tiểu thuyết do đã có bình luận";
                    return RedirectToAction(nameof(Index));
                }
                _context.Novels.Remove(novel);
            }
            
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa tiểu thuyết thành công";
            return RedirectToAction(nameof(Index));
        }

        private bool NovelExists(int id)
        {
          return _context.Novels.Any(e => e.NovelId == id);
        }

        // POST là lấy data từ form, GET là đưa data ra form.
        // Vì sử dụng Query parameter nên ở đây phải GET để truyền data qua form khác
        [HttpGet("novels/{novelId}/chapters")] 
        public IActionResult CreateChapter([FromRoute]int novelId)
        {
            return RedirectToAction("Create", "Chapters", new {novelId});
        }
                        
        [HttpGet("novels/{novelId}/chapters/{chapterId}")] // I have no idea how routing works, it just, works...
        public IActionResult ChapterDetails([FromRoute]int novelId, [FromRoute]int chapterId)
        {
            return RedirectToAction("getDetailsByNovelId", "Chapters", new { novelId, chapterId });
        }
    }
}
