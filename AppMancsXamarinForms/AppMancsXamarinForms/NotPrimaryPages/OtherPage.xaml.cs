using AppMancsXamarinForms.FileStoreAndLoad;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherPage : ContentPage
    {
        public OtherPage()
        {
            InitializeComponent();
        }

        private async System.Threading.Tasks.Task deleteAcoountPageButton_ClickedAsync(object sender, EventArgs e)
        {
            loguotButton.IsEnabled = false;
            deleteAcoountPageButton.IsEnabled = false;
            deleteActivity.IsRunning = true;

            string success = GlobalVariables.otherFragmentViewModel.DeleteAccount();

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(),success,English.OK());
            }
            else
            {
                FileStoreAndLoading.InsertToFile(GlobalVariables.logintxt, String.Empty);
                
                var page = new LoginPage();

                await Navigation.PushModalAsync(new NavigationPage(page)
                {
                    BarBackgroundColor = Color.FromHex(GlobalVariables.InitColor),
                    BarTextColor = Color.White
                });
            }
            loguotButton.IsEnabled = true;
            deleteAcoountPageButton.IsEnabled = true;
            deleteActivity.IsRunning = false;
        }

        private async System.Threading.Tasks.Task loguotButton_Clicked(object sender, EventArgs e)
        {
            loguotButton.IsEnabled = false;

            FileStoreAndLoading.InsertToFile(GlobalVariables.logintxt,String.Empty);

            var page = new LoginPage();

            await Navigation.PushModalAsync(new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex(GlobalVariables.InitColor),
                BarTextColor = Color.White
            });

            loguotButton.IsEnabled = true;
        }
    }
}