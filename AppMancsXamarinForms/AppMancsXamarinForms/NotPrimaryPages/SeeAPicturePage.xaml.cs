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
	public partial class SeeAPicturePage : ContentPage
    {
        private string userEmail = "";

        private int howmanylike = 0;

        private bool haveiliked = false;

        SeePictureFragmentViewModel seePictureFragmentViewModel =
            new SeePictureFragmentViewModel();

        private List<Likes> likes = new List<Likes>();

        Pet thisPet = new Pet();

        Petpictures petpictures = new Petpictures();

        public SeeAPicturePage()
        {
            InitializeComponent();
        }

        public SeeAPicturePage(Petpictures petpictures)
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

            if (haveiliked) likeOrNotButton.Text = seePictureFragmentViewModel.unlikeText;
            else likeOrNotButton.Text = seePictureFragmentViewModel.likeText;
        }

        private void likeOrNotButton_Clicked(object sender, EventArgs e)
        {
            if (haveiliked)
            {
                var success = seePictureFragmentViewModel.UnLikeClick(userEmail, petpictures.id);

                if (!String.IsNullOrEmpty(success))
                {
                    //HIBA
                }
                else
                {
                    howmanylike = howmanylike - 1;

                    howmanyLikesLabel.Text = howmanylike.ToString();

                    haveiliked = !haveiliked;

                    likeOrNotButton.Text = seePictureFragmentViewModel.likeText;
                }
            }
            else
            {
                string success = seePictureFragmentViewModel.LikeClick(userEmail, petpictures.id);

                if (!String.IsNullOrEmpty(success))
                {
                    //HIBA
                }
                else
                {
                    howmanylike = howmanylike + 1;

                    howmanyLikesLabel.Text = howmanylike.ToString();

                    haveiliked = !haveiliked;

                    likeOrNotButton.Text = seePictureFragmentViewModel.unlikeText;
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeeAPetProfile(petpictures.PetID));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WhosLiked(petpictures.id));
        }
    }
}