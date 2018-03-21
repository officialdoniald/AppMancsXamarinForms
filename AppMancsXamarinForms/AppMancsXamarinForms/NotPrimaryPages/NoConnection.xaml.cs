﻿using Plugin.Connectivity;
using Xamarin.Forms;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    public partial class NoConnection : ContentPage
    {
        private bool wasNotConn = false;

        public NoConnection()
        {
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                if (CrossConnectivity.Current.IsConnected && !wasNotConn)
                {
                    wasNotConn = true;
                 
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                }
                else
                {
                    wasNotConn = false;
                }
            };
        }
    }
}
