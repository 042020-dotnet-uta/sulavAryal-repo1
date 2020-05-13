using MusicShop.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShop.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        //Task<IEnumerable<Customer>> Bear();
    }
}
