using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore_Spr2026.Data;
using SportsStore_Spr2026.Models;

namespace SportsStore_Spr2026.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public CreateModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [BindProperty]
        public Product product {  get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        { 
            //use the project repository to talk to the db and post the data
            _productRepository.CreateProduct(product);//crreates the product in the product table

            return RedirectToPage("Index");

        }
    }
}
