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
    public partial class UploadPhotoPage : ContentPage
    {
        public UploadPhotoPage()
        {
            InitializeComponent();
        }

        /*
         #region takeandselectphoto

        private async void takePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable ||
                !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Warning", "No camera available, please try again later!", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Name = "test.jpg"
            });

            if (file == null) return;

            photo = DependencyService.Get<IFileToByte>().ReadAllByteS(file.Path);

            f = file.GetStream();
            pathf = file.Path;

            imageFromDevice.Source = ImageSource.FromStream(() => file.GetStream());

        }

        private async void selectPhoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert(cimkek.GetWarning(), cimkek.GetNoPicking(), cimkek.GetOK());
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            mediaFile = file;

            if (file == null) return;

            photo = DependencyService.Get<IFileToByte>().ReadAllByteS(file.Path);

            f = file.GetStream();
            pathf = file.Path;

            imageFromDevice.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async void updatePictureButton_Clicked(object sender, EventArgs e)
        {
            indicatorStackLayout.IsVisible = activityIndicator.IsVisible = activityIndicator.IsRunning = true;
            mainStackLayout.IsVisible = false;

            if (photo == null || photo.Length == 0)
            {
                indicatorStackLayout.IsVisible = activityIndicator.IsVisible = activityIndicator.IsRunning = false;
                mainStackLayout.IsVisible = true;
                await DisplayAlert(cimkek.GetWarning(), cimkek.GetChoosePhoto(), cimkek.GetOK());
            }
            else
            {
                string uniqueBlobName = await blobStorage.UploadFileAsync(pathf, f);

                newUser.PROFILEPICTURE = "https://officialdoniald.blob.core.windows.net/invme/" + uniqueBlobName;

                try
                {
                    bool successInsert = false;

                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS:
                            successInsert = DependencyService.Get<IiOSDatabaseAccess>().UpdateUser(newUser.id, newUser);
                            break;
                        case Device.Android:
                            successInsert = DependencyService.Get<IAndroidDatabaseAccess>().UpdateUser(newUser.id, newUser);
                            break;
                        case Device.WinPhone:
                            successInsert = await DependencyService.Get<IDatabaseAccess>().UpdateUser(newUser.id, newUser);
                            break;
                        case Device.Windows:
                            successInsert = await DependencyService.Get<IDatabaseAccess>().UpdateUser(newUser.id, newUser);
                            break;
                        default:
                            indicatorStackLayout.IsVisible = activityIndicator.IsVisible = activityIndicator.IsRunning = false;
                            mainStackLayout.IsVisible = true;
                            await DisplayAlert(cimkek.GetWarning(), cimkek.GetSomethingWentWrong(), cimkek.GetOK());
                            break;
                    }

                    if (successInsert)
                    {
                        await DisplayAlert(cimkek.GetSuccess(), cimkek.GetYouHaceChangedyourPhoto(), cimkek.GetOK());

                        await Navigation.PushModalAsync(new NavigationPage(new MainPage(newUser)));
                    }
                }
                catch (Exception ex2)
                {
                    indicatorStackLayout.IsVisible = activityIndicator.IsVisible = activityIndicator.IsRunning = false;
                    mainStackLayout.IsVisible = true;
                    await DisplayAlert(cimkek.GetWarning(), ex2.Message, cimkek.GetOK());
                }

            }
        }

        #endregion
             */
    }
}