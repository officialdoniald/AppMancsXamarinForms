using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class SeeAnOwnerProfileViewModel
    {
        public User GetUser(int userid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByID(userid);
        }

        public User GetUserByEmail(string userEmail)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail);
        }

        public List<Pet> GetPet(int userid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetPetsByUserID(userid);
        }
    }
}
