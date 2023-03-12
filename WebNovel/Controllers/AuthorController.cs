using Microsoft.AspNetCore.Mvc;
using WebNovel.Models;

namespace WebNovel.Controllers
{
    public class AuthorController : Controller
    {
        private readonly NovelReaderContext _db;
        public AuthorController(NovelReaderContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var listAuthor = refreshListAuthor();
            return View(listAuthor);
        }

        public IEnumerable<Author> refreshListAuthor()
        {
            IEnumerable<Author> refreshListAuthor = _db.Authors;
            return refreshListAuthor;
        }
    }
}
