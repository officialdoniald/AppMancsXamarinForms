using AppMancsXamarinForms.BLL.Languages;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class UpdateProfileFragmentViewModel
    {
        private English language = new English();

        public User GetUserByEmail(string userEmail)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail);
        }

        private string UpdateUser(User user)
        {
            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().UpdateUser(user.id, user);

            if (success)
            {
                return language.Empty();
            }
            else
            {
                return language.SomethingWentWrong();
            }
        }

        public string UpdateEmail(string EMAIL, string newEmail)
        {
            User userFromDB = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL);

            if (EMAIL == newEmail)
            {
                return language.ThisEmailIsYourEmail();
            }
            if (String.IsNullOrEmpty(newEmail))
            {
                userFromDB.Email = newEmail;

                User checkEmailExist = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(newEmail);

                if (!String.IsNullOrEmpty(checkEmailExist.Email))
                {
                    return language.ThisEmailIsExist();
                }
                else
                {
                    //új emailt küldeni az új címre

                    return UpdateUser(userFromDB);
                }

            }

            return language.SomethingWentWrong();
        }

        public string UpdateProfile(string firstname, string lastname, string EMAIL)
        {
            User user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL);

            if (!String.IsNullOrEmpty(firstname))
            {
                user.FirstName = firstname;
            }
            if (!String.IsNullOrEmpty(lastname))
            {
                user.LastName = lastname;
            }

            return UpdateUser(user);
        }

        public async Task<string> UpdateProfilePicture(string uri, Stream stream, string EMAIL)
        {
            User user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL);

            if (!String.IsNullOrEmpty(uri))
            {
                string uniqueBlobName = await DependencyService.Get<IBlobStorage.IBlobStorage>().UploadFileAsync(uri, stream);

                uniqueBlobName = "https://officialdoniald.blob.core.windows.net/appmancs/" + uniqueBlobName;

                user.ProfilePictureURL = uniqueBlobName;
            }
            else user.ProfilePictureURL = "";

            return UpdateUser(user);
        }

        public string UpdatePassword(string oldpassword, string newPassword, string EMAIL)
        {
            if (String.IsNullOrEmpty(oldpassword) || String.IsNullOrEmpty(newPassword))
            {
                return language.ThisEmailIsExist();
            }

            User userFromDB = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL);

            if (userFromDB.Password != oldpassword)
            {
                return language.BadPasswordLength();
            }

            if (newPassword.Length < 6 && newPassword.Length > 16)
            {
                return language.BadPasswordLength();
            }

            userFromDB.Password = newPassword;

            return UpdateUser(userFromDB);
        }
    }
}
