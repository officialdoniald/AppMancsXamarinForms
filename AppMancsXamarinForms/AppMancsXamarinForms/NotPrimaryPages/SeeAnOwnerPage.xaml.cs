using AppMancsXamarinForms.BLL.Helper;
using AppMancsXamarinForms.BLL.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeAnOwnerPage : ContentPage
    {
        private int userID = -1;

        private List<Pet> petList = new List<Pet>();

        double currentWidth = 0;

        double optimalWidth = 0;

        public SeeAnOwnerPage(int userid)
        {
            this.userID = userid;

            InitializeComponent();

            Initialize();
        }

        private async Task Initialize()
        {
            await Task.Run(() =>
            {
                User user = DependencyService.Get<BLL.IDBAccess.IBlobStorage>().GetUserByID(userID);

                if (!String.IsNullOrEmpty(user.ProfilePictureURL))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        currentWidth = Application.Current.MainPage.Width;

                        optimalWidth = currentWidth / 3;

                        profilePictureImage.Source = ImageSource.FromUri(new Uri(user.ProfilePictureURL));

                        profilePictureImage.HeightRequest = optimalWidth;
                    });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    currentWidth = Application.Current.MainPage.Width;

                    optimalWidth = currentWidth / 5;

                    userNameLabel.Text = user.FirstName + " " + user.LastName;
                });

                petList = GlobalVariables.seeAnOwnerProfileViewModel.GetPet(user.id);

                foreach (var item in petList)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StackLayout oneGrid = new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical
                        };

                        Image petProfilePictureImage = new Image()
                        {
                            Source = ImageSource.FromUri(new Uri(item.ProfilePictureURL)),
                            HeightRequest = optimalWidth,
                            Aspect = Aspect.AspectFill,
                            HorizontalOptions = LayoutOptions.Center
                        };

                        Label petNameLabel = new Label()
                        {
                            Text = item.Name,
                            Style = (Style)Resources["NormalLabel"]
                        };

                        var goToPetProfileTapped = new TapGestureRecognizer();
                        goToPetProfileTapped.Tapped += (s, e) =>
                        {
                            var searchResultPage = new SeeAPetProfile(item.id);

                            Navigation.PushAsync(searchResultPage);
                        };

                        petProfilePictureImage.GestureRecognizers.Add(goToPetProfileTapped);
                        petNameLabel.GestureRecognizers.Add(goToPetProfileTapped);

                        oneGrid.Children.Add(petProfilePictureImage);
                        oneGrid.Children.Add(petNameLabel);

                        petsStackLayout.Children.Add(oneGrid);
                    });
                }
            });
        }
    }
}