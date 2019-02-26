using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSystem.Data.Models;
using BookSystem.Models.BookViewModels;
using BookSystem.Models.ManageViewModels;
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
        private readonly IReviewService _reviewService;

        public BookController(UserManager<User> userManager, IBookService bookService,
            IReviewService reviewService)
        {
            _userManager = userManager;
            _bookService = bookService;
            _reviewService = reviewService;
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

        


        public async Task<IActionResult> LikeBook(int bookId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Manage");
            }
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var likedBook = await _bookService.LikeBook(bookId, user);

            return RedirectToAction("Index", "Manage");
        }

        public async Task<IActionResult> DislikeBook(int bookId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Manage");
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var likedBook = await _bookService.DislikeBook(bookId, user);

            return RedirectToAction("Index", "Manage");
        }
    }
}