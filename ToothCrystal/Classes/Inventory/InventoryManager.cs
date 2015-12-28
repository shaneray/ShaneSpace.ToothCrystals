using Raven.Client;
using System.Linq;
using System.Threading.Tasks;

namespace ToothCrystal.Classes.Inventory
{
    public class InventoryManager : IInventoryManager
    {
        private IAsyncDataDocumentSession RavenSession { get; set; }

        public InventoryManager(IAsyncDataDocumentSession session)
        {
            RavenSession = session;
        }

        public async Task<string> AddNewInventoryItem(InventoryItem newInventoryItem)
        {
            await RavenSession.StoreAsync(newInventoryItem);
            return newInventoryItem.Id;
        }

        public async Task<InventoryItem> GetInventoryItem(string id)
        {
            return await RavenSession.LoadAsync<InventoryItem>(id);
        }

        public async Task DeleteInventoryItem(string id)
        {
            var obj = await GetInventoryItem(id);
            RavenSession.Delete(obj);
        }

        public async Task<System.Collections.Generic.IList<InventoryItem>> GetInventoryList()
        {
            RavenQueryStatistics stats;
            return await RavenSession.Query<InventoryItem>("SortByMyCode").Statistics(out stats).Customize(x => x.WaitForNonStaleResultsAsOfNow()).OrderBy(i => i.MyCode).ToListAsync();
        }

        public async Task<string> UpdateInventoryItem(InventoryItem updatedInventoryItem)
        {
            var obj = await GetInventoryItem(updatedInventoryItem.Id);
            obj.ProductCode = updatedInventoryItem.ProductCode;
            obj.Color = updatedInventoryItem.Color;
            obj.Description = updatedInventoryItem.Description;
            obj.QtyInStock = updatedInventoryItem.QtyInStock;
            obj.ImageExtension = updatedInventoryItem.ImageExtension;
            obj.MyCode = updatedInventoryItem.MyCode;

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