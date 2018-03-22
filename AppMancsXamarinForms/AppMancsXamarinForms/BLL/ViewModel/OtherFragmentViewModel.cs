using AppMancsXamarinForms.BLL.Helper;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class OtherFragmentViewModel
    {
        public string DeleteAccount()
        {
            foreach (var item in GlobalVariables.Mypetlist)
            {
                string siker = GlobalVariables.updatePetFragmentViewModel.DeletePet(item.petid);

                if (!string.IsNullOrEmpty(siker))
                {
                    return English.SomethingWentWrong();
                }
            }

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeleteAccount(GlobalVariables.ActualUser.id);

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
