using System;
using System.Collections.Generic;
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
        public static SignupPageViewModel signupPageViewModel = 
            new SignupPageViewModel();

        public static WhosLikedViewModel whosLikedViewModel = 
            new WhosLikedViewModel();

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

        public static HomeFragmentViewModel homeFragmentViewModel = 
            new HomeFragmentViewModel();

        public static SearchFragmentViewModel searchFragmentViewModel = 
            new SearchFragmentViewModel();

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

        public static string blobstorageurl = 
            "https://officialdoniald.blob.core.windows.net/appmancs/";

        public static string databaseFileName = 
            "LocalDatabaseTable.db3";

        /// <summary>
        /// What is the actual user now?
        /// </summary>
        private static string[] myPetsString;

        public static string[] MyPetsString
        {
            get => myPetsString;
            set => myPetsString = value;
        }

        /// <summary>
        /// What is the actual user now?
        /// </summary>
        private static User actualuser;

        public static User ActualUser
        {
            get => actualuser;
            set => actualuser = value;
        }

        /// <summary>
        /// What is the actual user's email
        /// </summary>
        private static string actualusersemail;

        public static string ActualUsersEmail
        {
            get => actualusersemail;
            set => actualusersemail = value;
        }

        /// <summary>
        /// Already logged in?
        /// </summary>
        private static bool havetologin;

        public static bool HaveToLogin
        {
            get => havetologin;
            set => havetologin = value;
        }

        /// <summary>
        /// My pets list.
        /// </summary>
        private static List<MyPetsList> mypetlist;

        public static List<MyPetsList> Mypetlist
        {
            get => mypetlist;
            set => mypetlist = value;
        }

        /// <summary>
        /// The local SQLite database.
        /// </summary>
        private static LocalDatabaseTable localSQLiteDatabase;

        public static LocalDatabaseTable LocalSQLiteDatabase
        {
            get{
                if (localSQLiteDatabase == null)
                {
                    localSQLiteDatabase = new LocalDatabaseTable(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath(databaseFileName));
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

        /// <summary>
        /// Initializes the users email variable.
        /// </summary>
        public static void InitializeUsersEmailVariable()
        {
            ActualUsersEmail = DependencyService.Get<IFileStoreAndLoad>().LoadText(logintxt);
        }

        /// <summary>
        /// Gets my pets and store to the local SQLite DB.
        /// </summary>
        public static void GetMyPets()
        {
            LocalSQLiteDatabase.DeleteAllMyPetList();

            var myPetList = uploadPhotoFragmentViewModel.GetMyPets(ActualUser.id);

            Mypetlist = new List<MyPetsList>();

            foreach (var item in myPetList)
            {
                var myPetLists = new MyPetsList()
                {
                    Age = item.Age,
                    HaveAnOwner = item.HaveAnOwner,
                    Name = item.Name,
                    petid = item.id,
                    PetType = item.PetType,
                    ProfilePictureURL = item.ProfilePictureURL
                };

                var itit = LocalSQLiteDatabase.InsertMyPetsList(myPetLists).Result;

                Mypetlist.Add(myPetLists);
            }
        }

        /// <summary>
        /// Initializes the list of my pets.
        /// </summary>
        public static void InitializeTheMyPetList()
        {
            Mypetlist = new List<MyPetsList>();

            Mypetlist = LocalSQLiteDatabase.GetMyPetsList().Result;

            SetMyPetListString();
        }

        /// <summary>
        /// Sets my pet list string.
        /// </summary>
        public static void SetMyPetListString()
        {
            MyPetsString = new string[GlobalVariables.Mypetlist.Count];

            int i = 0;

            foreach (var item in GlobalVariables.Mypetlist)
            {
                MyPetsString[i] = item.Name;

                i++;
            }
        }

        // ha hozzá akarunk adni háziállatot, akkor a setmypetlist előtt inicializálni kell
        //a háziállatokat.
    }
}
