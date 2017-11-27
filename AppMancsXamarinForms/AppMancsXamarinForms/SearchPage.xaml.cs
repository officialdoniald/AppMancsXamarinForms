﻿using AppMancsXamarinForms.BLL.ViewModel;
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
        SearchFragmentViewModel searchFragmentViewModel =
            new SearchFragmentViewModel();

        List<SearchModel> searchModelList = new List<SearchModel>();

        public SearchPage()
        {
            searchModelList = searchFragmentViewModel.GetSearchModel();

            InitializeComponent();

            searchListView.ItemsSource = searchModelList;
        }

        private void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var list = searchFragmentViewModel.GetSearchModelWithKeyword(searchEntry.Text, searchModelList);

            searchListView.ItemsSource = list;
        }

        private void searchListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            var selectedSearchModel = (SearchModel)listView.SelectedItem;

            var searchResultPage = new SearchResultPage(selectedSearchModel.petpicturesList, selectedSearchModel.hashtag);

            Navigation.PushAsync(searchResultPage);
        }
    }
}