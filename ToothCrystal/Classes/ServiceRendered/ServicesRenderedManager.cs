using Raven.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using Raven.Client.Linq;

namespace ToothCrystal.Classes.ServiceRendered
{
    public class ServiceRenderedManager : IServiceRenderedManager
    {
        private IAsyncDataDocumentSession RavenSession { get; set; }

        public ServiceRenderedManager(IAsyncDataDocumentSession session)
        {
            RavenSession = session;
        }

        public async Task<string> AddNewServiceRendered(ServiceRendered newServiceRendered)
        {
            await RavenSession.StoreAsync(newServiceRendered);
            return newServiceRendered.Id;
        }

        public async Task<ServiceRendered> GetServiceRendered(string id)
        {
            return await RavenSession.LoadAsync<ServiceRendered>(id);
        }

        public async Task DeleteServiceRendered(string id)
        {
            var obj = await GetServiceRendered(id);
            RavenSession.Delete(obj);
        }

        public async Task<IList<ServiceRendered>> GetServiceRenderedList(string customerId)
        {
            RavenQueryStatistics stats;
            return await RavenSession.Query<ServiceRendered>().Statistics(out stats).Customize(x => x.WaitForNonStaleResultsAsOfNow()).Where(x => x.CustomerId == customerId).ToListAsync();
        }

        public async Task<string> UpdateServiceRendered(ServiceRendered updatedServiceRendered)
        {
            var obj = await GetServiceRendered(updatedServiceRendered.Id);
            obj.AmountPaid = updatedServiceRendered.AmountPaid;
            obj.TipAmount = updatedServiceRendered.TipAmount;
            obj.Service = updatedServiceRendered.Service;
            obj.Notes = updatedServiceRendered.Notes;

            await RavenSession.StoreAsync(obj);
            return obj.Id;
        }

        public async Task SaveChanges()
        {
            await RavenSession.SaveChangesAsync();
            RavenSession.Dispose();
        }
    }
}