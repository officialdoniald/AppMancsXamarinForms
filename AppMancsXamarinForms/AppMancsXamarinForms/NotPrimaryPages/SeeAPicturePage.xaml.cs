﻿using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.NotPrimaryPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeeAPicturePage : ContentPage
    {
        private int howmanylike = 0;

        private bool haveiliked = false;

        private List<Likes> likes = new List<Likes>();

        Pet thisPet = new Pet();

        Petpictures petpictures = new Petpictures();

        public SeeAPicturePage()
        {
            InitializeComponent();
        }

        public SeeAPicturePage(Petpictures petpictures)
        {
            InitializeComponent();

            this.petpictures = petpictures;

            Initialize();
        }

        private async Task Initialize()
        {
            await Task.Run(() =>
            {

                thisPet = GlobalVariables.seePictureFragmentViewModel.GetPetById(petpictures.PetID);

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

                likes = GlobalVariables.seePictureFragmentViewModel.GetLikes(petpictures.id);

                howmanylike = likes.Count;
                Device.BeginInvokeOnMainThread(() =>
                {
                    howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();

                    haveiliked = GlobalVariables.seePictureFragmentViewModel.HaveILiked(petpictures.id);

                    if (haveiliked) likeornotImage.Source = "unlike.png";
                    else likeornotImage.Source = "like.png";
                });
            });
        }

        private async Task likeOrNotButton_ClickedAsync(object sender, EventArgs e)
        {
            if (haveiliked)
            {
                var success = GlobalVariables.seePictureFragmentViewModel.UnLikeClick(petpictures.id);

                if (!String.IsNullOrEmpty(success))
                {
                    await DisplayAlert(English.Failed(),success,English.OK());
                }
                else
                {
                    howmanylike = howmanylike - 1;

                    howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();

                    haveiliked = !haveiliked;

                    likeornotImage.Source = "like.png";
                }
            }
            else
            {
                string success = GlobalVariables.seePictureFragmentViewModel.LikeClick(petpictures.id);

                if (!String.IsNullOrEmpty(success))
                {
                    await DisplayAlert(English.Failed(),success,English.OK());
                }
                else
                {
                    howmanylike = howmanylike + 1;

                    howmanyLikesLabel.Text = howmanylike.ToString() + English.GetLike();

                    haveiliked = !haveiliked;

                    likeornotImage.Source = "unlike.png";
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