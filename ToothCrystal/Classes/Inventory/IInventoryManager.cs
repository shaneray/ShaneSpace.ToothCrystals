using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToothCrystal.Classes.Inventory
{
    public interface IInventoryManager
    {
        Task<string> AddNewInventoryItem(InventoryItem newInventoryItem);
        Task<InventoryItem> GetInventoryItem(string id);
        Task DeleteInventoryItem(string id);
        Task<IList<InventoryItem>> GetInventoryList();
        Task<string> UpdateInventoryItem(InventoryItem updatedInventoryItem);
        Task SaveChanges();
    }
}
