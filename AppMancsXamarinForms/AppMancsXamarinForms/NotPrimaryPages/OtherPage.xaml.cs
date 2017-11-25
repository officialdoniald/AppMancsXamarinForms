using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OtherPage : ContentPage
    {
        private string userEmail = "";

        private OtherFragmentViewModel otherFragmentViewModel =
            new OtherFragmentViewModel();

        public OtherPage()
        {
            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            InitializeComponent();
        }

        private void deleteAcoountPageButton_Clicked(object sender, EventArgs e)
        {
            string success = otherFragmentViewModel.DeleteAccount(userEmail);

            if (!String.IsNullOrEmpty(success))
            {
                //HIBA
            }
            else
            {
                var page = new LoginPage();

                Navigation.PushAsync(page);

                NavigationPage.SetHasNavigationBar(page, false);
            }
        }

        private void loguotButton_Clicked(object sender, EventArgs e)
        {
            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            fileStoreAndLoading.InsertToFile("login.txt","");

            var page = new LoginPage();

            Navigation.PushAsync(page);

            NavigationPage.SetHasNavigationBar(page, false);
        }
    }
}