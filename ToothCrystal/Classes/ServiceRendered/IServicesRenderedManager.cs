using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToothCrystal.Classes.ServiceRendered
{
    public interface IServiceRenderedManager
    {
        Task<string> AddNewServiceRendered(ServiceRendered newServiceRendered);
        Task<ServiceRendered> GetServiceRendered(string id);
        Task DeleteServiceRendered(string id);
        Task<IList<ServiceRendered>> GetServiceRenderedList(string customerId);
        Task<string> UpdateServiceRendered(ServiceRendered updatedServiceRendered);
        Task SaveChanges();
    }
}
