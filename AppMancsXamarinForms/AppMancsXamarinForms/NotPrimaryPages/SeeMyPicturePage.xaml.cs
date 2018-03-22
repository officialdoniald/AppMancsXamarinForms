using AppMancsXamarinForms.BLL.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;
using System.Linq;
using System.Threading.Tasks;

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

            Initialize();
        }

        private async Task Initialize()
        {
            await Task.Run(() =>
            {
                thisPet = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(u => u.petid == petpictures.PetID).FirstOrDefault());
                Device.BeginInvokeOnMainThread(() =>
                {
                    nameLabel.Text = thisPet.Name;

                    profilePictureImage.Source = ImageSource.FromUri(new Uri(thisPet.ProfilePictureURL));

                    pictureImage.Source = ImageSource.FromUri(new Uri(petpictures.PictureURL));
                });

                var asd = GlobalVariables.seePictureFragmentViewModel.GetHashtags(petpictures.id).Split(' ');

                foreach (var item2 in asd)
                {
                    Device.BeginInvokeOnMainThread(() =>
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
                    });
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    likes = GlobalVariables.seePictureFragmentViewModel.GetLikes(petpictures.id);

                    howmanylike = likes.Count;

                    howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();

                    haveiliked = GlobalVariables.seePictureFragmentViewModel.HaveILiked(petpictures.id);
                });
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SeeMyPetProfile(petpictures.PetID));
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WhosLiked(petpictures.id));
        }

        private async Task deleteButton_ClickedAsync(object sender, EventArgs e)
        {
            if (!GlobalVariables.seePictureFragmentViewModel.DeletePicture(this.petpictures))
            {
                await DisplayAlert("Failed",English.SomethingWentWrong(),"OK");
            }
            else
            {
                GlobalVariables.IsPictureDeleted = true;

                await Navigation.PopToRootAsync();

                await DisplayAlert("Successful", English.SuccessfulDeletedThePicture(), "OK");
            }
        }
    }
}