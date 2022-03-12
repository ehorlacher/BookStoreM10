using BookStoreM10.Models;
using BookStoreM10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreM10.Controllers
{
    public class HomeController : Controller
    {
        private IBookStoreRepository repo;
        public HomeController (IBookStoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string category, int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(c => c.Category == category || category == null)
                .OrderBy(t => t.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = 
                        (category == null 
                            ? repo.Books.Count() 
                            : repo.Books.Where(x => x.Category == category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };


            return View(x);
        }

    }
}
