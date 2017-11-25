using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeMyPicturePage : ContentPage
    {
        private string userEmail = "";

        private int howmanylike = 0;

        private bool haveiliked = false;

        SeePictureFragmentViewModel seePictureFragmentViewModel =
            new SeePictureFragmentViewModel();

        private List<Likes> likes = new List<Likes>();

        Pet thisPet = new Pet();

        Petpictures petpictures = new Petpictures();

        public SeeMyPicturePage(Petpictures petpictures)
        {
            this.petpictures = petpictures;

            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            InitializeComponent();

            thisPet = seePictureFragmentViewModel.GetPetById(petpictures.PetID);

            nameLabel.Text = thisPet.Name;

            profilePictureImage.Source = ImageSource.FromUri(new Uri(thisPet.ProfilePictureURL));

            pictureImage.Source = ImageSource.FromUri(new Uri(petpictures.PictureURL));

            hashtagsLabel.Text = seePictureFragmentViewModel.GetHashtags(petpictures.id);

            likes = seePictureFragmentViewModel.GetLikes(petpictures.id);

            howmanylike = likes.Count;

            howmanyLikesLabel.Text = howmanylike.ToString();

            haveiliked = seePictureFragmentViewModel.HaveILiked(userEmail, petpictures.id);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeeMyPetProfile(petpictures.PetID));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WhosLiked(petpictures.id));
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            //TODO
        }
    }
}