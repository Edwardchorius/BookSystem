using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookSystem.Data.Models;
using BookSystem.Models.BookViewModels;
using BookSystem.Models.ReviewViewModels;
using BookSystem.ServiceLayer.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IBookService _bookService;
        private readonly IReviewService _reviewService;

        public ReviewController(UserManager<User> userManager, IBookService bookService,
            IReviewService reviewService)
        {
            _userManager = userManager;
            _bookService = bookService;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> MakeReview(int bookId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var book = await _bookService.GetById(user, bookId);
            var model = new MakeReviewViewModel(user, book);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeReview(int Id, MakeReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MakeReview", "Book");
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var addReview = await _reviewService.MakeReview(user, Id, model.Content);

            return RedirectToAction("Index", "Manage", new { });
        }

        [HttpGet]
        public async Task<IActionResult> BookReviews(int bookId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            var bookReviews = await _reviewService.GetBookReviews(bookId);
            var model = new BookReviewsViewModel(bookReviews);

            return View(model);
        }
    }
}