using AppMancsXamarinForms.BLL.Languages;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class ForgotPasswordPageViewModel
    {
        private English language = new English();
        
        private Segédfüggvények segédfüggvények =
            new Segédfüggvények();

        public string SendEmail(string EMAIL)
        {
            if (String.IsNullOrEmpty(EMAIL))
            {
                return language.GiveYourEmail();
            }

            EMAIL = EMAIL.ToLower();

            User user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL);

            if (user.Email is null)
            {
                return language.NoAcoountFoundWithThisEmail();
            }

            user.Password = segédfüggvények.RandomString(8, false);

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().UpdateUser(user.id, user);

            //TODO send E-Mail

            return language.Empty();
        }
    }
}
