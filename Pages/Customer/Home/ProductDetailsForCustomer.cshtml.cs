using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using SportsStore_Spr2026.Data;
using SportsStore_Spr2026.Models;

namespace SportsStore_Spr2026.Pages.Customer.Home
{
    public class ProductDetailsForCustomerModel : PageModel
    {
        private readonly IProductRepository _proudctRepository;

        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ProductDetailsForCustomerModel(IProductRepository proudctRepository, 
            IShoppingCartRepository shoppingCartRepository)
        {

            _proudctRepository = proudctRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }




        public Product product { get; set; }





        public void OnGet(int id)
        {

            product = _proudctRepository.GetProductById(id);

        }

        public IActionResult OnPost(string cartID, int ProductId)
        {
            if (ModelState.IsValid)
            {
                _shoppingCartRepository.AddProductToCart(cartID, ProductId);
                return RedirectToPage("Index");
            }
            return Page();
        }





    }
}
