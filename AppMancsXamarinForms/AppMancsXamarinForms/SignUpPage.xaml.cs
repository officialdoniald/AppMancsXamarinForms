using AppMancsXamarinForms.BLL.ViewModel;
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
    public partial class SignUpPage : ContentPage
    {
        SignupPageViewModel signupPageViewModel = new SignupPageViewModel();

        public SignUpPage()
        {
            InitializeComponent();
        }

        private void signupButton_Clicked(object sender, EventArgs e)
        {
            User user = new User()
            {
                Email = emailEntry.Text.ToLower(),
                Password = pwEntry.Text,
                FirstName = firstnameEntry.Text,
                LastName = lastnameEntry.Text,
                FacebookId = "",
                ProfilePictureURL = ""
            };

            string success = signupPageViewModel.SignUp(user);

            if (!String.IsNullOrEmpty(success))
            {
                //HIBA
            }
            else
            {
                Navigation.PopToRootAsync();
            }
        }
    }
}