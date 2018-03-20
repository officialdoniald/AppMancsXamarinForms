using Plugin.Connectivity;
using Xamarin.Forms;

namespace AppMancsXamarinForms.NotPrimaryPages
{
    public partial class NoConnection : ContentPage
    {
        public NoConnection()
        {
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                }
            };
        }
    }
}
