using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;
using AppMancsXamarinForms.NotPrimaryPages;
using Plugin.Connectivity;

namespace AppMancsXamarinForms
{
    public partial class MainPage : TabbedPage
    {
        private bool wasNotConn = false;
        
        public MainPage()
        {
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                if (!CrossConnectivity.Current.IsConnected && !wasNotConn)
                {
                    wasNotConn = true;
                 
                    await Navigation.PushModalAsync(new NoConnection());
                }
                else
                {
                    wasNotConn = false;
                }
            };

            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            Device.BeginInvokeOnMainThread(() =>
            {
                var homePage = new HomePage();

                var navigationHomePage = new NavigationPage(homePage);
                //navigationHomePage.Title = "Főmenü";
                navigationHomePage.Icon = GlobalVariables.homepng;
                navigationHomePage.BarBackgroundColor = Color.FromHex(GlobalVariables.InitColor);
                navigationHomePage.BarTextColor = Color.White;
                NavigationPage.SetHasNavigationBar(homePage, false);

                var searchPage = new SearchPage();
                var navigationSearchPage = new NavigationPage(searchPage);
                //navigationSearchPage.Title = "Keresés";
                navigationSearchPage.Icon = GlobalVariables.searchpng;
                navigationSearchPage.BarBackgroundColor = Color.FromHex(GlobalVariables.InitColor);
                navigationSearchPage.BarTextColor = Color.White;
                NavigationPage.SetHasNavigationBar(searchPage, false);

                var uploadPhotoPage = new UploadPhotoPage();
                var navigationUploadPhotoPage = new NavigationPage(uploadPhotoPage);
                //navigationUploadPhotoPage.Title = "Fotó";
                navigationUploadPhotoPage.Icon = GlobalVariables.camerapng;
                navigationUploadPhotoPage.BarBackgroundColor = Color.FromHex(GlobalVariables.InitColor);
                navigationUploadPhotoPage.BarTextColor = Color.White;
                NavigationPage.SetHasNavigationBar(uploadPhotoPage, false);

                var myAccountPage = new MyAccountPage();
                var navigationMyAccountPage = new NavigationPage(myAccountPage);
                //navigationMyAccountPage.Title = "Fiók";
                navigationMyAccountPage.Icon = GlobalVariables.profilepng;
                navigationMyAccountPage.BarBackgroundColor = Color.FromHex(GlobalVariables.InitColor);
                navigationMyAccountPage.BarTextColor = Color.White;
                NavigationPage.SetHasNavigationBar(myAccountPage, false);

                Children.Add(navigationHomePage);
                Children.Add(navigationSearchPage);
                Children.Add(navigationUploadPhotoPage);
                Children.Add(navigationMyAccountPage);
            });
        }
    }
}
