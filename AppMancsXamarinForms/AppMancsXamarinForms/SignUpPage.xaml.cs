using Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;
using System.Threading.Tasks;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private string asd = "";

        public SignUpPage()
        {
            InitializeComponent();
        }

        private async Task signupButton_ClickedAsync(object sender, EventArgs e)
        {
            await Task.Run(()=>{
                Registration();
            });

            if (!String.IsNullOrEmpty(asd))
            {
                await DisplayAlert(English.Failed(), asd, English.OK());
                uploadActivity.IsRunning = false;
            }
            else
            {
                string sentMail = DependencyService.Get<IMailerInj>().SendMail(emailEntry.Text, string.Empty);

                await Navigation.PopToRootAsync();
            }

            signupButton.IsEnabled = true;
            uploadActivity.IsRunning = false;
        }

        private void Registration(){
            
            Device.BeginInvokeOnMainThread(() => {
                signupButton.IsEnabled = false;
                uploadActivity.IsRunning = true;
            });

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
            asd = success;
        }

        void Handle_CompletedOnEmailEntry(object sender, System.EventArgs e)
        {
            pwEntry.Focus();
        }

        void Handle_CompletedOnLastNameEntry(object sender, System.EventArgs e)
        {
            firstnameEntry.Focus();
        }

        void Handle_CompletedOnFirstNameEntry(object sender, System.EventArgs e)
        {
            emailEntry.Focus();
        }

        async Task Handle_CompletedOnPasswordEntry(object sender, System.EventArgs e)
        {
            await signupButton_ClickedAsync(this, new EventArgs());
        }

        //private async void loginFacebookButton_Clicked(object sender, EventArgs e)
        //{
        //    DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();

        //    await Navigation.PushAsync(new FacebookLogin.Views.FacebookProfileCsPage());
        //}

    }
}