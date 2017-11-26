using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
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

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProfilePage : ContentPage
    {
        private MediaFile mediaFile;

        private Stream f;

        private string pathf = "";

        private string userEmail = "";

        UpdateProfileFragmentViewModel updateProfileFragmentViewModel
            = new UpdateProfileFragmentViewModel();

        public UpdateProfilePage()
        {
            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            User user = updateProfileFragmentViewModel.GetUserByEmail(userEmail);

            InitializeComponent();

            lastnameEntry.Placeholder = user.LastName;
            firstnameEntry.Placeholder = user.FirstName;
            emailEntry.Placeholder = user.Email;

            if (!String.IsNullOrEmpty(user.ProfilePictureURL))
            {
                profilePictureImage.Source = ImageSource.FromUri(new Uri(user.ProfilePictureURL));
            }
        }

        private async void updateMyProfileButton_ClickedAsync(object sender, EventArgs e)
        {
            string success = updateProfileFragmentViewModel.UpdateProfile(firstnameEntry.Text, lastnameEntry.Text, userEmail);

            if (!String.IsNullOrEmpty(success))
            {
                //HIBA
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private async void changeEmailButton_ClickedAsync(object sender, EventArgs e)
        {
            string success = updateProfileFragmentViewModel.UpdateEmail(userEmail, emailEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                //HIBA
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private async void changepwButton_ClickedAsync(object sender, EventArgs e)
        {
            string success = updateProfileFragmentViewModel.UpdatePassword(pwEntry.Text, newpwEntry.Text, userEmail);

            if (!String.IsNullOrEmpty(success))
            {
                //HIBA
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private async Task changeProfilePictureButton_ClickedAsync(object sender, EventArgs e)
        {
            string success = await updateProfileFragmentViewModel.UpdateProfilePicture(pathf, f, userEmail);

            if (!String.IsNullOrEmpty(success))
            {
                //hibaüzenet
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private async Task galleryButton_ClickedAsync(object sender, EventArgs e)
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

        private async void changeFaceookButton_ClickedAsync(object sender, EventArgs e)
        {
            DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();
            
            await Navigation.PushAsync(new FacebookLogin.Views.FacebookProfileCsPage());
        }
    }
}