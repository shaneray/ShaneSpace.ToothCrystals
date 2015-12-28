using System.Collections.Generic;
using System.Threading.Tasks;
using ToothCrystal.Areas.Admin.Models.Customer;

namespace ToothCrystal.Classes.Customer
{
    public interface ICustomerManager
    {
        Task CreateCustomer(Customer model);
        void DeleteCustomer(string id);
        Task<IList<Customer>> GetCustomerList();
        Task<Customer> GetCustomer(string id);
        Task SaveChanges();
        Task UpdateCustomer(CreateViewModel model);
    }
}