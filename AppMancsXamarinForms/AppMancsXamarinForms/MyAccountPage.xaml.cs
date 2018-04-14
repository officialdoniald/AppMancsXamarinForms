using AppMancsXamarinForms.NotPrimaryPages;
using Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;
using Plugin.Connectivity;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountPage : ContentPage
    {
        List<ListViewWithPictureAndSomeText> listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

        public MyAccountPage()
        {
            InitializeComponent();

            userNameLabel.Text = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;
        }

        protected override void OnAppearing()
        {
            var currentWidth = Application.Current.MainPage.Width;

            var optimalWidth = currentWidth / 3;

            profilePictureImage.HeightRequest = optimalWidth;
            profilePictureImage.WidthRequest = optimalWidth;
            
            if (!String.IsNullOrEmpty(GlobalVariables.ActualUser.ProfilePictureURL))
            {
                profilePictureImage.Source = ImageSource.FromUri(new Uri(GlobalVariables.ActualUser.ProfilePictureURL));
            }else
            {
                profilePictureImage.Source = "";
            }

            userNameLabel.Text = GlobalVariables.ActualUser.FirstName + " " + GlobalVariables.ActualUser.LastName;

            listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

            foreach (var item in GlobalVariables.Mypetlist)
            {
                listViewWithPictureAndSomeText.Add(new ListViewWithPictureAndSomeText()
                {
                    pet = GlobalVariables.ConvertMyPetListToPet(item),
                    ProfilePicture = item.ProfilePictureURL == string.Empty ? "" : ImageSource.FromUri(new Uri(item.ProfilePictureURL)),
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