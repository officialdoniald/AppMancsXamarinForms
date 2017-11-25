using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPetPage : ContentPage
    {
        private MediaFile mediaFile;

        private Stream f;

        private string pathf = "";

        private string userEmail = "";

        private AddpetFragmentViewModel addpetFragmentViewModel =
                new AddpetFragmentViewModel();

        public AddPetPage()
        {
            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            InitializeComponent();
        }
        
        private async Task addPetButton_ClickedAsync(object sender, EventArgs e)
        {
            bool isChecked = shelterpetSwitch.IsToggled;

            int isCheckedToInt = 0;

            if (isChecked) isCheckedToInt = 1;

            int age;

            try
            {
                age = Convert.ToInt32(ageEntry.Text);
            }
            catch (Exception)
            {
                age = -1;
            }

            Pet pet = new Pet()
            {
                Name = nameEntry.Text,
                Age = age,
                PetType = typeEntry.Text,
                HaveAnOwner = isCheckedToInt
            };

            string success = await addpetFragmentViewModel.AddPetAsync(userEmail, pathf, f, pet);

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
    }
}