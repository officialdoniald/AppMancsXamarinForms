using System;
using FacebookLogin.Models;
using FacebookLogin.ViewModels;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;
using AppMancsXamarinForms;
using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using AppMancsXamarinForms.BLL.Languages;
using AppMancsXamarinForms.NotPrimaryPages;
using AppMancsXamarinForms.BLL.Helper;

namespace FacebookLogin.Views
{
    public class FacebookProfileCsPage : ContentPage
    {

        /// <summary>
        /// Make sure to get a new ClientId from:
        /// https://developers.facebook.com/apps/
        /// </summary>
        private string ClientId = "142251139871410";

        public FacebookProfileCsPage()
        {
            BindingContext = new FacebookViewModel();

            Title = "Facebook Profile";
            BackgroundColor = Color.White;

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);

                SetPageContent(vm.FacebookProfile);
            }
        }

        private async void SetPageContent(FacebookProfile facebookProfile)
        {
            FacebookProfilePageViewModel facebookProfilePageViewModel = new FacebookProfilePageViewModel();

            var page = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];

            if (page is LoginPage)
            {
                string success = facebookProfilePageViewModel.isThereAnyUser(facebookProfile.Id);

                English english = new English();

                if (success == english.NoAccountFindWithThisFacebookAccount())
                {
                    //TODO
                    DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();

                    await Navigation.PopToRootAsync();
                }
                else
                {
                    FileStoreAndLoading.InsertToFile(GlobalVariables.logintxt, success);

                    //var pages = Navigation.NavigationStack;

                    //while (pages.Count > 1)
                    //{
                    //    Navigation.RemovePage(pages.Last());
                    //}

                    Navigation.PushModalAsync(new JustActivityIndicator("facebook"));
                }
            }
            else if (page is SignUpPage)
            {
                English english = new English();

                DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();

                var success = facebookProfilePageViewModel.isThereAnyUser(facebookProfile.Id);

                if (success == english.NoAccountFindWithThisFacebookAccount())
                {
                    await Navigation.PushAsync(new SomeInformationPage(facebookProfile));
                }
                else
                {
                    await Navigation.PopAsync();
                }
            }
            else if (page is UpdateProfilePage)
            {
                DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();

                FileStoreAndLoading.GetSomethingText(GlobalVariables.logintxt);

                var success = facebookProfilePageViewModel.ChangeFacebookId(facebookProfile.Id, facebookProfile.Picture.Data.Url, GlobalVariables.ActualUsersEmail);

                if (String.IsNullOrEmpty(success))
                {
                    await Navigation.PushAsync(new MyAccountPage());
                }
                else
                {
                    //TODO
                    await Navigation.PushAsync(new UpdateProfilePage());
                }
            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}