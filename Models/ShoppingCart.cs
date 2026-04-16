namespace SportsStore_Spr2026.Models
{
    public class ShoppingCart
    {
        public string CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }
    }
}
