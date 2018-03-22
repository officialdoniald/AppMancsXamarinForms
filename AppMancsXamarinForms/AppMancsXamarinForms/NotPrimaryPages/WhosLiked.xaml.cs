﻿using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.FileStoreAndLoad;
using Model;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppMancsXamarinForms.BLL.Helper;
using System.Threading.Tasks;

namespace AppMancsXamarinForms.NotPrimaryPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WhosLiked : ContentPage
    {
        //private Pet thisPet = new Pet();

        private int petpicturesid = -1;

        private List<User> users = new List<User>();

        public WhosLiked (int petpicturesid)
		{
            this.petpicturesid = petpicturesid;

            //thisPet = whosLikedViewModel.GetPetByPetId(petpicturesid);

			InitializeComponent ();

            Initialize();
        }
        
        private async Task Initialize(){
            await Task.Run(()=>{
                users = GlobalVariables.whosLikedViewModel.GetUserList(petpicturesid);

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

                Device.BeginInvokeOnMainThread(()=>{
                    userListView.ItemsSource = listViewWithPictureAndSomeText;

                    userListView.IsRefreshing = false;
                });
            });
        }

        private void userListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedLVWPAST = (ListViewWithPictureAndSomeText)listView.SelectedItem;

            if (GlobalVariables.ActualUsersEmail != selectedLVWPAST.user.Email)
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

        void Handle_Refreshing(object sender, System.EventArgs e)
        {
            Initialize();
        }
    }
}