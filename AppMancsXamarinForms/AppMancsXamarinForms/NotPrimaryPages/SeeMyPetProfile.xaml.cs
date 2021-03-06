﻿using Model;
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
    public partial class SeeMyPetProfile : ContentPage
    {
        private int petid = -1;

        private double currentWidth = 0;
        private double optimalWidth = 0;

        private Pet thisPet = new Pet();

        private List<Petpictures> petPictureListfromDB = new List<Petpictures>();

        private Petpictures petpictures = new Petpictures();

        public SeeMyPetProfile(int petid)
        {
            InitializeComponent();

            this.petid = petid;

            InitializeThePetPictures();
        }

        private async Task InitializeThePetPictures()
        {
            await Task.Run(() =>
            {
                thisPet = GlobalVariables.ConvertMyPetListToPet(GlobalVariables.Mypetlist.Where(u => u.petid == petid).FirstOrDefault());

                petPictureListfromDB = GlobalVariables.petProfileFragmentViewModel.GetPetPictureURL(petid);
                Device.BeginInvokeOnMainThread(() =>
                {
                    followersLabel.Text = GlobalVariables.followersViewModel.GetUserList(this.petid).Count + " followers";

                    currentWidth = Application.Current.MainPage.Width;

                    optimalWidth = currentWidth / 3;

                    petnameLabel.Text = thisPet.Name;

                    profilePictureImage.HeightRequest = optimalWidth;
                    profilePictureImage.WidthRequest = optimalWidth;

                    profilePictureImage.Source = thisPet.ProfilePictureURL == "" ? "" : ImageSource.FromUri(new Uri(thisPet.ProfilePictureURL));
                });
                int left = 0;
                int top = 0;

                int i = 1;

                foreach (var item in petPictureListfromDB)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Image image = new Image();

                        image.Source = ImageSource.FromUri(new Uri(item.PictureURL));

                        image.HeightRequest = optimalWidth;

                        image.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            NumberOfTapsRequired = 1,
                            TappedCallback = delegate
                            {
                                OnPictureClicked(item);
                            }
                        });

                        image.Aspect = Aspect.AspectFill;

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
                    });
                }
            });
        }

		protected override void OnAppearing()
		{
            InitializeThePetPictures();
		}

		public void OnPictureClicked(Petpictures petpictures)
        {
            Navigation.PushAsync(new SeeMyPicturePage(petpictures));
        }

        private void updateButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdatePetProfilePage(thisPet.id));
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new FollowersPage(thisPet.id));
        }
    }
}