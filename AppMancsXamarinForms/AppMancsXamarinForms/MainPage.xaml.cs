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
        public MainPage()
        {
            InitializeComponent();
            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await Navigation.PushModalAsync(new NoConnection());

                    await DisplayAlert("Failed", "You are offline!", "OK");
                }
            };
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);

            //var activityPage = new JustActivityIndicator();

            //var navigationactivityPage = new NavigationPage(activityPage);
            //navigationactivityPage.Icon = "home.png";

            //Children.Add(navigationactivityPage);

            Device.BeginInvokeOnMainThread(() =>
            {
                var homePage = new HomePage();

                var navigationHomePage = new NavigationPage(homePage);

                //navigationHomePage.Title = "Főmenü";
                navigationHomePage.Icon = "home.png";

                NavigationPage.SetHasNavigationBar(homePage, false);

                var searchPage = new SearchPage();

                var navigationSearchPage = new NavigationPage(searchPage);
                //navigationSearchPage.Title = "Keresés";
                navigationSearchPage.Icon = "search.png";

                NavigationPage.SetHasNavigationBar(searchPage, false);

                var uploadPhotoPage = new UploadPhotoPage();

                var navigationUploadPhotoPage = new NavigationPage(uploadPhotoPage);
                //navigationUploadPhotoPage.Title = "Fotó";
                navigationUploadPhotoPage.Icon = "camera.png";

                NavigationPage.SetHasNavigationBar(uploadPhotoPage, false);

                var myAccountPage = new MyAccountPage();

                var navigationMyAccountPage = new NavigationPage(myAccountPage);
                //navigationMyAccountPage.Title = "Fiók";
                navigationMyAccountPage.Icon = "profile.png";

                NavigationPage.SetHasNavigationBar(myAccountPage, false);

                //Children.RemoveAt(0);
                //Children.Insert(0, homePage);
                //CurrentPage = homePage;
                Children.Add(navigationHomePage);
                Children.Add(navigationSearchPage);
                Children.Add(navigationUploadPhotoPage);
                Children.Add(navigationMyAccountPage);
            });
        }
    }
}
