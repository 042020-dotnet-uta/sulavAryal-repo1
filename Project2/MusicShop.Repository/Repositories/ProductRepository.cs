using Microsoft.EntityFrameworkCore;
using MusicShop.Domain;
using MusicShop.Repository.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly MSDbContext _context;

        public ProductRepository(MSDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddInventoryItems(InventoryItem inventoryItemToUpdate)
        {
            try
            {
                _context.Inventory.Update(inventoryItemToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<InventoryItem> GetProductQuantity(int storeId, int? Id)
        {
            try
            {
                return await _context.Inventory
                    .Include(i => i.Product)
                    .Include(i => i.Store)
                    .AsNoTracking()
                    .Where(c => c.ProductId == Id && c.StoreId == storeId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<InventoryItem>> GetStoreInventory(int? id)
        {
            try
            {
                var result = await _context.Inventory
                    .Include(i => i.Product)
                    .Include(i => i.Store)
                    .AsNoTracking()
                    .Where(c => c.StoreId == id).ToListAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
