﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TutorApp.Website.Context;
using TutorApp.Website.Models;
using TutorApp.Website.ViewModels;

namespace TutorApp.Website.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolDbContext _context;

        public HomeController(ILogger<HomeController> logger, SchoolDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            GeneralModel model = new GeneralModel();

            // Use nullable navigation and null-coalescing operator
            model.LogoUrl = _context.Settings.FirstOrDefault(x => x.Key == "logo-url")?.Value ?? string.Empty;
            model.Phone = _context.Settings.FirstOrDefault(x => x.Key == "phone")?.Value ?? string.Empty;
            model.Email = _context.Settings.FirstOrDefault(x => x.Key == "email")?.Value ?? string.Empty;
            model.Address = _context.Settings.FirstOrDefault(x => x.Key == "address")?.Value ?? string.Empty;

            return View(model);
        }

        public IActionResult Contact()
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

       
    }
}