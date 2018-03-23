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

        private async System.Threading.Tasks.Task signupButton_ClickedAsync(object sender, EventArgs e)
        {
            signupButton.IsEnabled = false;

            User user = new User()
            {
                Email = emailEntry.Text,
                Password = pwEntry.Text,
                FirstName = firstnameEntry.Text,
                LastName = lastnameEntry.Text,
                FacebookId = "",
                ProfilePictureURL = ""
            };

            string success = GlobalVariables.signupPageViewModel.SignUp(user);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(),success,English.OK());
            }
            else
            {
                await Navigation.PopToRootAsync();
            }
            signupButton.IsEnabled = true;
        }

        private async void loginFacebookButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();

            await Navigation.PushAsync(new FacebookLogin.Views.FacebookProfileCsPage());
        }
    }
}