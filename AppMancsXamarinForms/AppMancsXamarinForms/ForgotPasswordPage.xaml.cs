using AppMancsXamarinForms.BLL.Helper;
using AppMancsXamarinForms.BLL.ViewModel;
using System;
using System.Threading.Tasks;
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
            string success = GlobalVariables.forgotPasswordPageViewModel.SendEmail(emailEntry.Text);

            if (String.IsNullOrEmpty(success))
            {
                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert(English.Failed(),success,English.OK());
            }
        }

        async Task Handle_Completed(object sender, System.EventArgs e)
        {
            await sendNewPassword_ClickedAsync(this,new EventArgs());
        }
    }
}