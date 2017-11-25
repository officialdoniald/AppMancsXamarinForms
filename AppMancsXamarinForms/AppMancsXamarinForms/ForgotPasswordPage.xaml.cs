using AppMancsXamarinForms.BLL.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private void sendNewPassword_Clicked(object sender, EventArgs e)
        {
            ForgotPasswordPageViewModel forgotPasswordPageViewModel =
                   new ForgotPasswordPageViewModel();

            string success = forgotPasswordPageViewModel.SendEmail(emailEntry.Text);

            if (String.IsNullOrEmpty(success))
            {
                Navigation.PopToRootAsync();
            }
            else
            {
                //hiba
            }
        }
    }
}