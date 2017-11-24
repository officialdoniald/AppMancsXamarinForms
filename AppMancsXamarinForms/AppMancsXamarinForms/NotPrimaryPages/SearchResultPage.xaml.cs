using Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        public SearchResultPage(List<Petpictures> petpicturesList)
        {
            this.petpicturesList = petpicturesList;

            InitializeComponent();

            int left = 0;
            int top = 0;

            int i = 1;

            foreach (var item in petpicturesList)
            {
                Image image = new Image();

                image.Source = ImageSource.FromUri(new Uri(item.PictureURL));

                image.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    TappedCallback = delegate
                    {
                        OnPictureClicked(item);
                    }
                });

                image.WidthRequest = 50;
                image.HeightRequest = 50;

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
            Navigation.PushAsync(new SeeAPicturePage(petpictures));
        }
    }
}