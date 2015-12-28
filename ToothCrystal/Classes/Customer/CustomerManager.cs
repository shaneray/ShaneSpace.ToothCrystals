
using Raven.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToothCrystal.Areas.Admin.Models.Customer;

namespace ToothCrystal.Classes.Customer
{
    public class CustomerManager : ICustomerManager
    {
        private IAsyncDataDocumentSession RavenSession { get; set; }

        public CustomerManager(IAsyncDataDocumentSession session)
        {
            RavenSession = session;
        }

        public async Task CreateCustomer(Customer model)
        {
            await RavenSession.StoreAsync(model);
        }

        public async void DeleteCustomer(string id)
        {
            var customer = await GetCustomer(id);
            RavenSession.Delete(customer);
        }

        public async Task SaveChanges()
        {
            await RavenSession.SaveChangesAsync();
            RavenSession.Dispose();
        }

        public async Task UpdateCustomer(CreateViewModel model)
        {
            var obj = await GetCustomer(model.Id);
            obj.FirstName = model.FirstName;
            obj.LastName = model.LastName;
            obj.Address = model.Address;
            obj.Notes = model.Notes;
            await RavenSession.StoreAsync(obj);
        }

        public async Task<IList<Customer>> GetCustomerList()
        {
            return await RavenSession.Query<Customer>().Customize(x => x.WaitForNonStaleResultsAsOfLastWrite()).ToListAsync();
        }

        public async Task<Customer> GetCustomer(string id)
        {
            return await RavenSession.LoadAsync<Customer>(id);
        }
    }
}