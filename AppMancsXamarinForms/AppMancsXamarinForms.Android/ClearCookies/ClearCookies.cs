using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;

[assembly: Xamarin.Forms.Dependency(typeof(AppMancsXamarinForms.Droid.ClearCookies.ClearCookies))]
namespace AppMancsXamarinForms.Droid.ClearCookies
{
    public class ClearCookies : IClearCookies
    {
        public void ClearAllWebAppCookies()
        {
            CookieManager.Instance.RemoveAllCookie();
        }
    }
}