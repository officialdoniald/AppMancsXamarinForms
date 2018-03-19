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
            string success = GlobalVariables.otherFragmentViewModel.DeleteAccount(GlobalVariables.ActualUsersEmail);

            if (!String.IsNullOrEmpty(success))
            {
                await GlobalVariables.ContentPageFunctions.CreateNegativDisplayAlert(success);
            }
            else
            {
                var page = new LoginPage();

                await Navigation.PushAsync(page);

                NavigationPage.SetHasNavigationBar(page, false);
            }
        }

        private void loguotButton_Clicked(object sender, EventArgs e)
        {
            FileStoreAndLoading.InsertToFile(GlobalVariables.logintxt,String.Empty);

            var page = new NavigationPage(new LoginPage());

            Navigation.PushModalAsync(page);
        }
    }
}