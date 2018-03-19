using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.Helper
{
    public class ContentPageFunctions : ContentPage
    {
        public async Task CreatePositivDisplayAlert(string text)
        {
            await DisplayAlert("Success", text, "OK");
        }

        public async Task CreateNegativDisplayAlert(string text)
        {
            await DisplayAlert("Failed", text, "OK");
        }
    }
}

