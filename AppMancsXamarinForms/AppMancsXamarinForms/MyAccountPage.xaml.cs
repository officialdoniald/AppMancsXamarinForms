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

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountPage : ContentPage
    {
        private string userEmail = "";

        private List<Pet> petList = new List<Pet>();

        private SeeAnOwnerProfileViewModel seeAnOwnerProfileViewModel =
                new SeeAnOwnerProfileViewModel();

        public MyAccountPage()
        {
            InitializeComponent();

            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            User user = seeAnOwnerProfileViewModel.GetUserByEmail(userEmail);

            userNameLabel.Text = user.FirstName + " " + user.LastName;

            if(!String.IsNullOrEmpty(user.ProfilePictureURL))
            {
                profilePictureImage.Source = ImageSource.FromUri(new Uri(user.ProfilePictureURL));
            }

            petList = seeAnOwnerProfileViewModel.GetPet(user.id);

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