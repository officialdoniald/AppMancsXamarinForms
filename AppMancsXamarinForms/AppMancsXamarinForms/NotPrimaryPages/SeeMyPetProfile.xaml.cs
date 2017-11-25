using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeMyPetProfile : ContentPage
    {
        private int petid = -1;

        private string userEmail = "";

        private bool HaveIAlreadyFollow = false;

        private Pet thisPet = new Pet();

        private List<Petpictures> petPictureListfromDB = new List<Petpictures>();

        private Petpictures petpictures = new Petpictures();

        private PetProfileFragmentViewModel petProfileFragmentViewModel =
            new PetProfileFragmentViewModel();

        public SeeMyPetProfile(int petid)
        {
            this.petid = petid;

            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            petPictureListfromDB = petProfileFragmentViewModel.GetPetPictureURL(petid);

            thisPet = petProfileFragmentViewModel.GetPetFromDBByID(petid);

            InitializeComponent();

            petnameLabel.Text = thisPet.Name;

            profilePictureImage.Source = ImageSource.FromUri(new Uri(thisPet.ProfilePictureURL));

            HaveIAlreadyFollow = petProfileFragmentViewModel.HaveIAlreadyFollow(userEmail, petid);

            int left = 0;
            int top = 0;

            int i = 1;

            foreach (var item in petPictureListfromDB)
            {
                Image image = new Image();

                image.Source = ImageSource.FromUri(new Uri(item.PictureURL));

                image.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    TappedCallback = delegate
                    {
                        OnPictureClicked(item);
                    }
                });

                image.WidthRequest = 50;
                image.HeightRequest = 50;

                pictureListGrid.Children.Add(image, top, left);

                if (i == 3)
                {
                    left++;
                    i = 1;
                    top = 0;
                }
                else
                {
                    i++;
                    top++;
                }
            }
        }

        public void OnPictureClicked(Petpictures petpictures)
        {
            Navigation.PushAsync(new SeeMyPicturePage(petpictures));
        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdatePetProfilePage(thisPet.id));
        }
    }
}