using AppMancsXamarinForms.BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                navigationHomePage.Icon = "home.png";
                navigationHomePage.BarBackgroundColor = Color.FromHex("#FFCBB6");
                navigationHomePage.BarTextColor = Color.White;
                NavigationPage.SetHasNavigationBar(homePage, false);

                var searchPage = new SearchPage();
                var navigationSearchPage = new NavigationPage(searchPage);
                //navigationSearchPage.Title = "Keresés";
                navigationSearchPage.Icon = "search.png";
                navigationSearchPage.BarBackgroundColor = Color.FromHex("#FFCBB6");
                navigationSearchPage.BarTextColor = Color.White;
                NavigationPage.SetHasNavigationBar(searchPage, false);

                var uploadPhotoPage = new UploadPhotoPage();
                var navigationUploadPhotoPage = new NavigationPage(uploadPhotoPage);
                //navigationUploadPhotoPage.Title = "Fotó";
                navigationUploadPhotoPage.Icon = "camera.png";
                navigationUploadPhotoPage.BarBackgroundColor = Color.FromHex("#FFCBB6");
                navigationUploadPhotoPage.BarTextColor = Color.White;
                NavigationPage.SetHasNavigationBar(uploadPhotoPage, false);

                var myAccountPage = new MyAccountPage();
                var navigationMyAccountPage = new NavigationPage(myAccountPage);
                //navigationMyAccountPage.Title = "Fiók";
                navigationMyAccountPage.Icon = "profile.png";
                navigationMyAccountPage.BarBackgroundColor = Color.FromHex("#FFCBB6");
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
