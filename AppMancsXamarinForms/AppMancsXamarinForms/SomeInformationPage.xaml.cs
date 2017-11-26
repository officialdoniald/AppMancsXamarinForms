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

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SomeInformationPage : ContentPage
    {
        private MediaFile mediaFile;

        private Stream f;

        private string pathf = "";

        private FacebookProfile facebookProfile = new FacebookProfile();

        SignupPageViewModel signupPageViewModel = new SignupPageViewModel();

        public SomeInformationPage(FacebookProfile facebookProfile)
        {
            this.facebookProfile = facebookProfile;

            InitializeComponent();

            pathf = facebookProfile.Picture.Data.Url;

            profilePictureImage.Source = ImageSource.FromUri(new Uri(facebookProfile.Picture.Data.Url));

            lastnameEntry.Text = facebookProfile.LastName;
            firstnameEntry.Text = facebookProfile.FirstName;
        }

        private async void changePictureButton_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Warning", "No picking available, please try again later!", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            mediaFile = file;

            if (file == null) return;

            f = file.GetStream();
            pathf = file.Path;

            profilePictureImage.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async void signupButton_Clicked(object sender, EventArgs e)
        {
            User user = new User()
            {
                Email = emailEntry.Text.ToLower(),
                Password = pwEntry.Text,
                FirstName = firstnameEntry.Text,
                LastName = lastnameEntry.Text,
                FacebookId = facebookProfile.Id,
                ProfilePictureURL = pathf
            };

            if (f is null)
            {
                user.ProfilePictureURL = pathf;
            }
            else
            {
                user.ProfilePictureURL = await signupPageViewModel.UploadFileAsync(pathf, f);
            }

            string success = signupPageViewModel.SignUp(user);

            if (!String.IsNullOrEmpty(success))
            {
                //HIBA
            }
            else
            {
                await Navigation.PopToRootAsync();
            }
        }
    }
}