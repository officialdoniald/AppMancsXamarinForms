using AppMancsXamarinForms.BLL.Helper;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class OtherFragmentViewModel
    {
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
                    return English.SomethingWentWrong();
                }
            }

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeleteAccount(UserID);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                return English.Empty();
            }
        }
    }
}
