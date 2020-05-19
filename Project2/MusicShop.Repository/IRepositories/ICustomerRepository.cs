using MusicShop.Domain;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> FindCustomerById(int? id);
        Task<bool> ValidateCustomer(string username, string password);
    }
}
