using AppMancsXamarinForms.NotPrimaryPages;
using Model;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadPhotoPage : ContentPage
    {
        private int selectedPetId = -1;

        public UploadPhotoPage()
        {
            Initialize();
        }

		protected override void OnAppearing()
		{
            if (GlobalVariables.AddedPet || GlobalVariables.AddedPhoto)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            InitializeComponent();

            if (GlobalVariables.Mypetlist.Count != 0)
            {
                selectedPetId = GlobalVariables.Mypetlist[0].petid;

                GlobalVariables.AddedPet = false;
                GlobalVariables.AddedPhoto = false;
            }

            petPicker.ItemsSource = GlobalVariables.MyPetsString;
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

            pictureImage.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async Task addPhotoButton_ClickedAsync(object sender, EventArgs e)
        {
            uploadActivity.IsRunning = true;
            addPhotoButton.IsEnabled = false;
            galleryButton.IsEnabled = false;

            string success = await GlobalVariables.uploadPhotoFragmentViewModel.UploadPictureAsync(GlobalVariables.pathf, GlobalVariables.f, selectedPetId, hashtagsEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                //TODO
            }
            else
            {
                GlobalVariables.AddedPhoto = true;

                await Navigation.PushAsync(new SeeMyPetProfile(selectedPetId));
            }

            galleryButton.IsEnabled = true;
            addPhotoButton.IsEnabled = true;
            uploadActivity.IsRunning = false;
        }

        private void petPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPetId = GlobalVariables.Mypetlist[petPicker.SelectedIndex].petid;
        }
    }
}