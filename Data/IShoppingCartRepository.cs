using SportsStore_Spr2026.Models;

namespace SportsStore_Spr2026.Data
{
    public interface IShoppingCartRepository
    {
        void AddProductToCart(string cartID, int prodID);

        List<ShoppingCart> LoadCartItems(string cartID, out decimal total);

        void UpdateCartItem(string cartID, int ProductId, int quantity);

    }
}
