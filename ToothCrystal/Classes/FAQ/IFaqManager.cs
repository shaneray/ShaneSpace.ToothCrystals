using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToothCrystal.Classes.FAQ
{
    public interface IFaqManager
    {
        Task AddNewFaq(FaqObject newFaq);
        Task<FaqObject> GetFaq(string id);
        void DeleteFaq(string id);
        Task<IList<FaqObject>> GetFaqList();
        Task UpdateFaq(FaqObject updatedFaqObject);
        Task SaveChanges();
    }
}