using AppMancsXamarinForms.BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this,false);
            NavigationPage.SetHasNavigationBar(this, false);

            var homePage = new HomePage();
            
            var navigationHomePage = new NavigationPage(homePage);
            //navigationHomePage.Title = "Home";
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

            Children.Add(navigationHomePage);
            Children.Add(navigationSearchPage);
            Children.Add(navigationUploadPhotoPage);
            Children.Add(navigationMyAccountPage);
        }
    }
}
