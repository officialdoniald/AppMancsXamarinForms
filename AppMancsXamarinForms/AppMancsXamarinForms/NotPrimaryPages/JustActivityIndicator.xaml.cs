using System;
using System.Collections.Generic;
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

        public JustActivityIndicator(bool connection)
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!String.IsNullOrEmpty(isEmpty))
            {
                GlobalVariables.InitializeUsersEmailVariable();

                GlobalVariables.InitializeUser();

                GlobalVariables.Mypetlist = new List<GlobalVariables.MyPetsList>();

                GlobalVariables.GetMyPets();

                GlobalVariables.SetMyPetListString();

                Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                GlobalVariables.GetMyPets();

                GlobalVariables.SetMyPetListString();

                Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
        }
    }
}