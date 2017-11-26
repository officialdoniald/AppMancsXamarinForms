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
            
            var homePage = new HomePage();
            
            var navigationHomePage = new NavigationPage(homePage);
            navigationHomePage.Title = "Home";

            NavigationPage.SetHasNavigationBar(homePage, false);

            var searchPage = new SearchPage();

            var navigationSearchPage = new NavigationPage(searchPage);
            navigationSearchPage.Title = "Keresés";

            NavigationPage.SetHasNavigationBar(searchPage, false);

            var uploadPhotoPage = new UploadPhotoPage();

            var navigationUploadPhotoPage = new NavigationPage(uploadPhotoPage);
            navigationUploadPhotoPage.Title = "Fotó";

            NavigationPage.SetHasNavigationBar(uploadPhotoPage, false);

            var myAccountPage = new MyAccountPage();

            var navigationMyAccountPage = new NavigationPage(myAccountPage);
            navigationMyAccountPage.Title = "Fiók";

            NavigationPage.SetHasNavigationBar(myAccountPage, false);

            Children.Add(navigationHomePage);
            Children.Add(navigationSearchPage);
            Children.Add(navigationUploadPhotoPage);
            Children.Add(navigationMyAccountPage);
        }
    }
}
