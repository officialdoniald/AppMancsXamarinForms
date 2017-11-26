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
    public class FacebookProfilePageViewModel
    {
        English english = new English();

        public string isThereAnyUser(string facebookid)
        {
            var user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByFacebookID(facebookid);

            if (user is null)
            {
                return english.NoAccountFindWithThisFacebookAccount();
            }
            else
            {
                return user.Email;
            }
        }

        public string ChangeFacebookId(string facebookid, string facebookprofilepictureurl, string userEmail)
        {
            var user1 = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByFacebookID(facebookid);

            if (user1 is null)
            {
                var user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByFacebookID(userEmail);

                user.FacebookId = facebookid;
                user.ProfilePictureURL = facebookprofilepictureurl;

                var success = DependencyService.Get<IDBAccess.IBlobStorage>().UpdateUser(user.id, user);

                if (!success)
                {
                    return english.SomethingWentWrong();
                }
                else
                {
                    return String.Empty;
                }
            }
            else
            {
                return english.AlreadyHaveThisFacebookAcoount();
            }
        }
    }
}
