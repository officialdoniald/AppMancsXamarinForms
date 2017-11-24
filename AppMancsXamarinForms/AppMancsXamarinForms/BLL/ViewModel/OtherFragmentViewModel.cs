using AppMancsXamarinForms.BLL.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class OtherFragmentViewModel
    {
        private English language = new English();

        public string DeleteAccount(string EMAIL)
        {
            var user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL);

            int UserID = user.id;

            var petlist = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetsByUserID(UserID);

            foreach (var item in petlist)
            {
                bool siker = DependencyService.Get<IDBAccess.IBlobStorage>().DeletePetpictures(item.id);

                if (!siker)
                {
                    return language.SomethingWentWrong();
                }
            }

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeleteAccount(UserID);

            if (!success)
            {
                return language.SomethingWentWrong();
            }
            else
            {
                return language.Empty();
            }
        }
    }
}
