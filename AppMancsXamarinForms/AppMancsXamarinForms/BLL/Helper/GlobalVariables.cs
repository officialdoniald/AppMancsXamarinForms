using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppMancsXamarinForms.BLL.ViewModel;
using AppMancsXamarinForms.LocalDB;
using FileStoringWithDependency.IFileStoreAndLoad;
using Model;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.Helper
{
    public static class GlobalVariables
    {
        public static List<WallListViewAdapter> wallListViewAdapter =
            new List<WallListViewAdapter>();
        
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
        /// Is Pet added?
        /// </summary>
        private static ContentPageFunctions contentPageFunctions = new ContentPageFunctions();

        public static ContentPageFunctions ContentPageFunctions
        {
            get => contentPageFunctions;
            set => contentPageFunctions = value;
        }

        /// <summary>
        /// Is Pet added?
        /// </summary>
        private static bool isUpdatedMyProfile = false;

        public static bool IsUpdatedMyProfile
        {
            get => isUpdatedMyProfile;
            set => isUpdatedMyProfile = value;
        }

        /// <summary>
        /// Is Pet added?
        /// </summary>
        private static bool addedPet = false;

        public static bool AddedPet
        {
            get => addedPet;
            set => addedPet = value;
        }

        /// <summary>
        /// Is Photo added?
        /// </summary>
        private static bool addedPhoto = false;

        public static bool AddedPhoto
        {
            get => addedPhoto;
            set => addedPhoto = value;
        }

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
            get => actualusersemail.ToLower();
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
                var pet = ConvertPetToMyPetList(item);

                var itit = LocalSQLiteDatabase.InsertMyPetsList(pet).Result;

                Mypetlist.Add(pet);
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

        /// <summary>
        /// Converts Mypetlist to Pet.
        /// </summary>
        /// <returns>The my pet list to pet.</returns>
        /// <param name="myPetsList">My pets list.</param>
        public static Pet ConvertMyPetListToPet(MyPetsList myPetsList)
        {
            return new Pet()
            {
                id = myPetsList.petid,
                Age = myPetsList.Age,
                HaveAnOwner = myPetsList.HaveAnOwner,
                Name = myPetsList.Name,
                PetType = myPetsList.PetType,
                ProfilePictureURL = myPetsList.ProfilePictureURL,
                Uploader = myPetsList.Uploader
            };
        }

        /// <summary>
        /// Converts the Pet to Mypetlist.
        /// </summary>
        /// <returns>The pet to my pet list.</returns>
        /// <param name="pet">Pet.</param>
        public static MyPetsList ConvertPetToMyPetList(Pet pet)
        {
            return new MyPetsList()
            {
                Age = pet.Age,
                HaveAnOwner = pet.HaveAnOwner,
                Name = pet.Name,
                petid = pet.id,
                PetType = pet.PetType,
                ProfilePictureURL = pet.ProfilePictureURL,
                Uploader = pet.Uploader
            };
        }

        /// <summary>
        /// Converts the pet pictures wall to petpictures.
        /// </summary>
        /// <returns>The pet pictures wall to petpictures.</returns>
        /// <param name="petpicturesWall">Petpictures wall.</param>
        public static Petpictures ConvertPetPicturesWallToPetpictures(PetpicturesWall petpicturesWall)
        {
            return new Petpictures()
            {
                id = petpicturesWall.petpicturesid,
                PetID = petpicturesWall.PetID,
                PictureURL = petpicturesWall.PictureURL,
                UploadDate = petpicturesWall.UploadDate
            };
        }

        /// <summary>
        /// Converts the pet pictures to petpictures wall.
        /// </summary>
        /// <returns>The pet pictures to petpictures wall.</returns>
        /// <param name="petpictures">Petpictures.</param>
        public static PetpicturesWall ConvertPetPicturesToPetpicturesWall(Petpictures petpictures)
        {
            return new PetpicturesWall()
            {
                petpicturesid = petpictures.id,
                PetID = petpictures.PetID,
                PictureURL = petpictures.PictureURL, 
                UploadDate = petpictures.UploadDate
            };
        }

        /// <summary>
        /// Converts my wall to wall list view adapter.
        /// </summary>
        /// <returns>The my wall to wall list view adapter.</returns>
        /// <param name="myWall">My wall.</param>
        public static WallListViewAdapter ConvertMyWallToWallListViewAdapter(MyWall myWall)
        {
            return new WallListViewAdapter()
            {
                profilepictureURL = ImageSource.FromUri(new Uri(myWall.profilepictureURL)),
                pictureURL = ImageSource.FromUri(new Uri(myWall.pictureURL)),
                hashtags = myWall.hashtags,
                followButtonText = myWall.followButtonText,
                petName = myWall.petName,
                howManyLikes = myWall.howManyLikes,
                wallItem = LocalSQLiteDatabase.GetWallItemById(myWall.wallItemid)
            };
        }

        /// <summary>
        /// Converts the wall list view adapter to my wall.
        /// </summary>
        /// <returns>The wall list view adapter to my wall.</returns>
        /// <param name="wallListViewAdapter">Wall list view adapter.</param>
        public static MyWall ConvertWallListViewAdapterToMyWall(WallListViewAdapter wallListViewAdapter)
        {
            return new MyWall()
            {
                profilepictureURL = wallListViewAdapter.profilepictureURL.ToString(),
                pictureURL = wallListViewAdapter.pictureURL.ToString(),
                hashtags = wallListViewAdapter.hashtags,
                followButtonText = wallListViewAdapter.followButtonText,
                petName = wallListViewAdapter.petName,
                howManyLikes = wallListViewAdapter.howManyLikes,
                wallItemid = wallListViewAdapter.wallItem.id
            };
        }

        /// <summary>
        /// Converts the wall item to wall.
        /// </summary>
        /// <returns>The wall item to wall.</returns>
        /// <param name="wallItem">Wall item.</param>
        public static Wall ConvertWallItemToWall(WallItem wallItem)
        {
            return new Wall()
            {
                id = wallItem.id,
                howmanylikes = wallItem.howmanylikes,
                haveILiked = wallItem.haveILiked,
                name = wallItem.name,
                ProfilePictureURL = wallItem.ProfilePictureURL,
                petpictures = LocalSQLiteDatabase.GetPetpicturesWallById(wallItem.petpicturesid)
            };
        }

        /// <summary>
        /// Converts the wall to wall item.
        /// </summary>
        /// <returns>The wall to wall item.</returns>
        /// <param name="wall">Wall.</param>
        public static WallItem ConvertWallToWallItem(Wall wall)
        {
            return new WallItem()
            {
                howmanylikes = wall.howmanylikes,
                haveILiked = wall.haveILiked,
                name = wall.name,
                ProfilePictureURL = wall.ProfilePictureURL, 
                petpicturesid = wall.petpictures.id
            };
        }


        /// <summary>
        /// Event handler when connection changes
        /// </summary>
        static event ConnectivityChangedEventHandler ConnectivityChanged; 

        public class ConnectivityChangedEventArgs : EventArgs
        {
            public bool IsConnected { get; set; }

        }

        public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);
    }

}
