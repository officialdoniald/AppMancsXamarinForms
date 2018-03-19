﻿using AppMancsXamarinForms.BLL.Helper;
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
    public class SignupPageViewModel
    {
        public async Task<string> UploadFileAsync(string pathf, Stream f)
        {
            string uniqueBlobName = await DependencyService.Get<IBlobStorage.IBlobStorage>().UploadFileAsync(pathf, f);

            uniqueBlobName = "https://officialdoniald.blob.core.windows.net/appmancs/" + uniqueBlobName;

            return uniqueBlobName;
        }

        public string SignUp(User user)
        {
            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.FirstName) ||
                String.IsNullOrEmpty(user.LastName) || String.IsNullOrEmpty(user.Password))
            {
                return English.YouHaveToFillAllEntries();
            }
            if (user.Password.Length < 6 && user.Password.Length > 16)
            {
                return English.BadPasswordLength();
            }

            user.Email = user.Email.ToLower();

            var isItAUser = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(user.Email);

            if (isItAUser.Email is null)
            {
                var success = DependencyService.Get<IDBAccess.IBlobStorage>().InsertUser(user);

                if (success)
                {
                    return English.Empty();
                }
            }
            else
            {
                return English.ThisEmailIsExist();
            }

            return English.SomethingWentWrong();
        }
    }
}
