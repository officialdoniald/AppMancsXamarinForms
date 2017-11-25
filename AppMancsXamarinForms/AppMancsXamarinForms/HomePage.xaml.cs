using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using AppMancsXamarinForms.NotPrimaryPages;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private string userEmail = "";

        private List<Wall> wallList = new List<Wall>();

        private List<WallListViewAdapter> wallListViewAdapter =
            new List<WallListViewAdapter>();

        private HomeFragmentViewModel homeFragmentViewModel = new HomeFragmentViewModel();

        private WhosLikedViewModel whosLikedViewModel = new WhosLikedViewModel();

        public HomePage()
        {
            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            InitializeComponent();

            wallList = homeFragmentViewModel.GetWallList(userEmail);

            foreach (var item in wallList)
            {
                if (!whosLikedViewModel.IsMyPet(item.petpictures.PetID, userEmail))
                {
                    wallListViewAdapter.Add(new WallListViewAdapter()
                    {
                        wallItem = item,
                        howManyLikes = item.howmanylikes.ToString(),
                        petName = item.name,
                        profilepictureURL = ImageSource.FromUri(new Uri(item.ProfilePictureURL)),
                        pictureURL = ImageSource.FromUri(new Uri(item.petpictures.PictureURL)),
                        followButtonText = followButtonText(item.haveILiked),
                        hashtags = homeFragmentViewModel.GetHashtags(item.petpictures.id)
                    });
                }
            }

            wallListView.ItemsSource = wallListViewAdapter;
        }

        private string followButtonText(bool haveILiked)
        {
            if (haveILiked)
            {
                return "Nem tetszik";
            }
            else
            {
                return "Tetszik";
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label label = (Label)sender;

            var wallListViewAdapterClicked = (WallListViewAdapter)label.BindingContext;

            if (!whosLikedViewModel.IsMyPet(wallListViewAdapterClicked.wallItem.petpictures.PetID, userEmail))
            {
                Navigation.PushAsync(new SeeAPetProfile(wallListViewAdapterClicked.wallItem.petpictures.PetID));
            }
            else
            {
                Navigation.PushAsync(new SeeMyPetProfile(wallListViewAdapterClicked.wallItem.petpictures.PetID));
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Label label = (Label)sender;

            var wallListViewAdapterClicked = (WallListViewAdapter)label.BindingContext;

            Navigation.PushAsync(new WhosLiked(wallListViewAdapterClicked.wallItem.petpictures.id));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

            Button button = (Button)sender;

            var asd = (Grid)button.Parent;

            var collection = (Grid.IGridList<View>)asd.Children;

            Label likeNumberLabel = (Label)collection[3];

            var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

            int howmanylikes = wallListViewAdapterClicked.wallItem.howmanylikes;

            if (wallListViewAdapterClicked.wallItem.haveILiked)
            {
                homeFragmentViewModel.Unlike(userEmail, wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Text = "Tetszik";

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes - 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString();
            }
            else
            {
                homeFragmentViewModel.LikePicture(userEmail, wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Text = "Nem tetszik";

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes + 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString();
            }
        }
    }
}