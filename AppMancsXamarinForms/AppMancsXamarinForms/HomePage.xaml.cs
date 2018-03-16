using AppMancsXamarinForms.NotPrimaryPages;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private List<Wall> wallList = new List<Wall>();

        private List<WallListViewAdapter> wallListViewAdapter =
            new List<WallListViewAdapter>();

        public HomePage()
        {
            InitializeComponent();

            Icon = "home.png";

            Title = String.Empty;

            NavigationPage.SetHasNavigationBar(this, false);

            wallList = GlobalVariables.homeFragmentViewModel.GetWallList();

            foreach (var item in wallList)
            {
                if (!GlobalVariables.whosLikedViewModel.IsMyPet(item.petpictures.PetID))
                {
                    wallListViewAdapter.Add(new WallListViewAdapter()
                    {
                        wallItem = item,
                        howManyLikes = item.howmanylikes.ToString(),
                        petName = item.name,
                        profilepictureURL = ImageSource.FromUri(new Uri(item.ProfilePictureURL)),
                        pictureURL = ImageSource.FromUri(new Uri(item.petpictures.PictureURL)),
                        followButtonText = followButtonText(item.haveILiked),
                        hashtags = GlobalVariables.homeFragmentViewModel.GetHashtags(item.petpictures.id)
                    });
                }
            }

            foreach (var item in wallListViewAdapter)
            {
                //KÉP
                var pictureImage = new Image()
                {
                    Source = item.pictureURL
                };

                //HEADER
                var header = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(5, 20, 0, 5)
                };

                var profileImage = new Image()
                {
                    Source = item.profilepictureURL,
                    HeightRequest = 28,
                    WidthRequest = 28
                };

                var profileName = new Label()
                {
                    Text = item.petName
                };

                var goToPetProfileTapped = new TapGestureRecognizer();
                goToPetProfileTapped.Tapped += (s, e) =>
                {
                    if (!GlobalVariables.whosLikedViewModel.IsMyPet(item.wallItem.petpictures.PetID))
                    {
                        Navigation.PushAsync(new SeeAPetProfile(item.wallItem.petpictures.PetID));
                    }
                    else
                    {
                        Navigation.PushAsync(new SeeMyPetProfile(item.wallItem.petpictures.PetID));
                    }
                };

                profileImage.GestureRecognizers.Add(goToPetProfileTapped);
                profileName.GestureRecognizers.Add(goToPetProfileTapped);


                var likeImage = new Image()
                {
                    Source = item.followButtonText,
                    HeightRequest = 28,
                    WidthRequest = 28,
                    HorizontalOptions = LayoutOptions.End
                };

                var howManyLikeLabel = new Label()
                {
                    Text = item.howManyLikes + " likes",
                    //HorizontalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.Center
                };

                var whoslikedTapped = new TapGestureRecognizer();
                whoslikedTapped.Tapped += (s, e) =>
                {
                    Navigation.PushAsync(new WhosLiked(item.wallItem.petpictures.id));
                };

                var likeOrNotTapped = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1
                };
                likeOrNotTapped.Tapped += (s, e) =>
                {
                    int howmanylikes = item.wallItem.howmanylikes;

                    if (item.wallItem.haveILiked)
                    {
                        GlobalVariables.homeFragmentViewModel.Unlike(item.wallItem.petpictures.id);

                        item.wallItem.haveILiked = !item.wallItem.haveILiked;

                        likeImage.Source = "like.png";

                        item.wallItem.howmanylikes = howmanylikes - 1;

                        howManyLikeLabel.Text = item.wallItem.howmanylikes.ToString() + " likes";
                    }
                    else
                    {
                        GlobalVariables.homeFragmentViewModel.LikePicture(item.wallItem.petpictures.id);

                        item.wallItem.haveILiked = !item.wallItem.haveILiked;

                        likeImage.Source = "unlike.png";

                        item.wallItem.howmanylikes = howmanylikes + 1;

                        howManyLikeLabel.Text = item.wallItem.howmanylikes.ToString() + " likes";
                    }
                };

                var likeOrNotTappedDoubleTap = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 2
                };
                likeOrNotTappedDoubleTap.Tapped += (s, e) =>
                {
                    int howmanylikes = item.wallItem.howmanylikes;

                    if (item.wallItem.haveILiked)
                    {
                        GlobalVariables.homeFragmentViewModel.Unlike(item.wallItem.petpictures.id);

                        item.wallItem.haveILiked = !item.wallItem.haveILiked;

                        likeImage.Source = "like.png";

                        item.wallItem.howmanylikes = howmanylikes - 1;

                        howManyLikeLabel.Text = item.wallItem.howmanylikes.ToString() + " likes";
                    }
                    else
                    {
                        GlobalVariables.homeFragmentViewModel.LikePicture(item.wallItem.petpictures.id);

                        item.wallItem.haveILiked = !item.wallItem.haveILiked;

                        likeImage.Source = "unlike.png";

                        item.wallItem.howmanylikes = howmanylikes + 1;

                        howManyLikeLabel.Text = item.wallItem.howmanylikes.ToString() + " likes";
                    }
                };

                howManyLikeLabel.GestureRecognizers.Add(whoslikedTapped);
                likeImage.GestureRecognizers.Add(likeOrNotTapped);

                pictureImage.GestureRecognizers.Add(likeOrNotTappedDoubleTap);

                header.Children.Add(profileImage);
                header.Children.Add(profileName);
                header.Children.Add(likeImage);
                header.Children.Add(howManyLikeLabel);

                //FOOTER
                var footer = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    Margin = new Thickness(5, 0, 0, 0)
                };
                var asd = item.hashtags.Split(' ');

                foreach (var item2 in asd)
                {
                    Label hashtagLabel = new Label()
                    {
                        Text = item2
                    };

                    var onHashtagClickedTap = new TapGestureRecognizer()
                    {
                        NumberOfTapsRequired = 1
                    };
                    onHashtagClickedTap.Tapped += async (s, e) =>
                    {
                        List<SearchModel> searchModelList = GlobalVariables.searchFragmentViewModel.GetSearchModel();

                        var asd24 = (from q in searchModelList where q.hashtag == item2 select q); 

                        await Navigation.PushAsync(new SearchResultPage(asd24.First().petpicturesList, item2.Split('#')[1]));
                    };

                    hashtagLabel.GestureRecognizers.Add(onHashtagClickedTap);

                    footer.Children.Add(hashtagLabel);
                }

                mainStackLayout.Children.Add(header);
                mainStackLayout.Children.Add(pictureImage);
                mainStackLayout.Children.Add(footer);
            }
        }

        private string followButtonText(bool haveILiked)
        {
            if (haveILiked)
            {
                return "unlike.png";
            }
            else
            {
                return "like.png";
            }
        }
    }
}