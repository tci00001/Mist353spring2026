using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore_Spr2026.Data;
using SportsStore_Spr2026.Models;

namespace SportsStore_Spr2026.Pages.Customer.Home
{
    public class PlaceOrderModel : PageModel
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public PlaceOrderModel(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }


        public Orderinput OrderInput { get; set; }

        public List<ShoppingCart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public void OnGet()
        {
            string cartID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            CartItems = _shoppingCartRepository.LoadCartItems(cartID, out decimal total);

            CartTotal = total;
        }
    }
}
