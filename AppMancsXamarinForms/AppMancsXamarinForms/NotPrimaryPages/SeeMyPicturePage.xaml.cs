using AppMancsXamarinForms.BLL.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;
using System.Linq;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeMyPicturePage : ContentPage
    {
        private int howmanylike = 0;

        private bool haveiliked = false;

        private List<Likes> likes = new List<Likes>();

        Pet thisPet = new Pet();

        Petpictures petpictures = new Petpictures();

        public SeeMyPicturePage(Petpictures petpictures)
        {
            this.petpictures = petpictures;

            InitializeComponent();

            thisPet = GlobalVariables.seePictureFragmentViewModel.GetPetById(petpictures.PetID);

            nameLabel.Text = thisPet.Name;

            profilePictureImage.Source = ImageSource.FromUri(new Uri(thisPet.ProfilePictureURL));

            pictureImage.Source = ImageSource.FromUri(new Uri(petpictures.PictureURL));

            //pictureImage.HeightRequest = Application.Current.MainPage.Width * 1.5;

            var asd = GlobalVariables.seePictureFragmentViewModel.GetHashtags(petpictures.id).Split(' ');

            foreach (var item2 in asd)
            {
                Label hashtagLabel = new Label()
                {
                    Text = item2,
                    TextColor = Color.FromHex("#FFCBB6"),
                    FontSize = 15
                };

                var onHashtagClickedTap = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1
                };
                onHashtagClickedTap.Tapped += (s, e) =>
                {
                    SearchFragmentViewModel searchFragmentViewModel = new SearchFragmentViewModel();

                    List<SearchModel> searchModelList = searchFragmentViewModel.GetSearchModel();

                    var asd24 = (from q in searchModelList where q.hashtag == item2 select q);

                    Navigation.PushAsync(new SearchResultPage(asd24.First().petpicturesList, item2.Split('#')[1]));
                };

                hashtagLabel.GestureRecognizers.Add(onHashtagClickedTap);

                mainStackLayout.Children.Add(hashtagLabel);
            }

            likes = GlobalVariables.seePictureFragmentViewModel.GetLikes(petpictures.id);

            howmanylike = likes.Count;

            howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();

            haveiliked = GlobalVariables.seePictureFragmentViewModel.HaveILiked(petpictures.id);
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