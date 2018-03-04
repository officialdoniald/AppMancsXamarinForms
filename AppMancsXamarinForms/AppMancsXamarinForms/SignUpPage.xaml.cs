using Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void signupButton_Clicked(object sender, EventArgs e)
        {
            User user = new User()
            {
                Email = emailEntry.Text.ToLower(),
                Password = pwEntry.Text,
                FirstName = firstnameEntry.Text,
                LastName = lastnameEntry.Text,
                FacebookId = "",
                ProfilePictureURL = ""
            };

            string success = GlobalVariables.signupPageViewModel.SignUp(user);

            if (!String.IsNullOrEmpty(success))
            {
                //TODO
            }
            else
            {
                Navigation.PopToRootAsync();
            }
        }

        private async void loginFacebookButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();

            await Navigation.PushAsync(new FacebookLogin.Views.FacebookProfileCsPage());
        }
    }
}