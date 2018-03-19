using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppMancsXamarinForms.BLL.Helper;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class LoginPageViewModel
    {
        private Segédfüggvények segédfüggvények =
            new Segédfüggvények();

        //1:üres mind a kettő
        //2:első üres
        //3:második üres
        //4:nem jó az email formátuma
        //5:a pw nem olyan hosszban van 6-16
        //6:nem található ilyen email a DBben vagy rossz a jelszó
        //7:minden jó
        public string Login(string EMAIL, string PASSWORD)
        {
            if (String.IsNullOrEmpty(EMAIL) && String.IsNullOrEmpty(PASSWORD))
            {
                return English.YouHaveToFillAllEntries();
            }
            if (String.IsNullOrEmpty(EMAIL))
            {
                return English.EmailIsEmpty();
            }
            if (String.IsNullOrEmpty(PASSWORD))
            {
                return English.PasswordIsEmpty();
            }
            if (!segédfüggvények.IsValidEmailAddress(EMAIL.ToLower()))
            {
                return English.BadEmailFormat();
            }
            if (PASSWORD.Length < 6 || PASSWORD.Length > 16)
            {
                return English.BadPasswordLength();
            }

            var user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL.ToLower());

            if (user is null || user.Password != PASSWORD)
            {
                return English.BadPasswordOrEmail();
            }

            return English.Empty();
        }
    }
}
