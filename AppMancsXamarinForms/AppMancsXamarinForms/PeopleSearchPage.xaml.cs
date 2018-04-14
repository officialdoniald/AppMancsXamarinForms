using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppMancsXamarinForms.BLL.Helper;
using AppMancsXamarinForms.NotPrimaryPages;
using Model;
using Xamarin.Forms;

namespace AppMancsXamarinForms
{
    public partial class PeopleSearchPage : ContentPage
    {
        List<UserJustWithPicAndName> userJustWithPicAndName = new List<UserJustWithPicAndName>();

        private bool isItFinished = false;

        public PeopleSearchPage()
        {
            InitializeComponent();

            Initialize();
        }

        private async Task Initialize()
        {
            await Task.Run(()=>
            {
                var list = GlobalVariables.peopleSearchPageViewModel.GetUserWithKeyword("");

                userJustWithPicAndName = new List<UserJustWithPicAndName>();

                foreach (var item in list)
                {
                    userJustWithPicAndName.Add(new UserJustWithPicAndName()
                    {
                        Name = item.FirstName + " " + item.LastName,
                        id = item.id,
                        ProfilePicture = item.ProfilePictureURL,
                        Email = item.Email
                    });
                }

                isItFinished = true;

                Device.BeginInvokeOnMainThread(() => 
                {
                    userListView.IsRefreshing = false;

                    userListView.ItemsSource = userJustWithPicAndName;
                });
            });
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            SetList();
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedLVWPAST = (UserJustWithPicAndName)listView.SelectedItem;

            if (GlobalVariables.ActualUsersEmail != selectedLVWPAST.Email)
            {
                var searchResultPage = new SeeAnOwnerPage(selectedLVWPAST.id);

                Navigation.PushAsync(searchResultPage);
            }
            else
            {
                var searchResultPage = new MyAccountPage();

                Navigation.PushAsync(searchResultPage);
            }
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            SetList();
        }

        private void SetList()
        {
            if (searchEntry.Text.Length >= 3 && isItFinished)
            {
                Device.BeginInvokeOnMainThread(()=>
                {
                    userListView.IsRefreshing = true;
                });

                userListView.ItemsSource = GlobalVariables.peopleSearchPageViewModel.GetUserByKeyWord(searchEntry.Text, userJustWithPicAndName);

                Device.BeginInvokeOnMainThread(() => 
                {
                    userListView.IsRefreshing = false;
                });
            }
        }

        void Handle_Refreshing(object sender, System.EventArgs e)
        {
            SetList();
        }
    }
}
