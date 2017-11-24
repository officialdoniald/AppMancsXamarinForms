using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppMancsXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            List<valami> asd = new List<valami>();

            asd.Add(new valami() { text = "asd"});
            asd.Add(new valami() { text = "asda" });

            eventListView.ItemsSource = asd;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label asd = (Label)sender;
            var s = asd.Text;

            var sd = (valami)asd.BindingContext;

            string h = sd.text;

            asd.Text = "kapdbe";
        }
    }
    public class valami
    {
        public string text { get; set; }
    }
}