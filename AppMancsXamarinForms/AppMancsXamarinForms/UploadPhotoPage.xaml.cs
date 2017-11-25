using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using AppMancsXamarinForms.NotPrimaryPages;
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
    public partial class UploadPhotoPage : ContentPage
    {
        private MediaFile mediaFile;

        private Stream f;

        private string pathf = "";

        private int selectedPetId = -1;

        private string userEmail = "";

        private UploadPhotoFragmentViewModel uploadPhotoFragmentViewModel =
            new UploadPhotoFragmentViewModel();

        private User user = new User();

        private string[] myPets = new string[] { };

        private List<Pet> myPetList = new List<Pet>();

        public UploadPhotoPage()
        {
            InitializeComponent();

            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            user = uploadPhotoFragmentViewModel.GetUserByEmail(userEmail);

            myPetList = uploadPhotoFragmentViewModel.GetMyPets(user.id);

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

            mediaFile = file;

            if (file == null) return;

            f = file.GetStream();
            pathf = file.Path;

            pictureImage.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async Task addPhotoButton_ClickedAsync(object sender, EventArgs e)
        {
            string success = await uploadPhotoFragmentViewModel.UploadPictureAsync(pathf, f, selectedPetId, hashtagsEntry.Text);

            if (!String.IsNullOrEmpty(success))
            {
                //HIBA
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