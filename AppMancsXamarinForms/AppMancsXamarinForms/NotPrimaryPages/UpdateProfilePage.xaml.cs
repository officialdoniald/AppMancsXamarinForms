using Plugin.Media;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProfilePage : ContentPage
    {
        public UpdateProfilePage()
        {
            InitializeComponent();

            lastnameEntry.Placeholder = GlobalVariables.ActualUser.LastName;
            firstnameEntry.Placeholder = GlobalVariables.ActualUser.FirstName;
            emailEntry.Placeholder = GlobalVariables.ActualUser.Email;

            if (!String.IsNullOrEmpty(GlobalVariables.ActualUser.ProfilePictureURL))
            {
                profilePictureImage.Source = ImageSource.FromUri(new Uri(GlobalVariables.ActualUser.ProfilePictureURL));
            }
        }

        private async void updateMyProfileButton_ClickedAsync(object sender, EventArgs e)
        {
            updateMyProfileButton.IsEnabled = false;
            
            string success = GlobalVariables.updateProfileFragmentViewModel.UpdateProfile(firstnameEntry.Text, lastnameEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                await GlobalVariables.ContentPageFunctions.CreateNegativDisplayAlert(success);
            }
            else
            {
                GlobalVariables.IsUpdatedMyProfile = true;
                
                await Navigation.PopAsync();
            }

            updateMyProfileButton.IsEnabled = true;
        }

        private async void changeEmailButton_ClickedAsync(object sender, EventArgs e)
        {
            changeEmailButton.IsEnabled = false;
            
            string success = GlobalVariables.updateProfileFragmentViewModel.UpdateEmail(emailEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                await GlobalVariables.ContentPageFunctions.CreateNegativDisplayAlert(success);
            }
            else
            {
                await Navigation.PopAsync();
            }

            changeEmailButton.IsEnabled = true;
        }

        private async void changepwButton_ClickedAsync(object sender, EventArgs e)
        {
            changepwButton.IsEnabled = false;

            string success = GlobalVariables.updateProfileFragmentViewModel.UpdatePassword(pwEntry.Text, newpwEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                await GlobalVariables.ContentPageFunctions.CreateNegativDisplayAlert(success);
            }
            else
            {
                await Navigation.PopAsync();
            }

            changepwButton.IsEnabled = true;
        }

        private async Task changeProfilePictureButton_ClickedAsync(object sender, EventArgs e)
        {
            changeProfilePictureButton.IsEnabled = false;

            string success = await GlobalVariables.updateProfileFragmentViewModel.UpdateProfilePicture(GlobalVariables.pathf, GlobalVariables.f);

            if (!String.IsNullOrEmpty(success))
            {
                await GlobalVariables.ContentPageFunctions.CreateNegativDisplayAlert(success);
            }
            else
            {
                await Navigation.PopAsync();
            }

            changeProfilePictureButton.IsEnabled = true;
        }

        private async Task galleryButton_ClickedAsync(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Warning", "No picking available, please try again later!", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            GlobalVariables.mediaFile = file;

            if (file == null) return;
            
            GlobalVariables.f = file.GetStream();
            GlobalVariables.pathf = file.Path;

            profilePictureImage.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async void changeFaceookButton_ClickedAsync(object sender, EventArgs e)
        {
            changeFaceookButton.IsEnabled = false;
            
            DependencyService.Get<IClearCookies>().ClearAllWebAppCookies();
            
            await Navigation.PushAsync(new FacebookLogin.Views.FacebookProfileCsPage());
        
            changeFaceookButton.IsEnabled = true;
        }
    }
}