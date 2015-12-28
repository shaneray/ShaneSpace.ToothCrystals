

using Raven.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToothCrystal.Classes.FAQ
{
    public class FaqManager : IFaqManager
    {
        private IAsyncDataDocumentSession RavenSession { get; set; }

        public FaqManager(IAsyncDataDocumentSession session)
        {
            RavenSession = session;
        }

        public async Task AddNewFaq(FaqObject newFaq)
        {
            await RavenSession.StoreAsync(newFaq);
        }

        public async Task<FaqObject> GetFaq(string id)
        {
            return await RavenSession.LoadAsync<FaqObject>(id);
        }

        public async void DeleteFaq(string id)
        {
            var obj = await GetFaq(id);
            RavenSession.Delete(obj);
        }

        public async Task<IList<FaqObject>> GetFaqList()
        {
            return await RavenSession.Query<FaqObject>().Customize(x => x.WaitForNonStaleResultsAsOfNow()).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await RavenSession.SaveChangesAsync();
            RavenSession.Dispose();
        }

        public async Task UpdateFaq(FaqObject updatedFaqObject)
        {
            var obj = await GetFaq(updatedFaqObject.Id);
            obj.Answer = updatedFaqObject.Answer;
            obj.Question = updatedFaqObject.Question;
            await RavenSession.StoreAsync(obj);
        }
    }
}