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
    public class SignupPageViewModel
    {
        private English language = new English();

        public string SignUp(User user)
        {
            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.FirstName) ||
                String.IsNullOrEmpty(user.LastName) || String.IsNullOrEmpty(user.Password))
            {
                return language.YouHaveToFillAllEntries();
            }
            if (user.Password.Length < 6 && user.Password.Length > 16)
            {
                return language.BadPasswordLength();
            }

            user.Email = user.Email.ToLower();

            var isItAUser = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(user.Email);

            if (isItAUser.Email is null)
            {
                var success = DependencyService.Get<IDBAccess.IBlobStorage>().InsertUser(user);

                if (success)
                {
                    return language.Empty();
                }
            }
            else
            {
                return language.ThisEmailIsExist();
            }

            return language.SomethingWentWrong();
        }
    }
}
