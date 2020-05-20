namespace MusicShop.UI.ViewModel
{
    public class ShoppingCartViewModel
    {
        /// <summary>
        /// To know which shopping cart does the item belongs to. 
        /// </summary>
        public ShoppingCart ShoppingCart { get; set; }
        /// <summary>
        /// Total Value for the shopping cart.
        /// </summary>
        public decimal ShoppingCartTotal { get; set; }
        /// <summary>
        /// Gets, Sets StoreId
        /// </summary>
        public int? StoreId { get; set; }
    }
}