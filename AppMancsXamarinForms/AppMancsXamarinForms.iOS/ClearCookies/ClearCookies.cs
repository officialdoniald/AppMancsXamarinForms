using Foundation;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(AppMancsXamarinForms.iOS.ClearCookies.ClearCookies))]
namespace AppMancsXamarinForms.iOS.ClearCookies
{
    public class ClearCookies : IClearCookies
    {
        public void ClearAllWebAppCookies()
        {
            foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
            {
                NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
            }
        }
    }
}
