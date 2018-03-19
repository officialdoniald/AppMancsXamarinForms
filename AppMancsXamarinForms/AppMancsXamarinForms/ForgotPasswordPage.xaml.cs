using AppMancsXamarinForms.BLL.Helper;
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

        private async System.Threading.Tasks.Task sendNewPassword_ClickedAsync(object sender, EventArgs e)
        {
            ForgotPasswordPageViewModel forgotPasswordPageViewModel =
                   new ForgotPasswordPageViewModel();

            string success = forgotPasswordPageViewModel.SendEmail(emailEntry.Text);

            if (String.IsNullOrEmpty(success))
            {
                await Navigation.PopToRootAsync();
            }
            else
            {
                await GlobalVariables.ContentPageFunctions.CreateNegativDisplayAlert(success);
            }
        }
    }
}