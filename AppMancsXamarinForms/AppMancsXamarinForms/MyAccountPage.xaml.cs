using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using AppMancsXamarinForms.NotPrimaryPages;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountPage : ContentPage
    {
        private List<Pet> petList = new List<Pet>();

        public MyAccountPage()
        {
            InitializeComponent();

            userNameLabel.Text = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;

            if(!String.IsNullOrEmpty(GlobalVariables.ActualUser.ProfilePictureURL))
            {
                profilePictureImage.Source = ImageSource.FromUri(new Uri(GlobalVariables.ActualUser.ProfilePictureURL));
            }

            petList = GlobalVariables.seeAnOwnerProfileViewModel.GetPet(GlobalVariables.ActualUser.id);

            List<ListViewWithPictureAndSomeText> listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

            foreach (var item in petList)
            {
                listViewWithPictureAndSomeText.Add(new ListViewWithPictureAndSomeText()
                {
                    pet = item,
                    ProfilePicture = ImageSource.FromUri(new Uri(item.ProfilePictureURL)),
                    Name = item.Name
                });
            }

            petListView.ItemsSource = listViewWithPictureAndSomeText;
        }

        protected override void OnAppearing()
        {
            var currentWidth = Application.Current.MainPage.Width;

            var optimalWidth = currentWidth / 3;

            profilePictureImage.HeightRequest = optimalWidth;
        }

        private void petListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedLVWPAST = (ListViewWithPictureAndSomeText)listView.SelectedItem;

            var searchResultPage = new SeeMyPetProfile(selectedLVWPAST.pet.id);

            Navigation.PushAsync(searchResultPage);
        }

        private void otherButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OtherPage());
        }

        private void updateProfileButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UpdateProfilePage());
        }

        private void addPetButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPetPage());
        }
    }
}