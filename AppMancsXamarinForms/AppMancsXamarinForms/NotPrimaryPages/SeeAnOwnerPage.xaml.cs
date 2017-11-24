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

        public SeeAnOwnerPage (int userid)
		{
            this.userID = userid;

			InitializeComponent ();

            User user = seeAnOwnerProfileViewModel.GetUser(userID);

            profilePictureImage.Source = ImageSource.FromUri(new Uri(user.ProfilePictureURL));

            userNameLabel.Text = user.FirstName + " " + user.LastName;

            petList = seeAnOwnerProfileViewModel.GetPet(user.id);

            petListView.ItemsSource = petList;

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

            var searchResultPage = new SeeAPetProfile(selectedLVWPAST.pet.id);

            Navigation.PushAsync(searchResultPage);
        }
    }
}