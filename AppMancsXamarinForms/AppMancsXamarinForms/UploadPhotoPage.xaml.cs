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

        private string[] myPets = new string[] { };

        private List<Pet> myPetList = new List<Pet>();

        public UploadPhotoPage()
        {
            InitializeComponent();

            myPetList = GlobalVariables.uploadPhotoFragmentViewModel.GetMyPets(GlobalVariables.ActualUser.id);

            myPets = new string[myPetList.Count];

            int i = 0;

            if (myPetList.Count != 0)
            {
                selectedPetId = myPetList[0].id;
            }

            foreach (var item in myPetList)
            {
                myPets[i] = item.Name;

                i++;
            }

            petPicker.ItemsSource = myPets;
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
            string success = await GlobalVariables.uploadPhotoFragmentViewModel.UploadPictureAsync(GlobalVariables.pathf, GlobalVariables.f, selectedPetId, hashtagsEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                //TODO
            }
            else
            {
                await Navigation.PushAsync(new SeeMyPetProfile(selectedPetId));
            }
        }

        private void petPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPetId = myPetList[petPicker.SelectedIndex].id;
        }
    }
}