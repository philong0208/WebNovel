﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebNovel.Models;

namespace WebNovel.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebNovelContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
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

        //public IEnumerable<Novel> getListNovel()
        //{
        //    var list = new List<Novel>();
        //    return list;
        //}
    }
}