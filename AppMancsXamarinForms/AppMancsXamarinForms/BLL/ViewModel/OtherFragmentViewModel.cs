using AppMancsXamarinForms.BLL.Helper;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class OtherFragmentViewModel
    {
        public string DeleteAccount()
        {
            int i = 0;

            if (GlobalVariables.Mypetlist.Count != 0)
            {
                while (GlobalVariables.Mypetlist.Count != 0)
                {
                    string siker = GlobalVariables.updatePetFragmentViewModel.DeletePet(GlobalVariables.Mypetlist[i].petid);

                    if (!string.IsNullOrEmpty(siker))
                    {
                        return English.SomethingWentWrong();
                    }

                    i++;
                }
            }

            GlobalVariables.Mypetlist = new System.Collections.Generic.List<LocalDB.MyPetsList>();

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
