using Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultPage : ContentPage
    {
        List<Petpictures> petpicturesList =
            new List<Petpictures>();

        public SearchResultPage()
        {
            InitializeComponent();
        }

        public SearchResultPage(List<Petpictures> petpicturesList, string hashtag)
        {
            this.Title = "#" + hashtag;

            this.petpicturesList = petpicturesList;

            InitializeComponent();

            var currentWidth = Application.Current.MainPage.Width;

            var optimalWidth = currentWidth / 3;

            int left = 0;
            int top = 0;

            int i = 1;

            foreach (var item in petpicturesList)
            {
                Image image = new Image();

                image.Source = ImageSource.FromUri(new Uri(item.PictureURL));

                image.HeightRequest = optimalWidth;

                image.Aspect = Aspect.AspectFill;

                image.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    TappedCallback = delegate
                    {
                        OnPictureClicked(item);
                    }
                });

                searchResultGrid.Children.Add(image, top, left);

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
            }
        }
        
        public void OnPictureClicked(Petpictures petpictures)
        {
            if (!GlobalVariables.whosLikedViewModel.IsMyPet(petpictures.PetID))
            {
                Navigation.PushAsync(new SeeAPicturePage(petpictures));
            }
            else
            {
                Navigation.PushAsync(new SeeMyPicturePage(petpictures));
            }
        }
    }
}