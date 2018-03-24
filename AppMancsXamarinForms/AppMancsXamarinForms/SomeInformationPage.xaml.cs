using AppMancsXamarinForms.BLL.ViewModel;
using FacebookLogin.Models;
using Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SomeInformationPage : ContentPage
    {
        private FacebookProfile facebookProfile = new FacebookProfile();

        public SomeInformationPage(FacebookProfile facebookProfile)
        {
            this.facebookProfile = facebookProfile;

            InitializeComponent();

            GlobalVariables.pathf = facebookProfile.Picture.Data.Url;

            profilePictureImage.Source = ImageSource.FromUri(new Uri(facebookProfile.Picture.Data.Url));

            lastnameEntry.Text = facebookProfile.LastName;
            firstnameEntry.Text = facebookProfile.FirstName;
        }

        private async void changePictureButton_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(English.Failed(), English.NoPicking(), English.OK());
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            GlobalVariables.mediaFile = file;

            if (file == null) return;

            GlobalVariables.f = file.GetStream();
            GlobalVariables.pathf = file.Path;

            profilePictureImage.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async void signupButton_Clicked(object sender, EventArgs e)
        {
            User user = new User()
            {
                Email = emailEntry.Text,
                Password = pwEntry.Text,
                FirstName = firstnameEntry.Text,
                LastName = lastnameEntry.Text,
                FacebookId = facebookProfile.Id,
                ProfilePictureURL = GlobalVariables.pathf
            };

            if (GlobalVariables.f is null)
            {
                user.ProfilePictureURL = GlobalVariables.pathf;
            }
            else
            {
                user.ProfilePictureURL = await GlobalVariables.signupPageViewModel.UploadFileAsync(GlobalVariables.pathf, GlobalVariables.f);
            }

            string success = GlobalVariables.signupPageViewModel.SignUp(user);

            if (!String.IsNullOrEmpty(success))
            {
                await DisplayAlert(English.Failed(),success,English.OK());
            }
            else
            {
                GlobalVariables.ActualUser = user;
                GlobalVariables.ActualUsersEmail = user.Email;

                await Navigation.PopToRootAsync();
            }
        }
    }
}