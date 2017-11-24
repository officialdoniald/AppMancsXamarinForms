using AppMancsXamarinForms.BLL.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class LoginPageViewModel
    {
        private English language = new English();

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
                return language.YouHaveToFillAllEntries();
            }
            if (String.IsNullOrEmpty(EMAIL))
            {
                return language.EmailIsEmpty();
            }
            if (String.IsNullOrEmpty(PASSWORD))
            {
                return language.PasswordIsEmpty();
            }
            if (!segédfüggvények.IsValidEmailAddress(EMAIL.ToLower()))
            {
                return language.BadEmailFormat();
            }
            if (PASSWORD.Length < 6 || PASSWORD.Length > 16)
            {
                return language.BadPasswordLength();
            }

            var user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL.ToLower());

            if (user is null || user.Password != PASSWORD)
            {
                return language.BadPasswordOrEmail();
            }

            return language.Empty();
        }
    }
}
