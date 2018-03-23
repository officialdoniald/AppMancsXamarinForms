﻿using AppMancsXamarinForms.BLL.Helper;
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
        private Segédfüggvények segédfüggvények =
            new Segédfüggvények();

        public string SendEmail(string EMAIL)
        {
            if (String.IsNullOrEmpty(EMAIL))
            {
                return English.GiveYourEmail();
            }

            EMAIL = EMAIL.ToLower();

            User user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(EMAIL);

            if (user.Email is null)
            {
                return English.NoAcoountFoundWithThisEmail();
            }

            user.Password = segédfüggvények.RandomString(8, false);

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().UpdateUser(user.id, user);

            if (!success)
            {
                return English.SomethingWentWrong();
            }

            string sentMail = DependencyService.Get<IMailerInj>().SendMail(user.Email,user.Password);

            return sentMail;
        }
    }
}
