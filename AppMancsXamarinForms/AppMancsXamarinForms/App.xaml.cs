using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            GlobalVariables.InitializeUsersEmail();

            // Handle when your app starts
            if (!GlobalVariables.HaveToLogin)
            {
                GlobalVariables.InitializeUser();

                MainPage = new NotPrimaryPages.JustActivityIndicator();
            }
            else
            {
                var page = new AppMancsXamarinForms.LoginPage();

                MainPage = new NavigationPage(page);

                NavigationPage.SetHasNavigationBar(page, false);
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
