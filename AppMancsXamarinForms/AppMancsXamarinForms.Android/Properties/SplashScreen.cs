
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppMancsXamarinForms.Droid
{
    [Activity(Label = "Chara", MainLauncher = true, Theme = "@style/MainTheme", NoHistory = false)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashScreenLayout);
        }

		protected async override void OnResume()
		{
            base.OnResume();
            await StartSplashScreen();
		}

        async Task StartSplashScreen(){

            await Task.Delay(3000);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
	}
}
