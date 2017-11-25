using AppMancsXamarinForms.FileStoreAndLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AppMancsXamarinForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            string success = fileStoreAndLoading.GetSomethingText("login.txt");

            if (!String.IsNullOrEmpty(success))
            {
                var page = new AppMancsXamarinForms.MainPage();

                MainPage = new NavigationPage(page);

                NavigationPage.SetHasNavigationBar(page, false);
            }
            else
            {
                var page = new AppMancsXamarinForms.LoginPage();

                MainPage = new NavigationPage(page);

                NavigationPage.SetHasNavigationBar(page, false);
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
