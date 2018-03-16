using System;
using System.IO;
using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.LocalDB;
using FileStoringWithDependency.IFileStoreAndLoad;
using Model;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.Helper
{
    public static class GlobalVariables
    {
        public static SignupPageViewModel signupPageViewModel = new SignupPageViewModel();

        public static WhosLikedViewModel whosLikedViewModel = new WhosLikedViewModel();

        public static AddpetFragmentViewModel addpetFragmentViewModel =
                new AddpetFragmentViewModel();

        public static UploadPhotoFragmentViewModel uploadPhotoFragmentViewModel =
            new UploadPhotoFragmentViewModel();

        public static UpdateProfileFragmentViewModel updateProfileFragmentViewModel
            = new UpdateProfileFragmentViewModel();

        public static UpdatePetFragmentViewModel updatePetFragmentViewModel =
            new UpdatePetFragmentViewModel();

        public static LoginPageViewModel loginPageViewModel =
            new LoginPageViewModel();

        public static HomeFragmentViewModel homeFragmentViewModel = new HomeFragmentViewModel();

        public static SearchFragmentViewModel searchFragmentViewModel = new SearchFragmentViewModel();

        public static SeeAnOwnerProfileViewModel seeAnOwnerProfileViewModel =
                new SeeAnOwnerProfileViewModel();

        public static OtherFragmentViewModel otherFragmentViewModel =
            new OtherFragmentViewModel();

        public static PetProfileFragmentViewModel petProfileFragmentViewModel =
            new PetProfileFragmentViewModel();

        public static SeePictureFragmentViewModel seePictureFragmentViewModel =
            new SeePictureFragmentViewModel();

        public static string logintxt = "login.txt";

        public static Stream f;

        public static string pathf = "";

        public static MediaFile mediaFile;

        public static string blobstorageurl = "https://officialdoniald.blob.core.windows.net/appmancs/";

        //What is the actual user now
        private static User actualuser;

        public static User ActualUser
        {
            get => actualuser;
            set => actualuser = value;
        }

        //What is the actual user's email
        private static string actualusersemail;

        public static string ActualUsersEmail
        {
            get => actualusersemail;
            set => actualusersemail = value;
        }

        //Already logged in?
        private static bool havetologin;

        public static bool HaveToLogin
        {
            get => havetologin;
            set => havetologin = value;
        }

        private static LocalDatabaseTable localSQLiteDatabase;

        public static LocalDatabaseTable LocalSQLiteDatabase
        {
            get{
                if (localSQLiteDatabase == null)
                {
                    localSQLiteDatabase = new LocalDatabaseTable(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("LocalDatabaseTable.db3"));
                }

                return localSQLiteDatabase;
            }
            set => localSQLiteDatabase = value;
        }

        /// <summary>
        /// Initializes the user.
        /// </summary>
        public static void InitializeUser()
        {
            ActualUser = DependencyService.Get<BLL.IDBAccess.IBlobStorage>().GetUserByEmail(ActualUsersEmail);
        }

        /// <summary>
        /// Initializes the users email.
        /// </summary>
        public static void InitializeUsersEmail()
        {
            try
            {
                ActualUsersEmail = DependencyService.Get<IFileStoreAndLoad>().LoadText(logintxt);

                if (!String.IsNullOrEmpty(ActualUsersEmail))
                {
                    HaveToLogin = false;
                }else
                {
                    HaveToLogin = true;
                }
            }
            catch (Exception)
            {
                HaveToLogin = true;
            }
        }

        public static void InitializeUsersEmailVariable()
        {
            ActualUsersEmail = DependencyService.Get<IFileStoreAndLoad>().LoadText(logintxt);
        }
    }
}
