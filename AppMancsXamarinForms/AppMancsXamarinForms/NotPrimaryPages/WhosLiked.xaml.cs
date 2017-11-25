using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms.NotPrimaryPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WhosLiked : ContentPage
    {
        private Pet thisPet = new Pet();

        private string userEmail = "";

        private int petpicturesid = -1;

        private List<User> users = new List<User>();

        private WhosLikedViewModel whosLikedViewModel = new WhosLikedViewModel();

        public WhosLiked (int petpicturesid)
		{
            FileStoreAndLoading fileStoreAndLoading = new FileStoreAndLoading();

            userEmail = fileStoreAndLoading.GetSomethingText("login.txt");

            this.petpicturesid = petpicturesid;

            //thisPet = whosLikedViewModel.GetPetByPetId(petpicturesid);

			InitializeComponent ();

            users = whosLikedViewModel.GetUserList(petpicturesid);

            List<ListViewWithPictureAndSomeText> listViewWithPictureAndSomeText = new List<ListViewWithPictureAndSomeText>();

            foreach (var item in users)
            {
                ListViewWithPictureAndSomeText listViewWith = new ListViewWithPictureAndSomeText()
                {
                    user = item,
                    Name = item.LastName + " " + item.FirstName
                    //pet = thisPet
                };

                if (!String.IsNullOrEmpty(item.ProfilePictureURL))
                {
                    listViewWith.ProfilePicture = ImageSource.FromUri(new Uri(item.ProfilePictureURL));
                }
                else
                {
                    listViewWith.ProfilePicture = "";
                }
                
                listViewWithPictureAndSomeText.Add(listViewWith);
            }

            userListView.ItemsSource = listViewWithPictureAndSomeText;
        }
        


        private void userListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedLVWPAST = (ListViewWithPictureAndSomeText)listView.SelectedItem;

            if (userEmail != selectedLVWPAST.user.Email)
            {
                var searchResultPage = new SeeAnOwnerPage(selectedLVWPAST.user.id);

                Navigation.PushAsync(searchResultPage);
            }
            else
            {
                var searchResultPage = new MyAccountPage();

                Navigation.PushAsync(searchResultPage);
            }
        }
    }
}