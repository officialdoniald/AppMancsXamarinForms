using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel loginPageViewModel =
            new LoginPageViewModel();

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            string success = loginPageViewModel.Login(emailEntry.Text, pwEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert("Figyelmeztetés", success, "OK");
            }
            else
            {
                FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

                fileStoreAndLoading.InsertToFile("login.txt", emailEntry.Text);

                await Navigation.PushAsync(new MainPage());
            }
        }

        private async void signUpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private void loginFacebookButton_Clicked(object sender, EventArgs e)
        {

        }

        private async void forgotPasswordButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }
    }
}