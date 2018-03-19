using AppMancsXamarinForms.BLL.Helper;
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
        public string isThereAnyUser(string facebookid)
        {
            var user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByFacebookID(facebookid);

            if (user is null)
            {
                return English.NoAccountFindWithThisFacebookAccount();
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
                    return English.SomethingWentWrong();
                }
                else
                {
                    return String.Empty;
                }
            }
            else
            {
                return English.AlreadyHaveThisFacebookAcoount();
            }
        }
    }
}
