using System.Collections.Generic;
using System.Linq;
using ToothCrystal.Core.Classes;

namespace ToothCrystal.Core
{
    public class SiteUsers
    {
        IDataDocumentSession RavenSession { get; set; }

        public SiteUsers(IDataDocumentSession session)
        {
            RavenSession = session;
        }

        public IList<ApplicationUser> GetUserList()
        {
            return RavenSession.Query<ApplicationUser>().ToList();
        }

    }
}
