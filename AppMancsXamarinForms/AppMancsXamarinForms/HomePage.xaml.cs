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
                return GlobalVariables.unlikepng;
            }
            else
            {
                return GlobalVariables.likepng;
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
                        howManyLikes = item.howmanylikes.ToString() + English.GetLike(),
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

            //if (wallList.Count == 0 && Device.OS == TargetPlatform.Android)
            //{
            //    wallListView.IsRefreshing = false;
            //}
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Run(()=>{
                Initialize();
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var wallListViewAdapterClicked = new WallListViewAdapter();

            try
            {
                wallListViewAdapterClicked = (WallListViewAdapter)(((Label)sender).BindingContext);
            }
            catch (Exception)
            {
                wallListViewAdapterClicked = (WallListViewAdapter)(((Image)sender).BindingContext);
            }

            var isThisMyPet = GlobalVariables.Mypetlist.Where(u => u.petid == wallListViewAdapterClicked.wallItem.petpictures.PetID);

            if (isThisMyPet != null)
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

            Label likeNumberLabel = (Label)collection[3];

            var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

            int howmanylikes = wallListViewAdapterClicked.wallItem.howmanylikes;

            if (wallListViewAdapterClicked.wallItem.haveILiked)
            {
                GlobalVariables.homeFragmentViewModel.Unlike(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = GlobalVariables.likepng;

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes - 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + English.GetLike();
            }
            else
            {
                GlobalVariables.homeFragmentViewModel.LikePicture(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = GlobalVariables.unlikepng;

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes + 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + English.GetLike();
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Image pic = (Image)sender;

            var asd = (Grid)pic.Parent;

            var collection = (Grid.IGridList<View>)asd.Children;

            Image button = (Image)collection[4];

            Label likeNumberLabel = (Label)collection[3];

            var wallListViewAdapterClicked = (WallListViewAdapter)button.BindingContext;

            int howmanylikes = wallListViewAdapterClicked.wallItem.howmanylikes;

            if (wallListViewAdapterClicked.wallItem.haveILiked)
            {
                GlobalVariables.homeFragmentViewModel.Unlike(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = GlobalVariables.likepng;

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes - 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + English.GetLike();
            }
            else
            {
                GlobalVariables.homeFragmentViewModel.LikePicture(wallListViewAdapterClicked.wallItem.petpictures.id);

                wallListViewAdapterClicked.wallItem.haveILiked = !wallListViewAdapterClicked.wallItem.haveILiked;

                button.Source = GlobalVariables.unlikepng;

                wallListViewAdapterClicked.wallItem.howmanylikes = howmanylikes + 1;

                likeNumberLabel.Text = wallListViewAdapterClicked.wallItem.howmanylikes.ToString() + English.GetLike();
            }
        }

    }
}