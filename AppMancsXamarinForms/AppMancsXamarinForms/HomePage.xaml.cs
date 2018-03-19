﻿using AppMancsXamarinForms.NotPrimaryPages;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;
using System.Threading.Tasks;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private List<Wall> wallList = new List<Wall>();

        public HomePage()
        {
            InitializeComponent();

            wallListView.IsRefreshing = true;

            FirstTime();
        }

        private async Task FirstTime(){
            await Task.Run(() => {
                Initialize();
            });
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

        private void Initialize()
        {
            GlobalVariables.wallListViewAdapter = new List<WallListViewAdapter>();

            wallList = GlobalVariables.homeFragmentViewModel.GetWallList();

            foreach (var item in wallList)
            {
                if (!GlobalVariables.whosLikedViewModel.IsMyPet(item.petpictures.PetID))
                {
                    GlobalVariables.wallListViewAdapter.Add(new WallListViewAdapter()
                    {
                        wallItem = item,
                        howManyLikes = item.howmanylikes.ToString() + " Like",
                        petName = item.name,
                        profilepictureURL = ImageSource.FromUri(new Uri(item.ProfilePictureURL)),
                        pictureURL = ImageSource.FromUri(new Uri(item.petpictures.PictureURL)),
                        followButtonText = followButtonText(item.haveILiked),
                        hashtags = GlobalVariables.homeFragmentViewModel.GetHashtags(item.petpictures.id)
                    });
                }
            }

            Device.BeginInvokeOnMainThread(()=>{
                wallListView.ItemsSource = GlobalVariables.wallListViewAdapter;
                wallListView.IsRefreshing = false;
            });
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Run(()=>{
                Initialize();
            });
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label label = (Label)sender;

            var wallListViewAdapterClicked = (WallListViewAdapter)label.BindingContext;

            if (!GlobalVariables.whosLikedViewModel.IsMyPet(wallListViewAdapterClicked.wallItem.petpictures.PetID))
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

            Image button = (Image)sender;

            var asd = (Grid)button.Parent;

            var collection = (Grid.IGridList<View>)asd.Children;

            Label likeNumberLabel = (Label)collection[4];

            var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

            int howmanylikes = wallListViewAdapterClicked.wallItem.howmanylikes;

            if (wallListViewAdapterClicked.wallItem.haveILiked)
            {
                GlobalVariables.homeFragmentViewModel.Unlike(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = "like.png";

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes - 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + " Like";
            }
            else
            {
                GlobalVariables.homeFragmentViewModel.LikePicture(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = "unlike.png";

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes + 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + " Like";
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

            Image pic = (Image)sender;

            var asd = (Grid)pic.Parent;

            var collection = (Grid.IGridList<View>)asd.Children;

            Image button = (Image)collection[3];

            Label likeNumberLabel = (Label)collection[4];

            var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

            int howmanylikes = wallListViewAdapterClicked.wallItem.howmanylikes;

            if (wallListViewAdapterClicked.wallItem.haveILiked)
            {
                GlobalVariables.homeFragmentViewModel.Unlike(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = "like.png";

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes - 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + " Like";
            }
            else
            {
                GlobalVariables.homeFragmentViewModel.LikePicture(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = "unlike.png";

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes + 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + " Like";
            }
        }

    }
}