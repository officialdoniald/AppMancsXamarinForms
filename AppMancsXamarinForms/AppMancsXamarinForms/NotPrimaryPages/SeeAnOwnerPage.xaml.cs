using AppMancsXamarinForms.BLL.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeAnOwnerPage : ContentPage
    {
        private int userID = -1;

        private SeeAnOwnerProfileViewModel seeAnOwnerProfileViewModel =
                new SeeAnOwnerProfileViewModel();

        private List<Pet> petList = new List<Pet>();

        public SeeAnOwnerPage(int userid)
        {
            this.userID = userid;

            InitializeComponent();

            var currentWidth = Application.Current.MainPage.Width;

            var optimalWidth = currentWidth / 3 ;

            User user = DependencyService.Get<BLL.IDBAccess.IBlobStorage>().GetUserByID(userID);

            if (!String.IsNullOrEmpty(user.ProfilePictureURL))
            {
                profilePictureImage.Source = ImageSource.FromUri(new Uri(user.ProfilePictureURL));

                profilePictureImage.HeightRequest = optimalWidth;
            }

            optimalWidth = currentWidth / 5;

            userNameLabel.Text = user.FirstName + " " + user.LastName;

            petList = seeAnOwnerProfileViewModel.GetPet(user.id);

            foreach (var item in petList)
            {
                StackLayout oneGrid = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical
                };

                Image petProfilePictureImage = new Image(){
                    Source = ImageSource.FromUri(new Uri(item.ProfilePictureURL)),
                    HeightRequest = optimalWidth,
                    Aspect = Aspect.AspectFill,
                    HorizontalOptions = LayoutOptions.Center
                };

                Label petNameLabel = new Label()
                {
                    Text = item.Name,
                    HorizontalOptions = LayoutOptions.Center
                };

                var goToPetProfileTapped = new TapGestureRecognizer();
                goToPetProfileTapped.Tapped += (s, e) => {
                    var searchResultPage = new SeeAPetProfile(item.id);

                    Navigation.PushAsync(searchResultPage);
                };

                petProfilePictureImage.GestureRecognizers.Add(goToPetProfileTapped);
                petNameLabel.GestureRecognizers.Add(goToPetProfileTapped);

                oneGrid.Children.Add(petProfilePictureImage);
                oneGrid.Children.Add(petNameLabel);

                //mainStackLayout.Children.Add(oneGrid);

                //pictureListGrid.Children.Add(oneGrid);
                petsStackLayout.Children.Add(oneGrid);
            }
        }
    }
}