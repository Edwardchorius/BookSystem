using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSystem.Data.Models;
using BookSystem.Models.BookViewModels;
using BookSystem.ServiceLayer.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSystem.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IBookService _bookService;

        public BookController(UserManager<User> userManager, IBookService bookService)
        {
            _userManager = userManager;
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult AddBook(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(AddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddBook", "Book");
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var addBook = await _bookService.AddBook(user, model.Title, model.Genre);

            return RedirectToAction("Index", "Manage");
        }
    }
}