
namespace Lab_03.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.Id == productId);
        }

        internal void UpdateQuantity(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Id == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }

        // Các phương thức khác...
    }

}
