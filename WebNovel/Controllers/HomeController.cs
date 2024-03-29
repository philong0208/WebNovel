﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebNovel.Models;
using WebNovel.Models.Pagination;

namespace WebNovel.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebNovelContext _context;
        //private readonly ILogger<HomeController> _logger;

        public HomeController(WebNovelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchText = "", int pg = 1, int pageSize = 5)
        {
            var webNovelContext = _context.Novels
                .Include(n => n.Author)
                .Include(n => n.Genres)
                .Include(n => n.Chapters)
                .AsQueryable();

            //const int pageSize = 5;
            if (pg < 1) { 
                pg = 1; 
            }

            if (searchText != "" && searchText != null)
            {
                webNovelContext = webNovelContext
                    .Where(p => p.NovelTitle.Contains(searchText))
                    .AsQueryable();
            }

            int recordCount = webNovelContext.Count();

            var pager = new Pager(recordCount, pg, pageSize);

            int recordSkip = (pg - 1) * pageSize;

            List<Novel> data = await webNovelContext.Skip(recordSkip).Take(pager.PageSize).ToListAsync();
            
            this.ViewBag.Pager = pager;

            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}