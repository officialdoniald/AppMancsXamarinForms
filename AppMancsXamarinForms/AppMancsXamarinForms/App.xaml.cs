﻿using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;
using Plugin.Connectivity;

namespace AppMancsXamarinForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            GlobalVariables.NormalLabel = (Style)Resources["NormalLabel"];
            GlobalVariables.NavigationPageStyle = (Style)Resources["NavigationPageStyle"];
        }

        protected override void OnStart()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                while (!CrossConnectivity.Current.IsConnected) { }
            }
            GlobalVariables.InitializeUsersEmail();

            if (!GlobalVariables.HaveToLogin)
            {
                GlobalVariables.InitializeUser();

                MainPage = new NotPrimaryPages.JustActivityIndicator();
            }
            else
            {
                var page = new AppMancsXamarinForms.LoginPage();

                MainPage = new NavigationPage(page){
                    Style = GlobalVariables.NavigationPageStyle
                };

                NavigationPage.SetHasNavigationBar(page, false);
            }
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
