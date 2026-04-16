using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore_Spr2026.Data;
using SportsStore_Spr2026.Models;

namespace SportsStore_Spr2026.Pages.Customer.Home
{
    public class ShoppingCartModel : PageModel
    {

        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartModel(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository=shoppingCartRepository;
        }
        [BindProperty]
        public List<ShoppingCart> CartItems { get; set; }

        string cartID { get; set; }
        public decimal CartTotal { get; set; }

        public void OnGet()
        {
            cartID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            CartItems = _shoppingCartRepository.LoadCartItems(cartID, out decimal total);

            CartTotal = total;
        }

        public IActionResult OnPost()
        {
            cartID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            if(ModelState.IsValid)
            {
                foreach(var item in CartItems)
                {
                    _shoppingCartRepository.UpdateCartItem(cartID, item.ProductID, item.Quantity);
                }
                CartItems = _shoppingCartRepository.LoadCartItems(cartID, out decimal total);
                CartTotal = total;
            }

            return Page();
        }
    }
}
