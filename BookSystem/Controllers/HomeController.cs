
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookSystem.Models;
using BookSystem.ServiceLayer.Data.Contracts;
using BookSystem.Models.HomeViewModels;


namespace BookSystem.Controllers
{
    public class HomeController : Controller
    {

        private IBookService _bookService;

        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        
        public async Task<IActionResult> Index(string sortOrder, string searchByTitle)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchByTitle;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_asc" : "";

            var topBooks = await _bookService.GetTopBooks(sortOrder, searchByTitle);
            var model = new TopBooksViewModel(topBooks);

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
