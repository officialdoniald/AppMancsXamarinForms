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
    public partial class UpdatePetProfilePage : ContentPage
    {
        private MediaFile mediaFile;

        private Stream f;

        private string pathf = "";

        private string userEmail = "";

        private int petid = -1;

        private Pet thisPet = new Pet();

        UpdatePetFragmentViewModel updatePetFragmentViewModel =
            new UpdatePetFragmentViewModel();

        public UpdatePetProfilePage(int petid)
        {
            this.petid = petid;

            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            InitializeComponent();

            thisPet = updatePetFragmentViewModel.GetThisPet(petid);

            nameEntry.Placeholder = thisPet.Name;
            ageEntry.Placeholder = thisPet.Age.ToString();
            typeEntry.Placeholder = thisPet.PetType;

            if (!String.IsNullOrEmpty(thisPet.ProfilePictureURL))
            {
                profilePictureImage.Source = ImageSource.FromUri(new Uri(thisPet.ProfilePictureURL));
            }

            if (thisPet.HaveAnOwner == 0) shelterpetSwitch.IsToggled = true;
            else shelterpetSwitch.IsToggled = false;
        }

        private async void deletePetButton_ClickedAsync(object sender, EventArgs e)
        {
            string success = updatePetFragmentViewModel.DeletePet(petid);

            if (!String.IsNullOrEmpty(success))
            {
                //hibaüzenet
            }
            else
            {
                await Navigation.PushAsync(new MyAccountPage());
            }
        }

        private async void changePetButton_ClickedAsync(object sender, EventArgs e)
        {
            bool isChecked = shelterpetSwitch.IsToggled;

            int isCheckedToInt = 0;

            if (!isChecked) isCheckedToInt = 1;

            int age;

            if (String.IsNullOrEmpty(ageEntry.Text))
            {
                age = -1;
            }
            else
            {
                age = Convert.ToInt32(ageEntry.Text);
            }

            Pet pet = new Pet();

            pet = thisPet;
            pet.HaveAnOwner = isCheckedToInt;

            if (!String.IsNullOrEmpty(nameEntry.Text))
            {
                pet.Name = nameEntry.Text;
            }
            if (age != -1)
            {
                pet.Age = age;
            }
            if (!String.IsNullOrEmpty(typeEntry.Text))
            {
                pet.PetType = typeEntry.Text;
            }

            string success = updatePetFragmentViewModel.UpdatePetProfile(pet);

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

        private async void changeProfilePictureButton_Clicked(object sender, EventArgs e)
        {
            string success = await updatePetFragmentViewModel.UpdatePetProfilePictureAsync(thisPet, f, pathf);

            if (!String.IsNullOrEmpty(success))
            {
                //hibaüzenet
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
    }
}