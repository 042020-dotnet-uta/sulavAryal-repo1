using MusicShop.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<InventoryItem>> GetStoreInventory(int? id);
        Task<InventoryItem> GetProductQuantity(int storeId, int? Id);

        Task AddInventoryItems(InventoryItem inventoryItemToUpdate);
    }
}
