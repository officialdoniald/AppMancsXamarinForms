using AppMancsXamarinForms.BLL.Helper;
using AppMancsXamarinForms.BLL.ViewModel;
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
    public partial class SearchPage : ContentPage
    {
        List<SearchModel> searchModelList = new List<SearchModel>();

        public SearchPage()
        {
            InitializeComponent();

            searchListView.IsRefreshing = true;

            FirstTime();
        }

        private void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var list = GlobalVariables.searchFragmentViewModel.GetSearchModelWithKeyword(searchEntry.Text.ToLower(), searchModelList);

            searchListView.ItemsSource = list;
        }

        private void searchListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedSearchModel = (SearchModel)listView.SelectedItem;

            string hasht = selectedSearchModel.hashtag.Split('#')[1];

            var searchResultPage = new SearchResultPage(selectedSearchModel.petpicturesList, hasht);

            Navigation.PushAsync(searchResultPage);
        }

        private async Task FirstTime()
        {
            await Task.Run(() => {
                SetTheListView();
            });
        }

        private void SetTheListView()
        {
            searchModelList = GlobalVariables.searchFragmentViewModel.GetSearchModel();

            Device.BeginInvokeOnMainThread(()=>{
                searchListView.ItemsSource = searchModelList;

                searchListView.IsRefreshing = false;
            });
        }

        async void Handle_Refreshing(object sender, System.EventArgs e)
        {
            await Task.Run(() => {
                SetTheListView();
            });
        }
    }
}