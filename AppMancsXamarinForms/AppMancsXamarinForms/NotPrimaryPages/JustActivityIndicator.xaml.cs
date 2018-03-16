using System;
using AppMancsXamarinForms.BLL.Helper;
using Xamarin.Forms;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    public partial class JustActivityIndicator : ContentPage
    {
        private string isEmpty = String.Empty;
        
        public JustActivityIndicator()
        {
            InitializeComponent();
        }

        public JustActivityIndicator(string facebookOrLogin)
        {
            InitializeComponent();

            isEmpty = facebookOrLogin;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!String.IsNullOrEmpty(isEmpty))
            {
                GlobalVariables.InitializeUser();

                GlobalVariables.InitializeUsersEmailVariable();

                GlobalVariables.InitializeTheMyPetList();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                });
            }
            else
            {
                GlobalVariables.InitializeTheMyPetList();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                });
            }
        }
    }
}