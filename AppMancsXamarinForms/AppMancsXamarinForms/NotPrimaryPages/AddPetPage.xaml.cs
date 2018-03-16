using Model;
using Plugin.Media;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPetPage : ContentPage
    {
        public AddPetPage()
        {
            InitializeComponent();
        }
        
        private async Task addPetButton_ClickedAsync(object sender, EventArgs e)
        {
            bool isChecked = shelterpetSwitch.IsToggled;

            int isCheckedToInt = 1;

            if (isChecked) isCheckedToInt = 0;

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

            string success = await GlobalVariables.addpetFragmentViewModel.AddPetAsync(GlobalVariables.pathf, GlobalVariables.f, pet);

            if (!String.IsNullOrEmpty(success))
            {
                //TODO
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

            GlobalVariables.mediaFile = file;

            if (file == null) return;

            GlobalVariables.f = file.GetStream();
            GlobalVariables.pathf = file.Path;

            profilePictureImage.Source = ImageSource.FromStream(() => file.GetStream());
        }
    }
}