using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreM10.Infrastructure;
using BookStoreM10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookStoreM10.Pages
{
    public class PurchaseModel : PageModel
    {
        private IBookStoreRepository repo { get; set; }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        public PurchaseModel (IBookStoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        // Isbn is a string, not an integer
        public IActionResult OnPost(int bookid, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookid);

            cart.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove (int bookid, string returnUrl)
        {
            cart.RemoveItem(cart.Items.First(x => x.Book.BookId == bookid).Book);

            return RedirectToPage(new {ReturnUrl = returnUrl });
        }
    }
}
