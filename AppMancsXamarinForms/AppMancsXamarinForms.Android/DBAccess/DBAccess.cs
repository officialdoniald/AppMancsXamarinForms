using Model;
using DBAccess;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(AppMancsXamarinForms.Droid.DBAccess.DBAccess))]
namespace AppMancsXamarinForms.Droid.DBAccess
{
    public class DBAccess: AppMancsXamarinForms.BLL.IDBAccess.IBlobStorage
    {
        private DatabaseConnections databaseConnections =
            new DatabaseConnections();

        #region GetFunctions

        public List<User> GetUsers()
        {
            return databaseConnections.GetUsers();
        }

        public List<Pet> GetPets()
        {
            return databaseConnections.GetPets();
        }

        public List<Donates> GetDonates()
        {
            return databaseConnections.GetDonates();
        }

        public List<Petpictures> GetPetpictures()
        {
            return databaseConnections.GetPetpictures();
        }

        public List<Likes> GetLikes()
        {
            return databaseConnections.GetLikes();
        }

        public List<Favoritepets> GetFavoritepets()
        {
            return databaseConnections.GetFavoritepets();
        }

        public List<Following> GetFollowing()
        {
            return databaseConnections.GetFollowing();
        }

        public List<Hashtags> GetHashtags()
        {
            return databaseConnections.GetHashtags();
        }

        #endregion

        #region GetByIDFunctions

        public User GetUserByEmail(string Email)
        {
            return databaseConnections.GetUserByEmail(Email);
        }

        public User GetUserByID(int ID)
        {
            return databaseConnections.GetUserByID(ID);
        }

        public Pet GetPetByID(int ID)
        {
            return databaseConnections.GetPetByID(ID);
        }

        public List<Pet> GetPetsByUserID(int UserID)
        {
            return databaseConnections.GetPetsByUserID(UserID);
        }

        public Donates GetDonateByID(int ID)
        {
            return databaseConnections.GetDonateByID(ID);
        }

        public List<Petpictures> GetPetpictureByID(int ID)
        {
            return databaseConnections.GetPetpictureByID(ID);
        }

        public List<Likes> GetLikeByPetpicturesID(int ID)
        {
            return databaseConnections.GetLikeByPetpicturesID(ID);
        }

        public Likes GetLikeByUserID(int userid, int petpicturesid)
        {
            return databaseConnections.GetLikeByUserID(userid, petpicturesid);
        }

        public Favoritepets GetFavoritepetByID(int ID)
        {
            return databaseConnections.GetFavoritepetByID(ID);
        }

        public Following GetFollowingByID(int userID, int petid)
        {
            return databaseConnections.GetFollowingByID(userID, petid);
        }

        public Petpictures GetOnePetpicturesByID(int ID)
        {
            return databaseConnections.GetOnePetpicturesByID(ID);
        }

        public List<Following> GetFollowingByfuserID(int userID, int petid)
        {
            return databaseConnections.GetFollowingByfuserID(userID, petid);
        }

        public List<Following> GetFollowingByuserID(int userID)
        {
            return databaseConnections.GetFollowingByuserID(userID);
        }

        public List<Hashtags> GetHashtagsByPetpictureID(int petpicturesid)
        {
            return databaseConnections.GetHashtagsByPetpictureID(petpicturesid);
        }

        #endregion

        #region InsertFunctions

        public bool InsertUser(User user)
        {
            return databaseConnections.InsertUser(user);
        }

        public int InsertPet(Pet pet)
        {
            return databaseConnections.InsertPet(pet);
        }

        public bool InsertDonates(Donates donates)
        {
            return databaseConnections.InsertDonates(donates);
        }

        public int InsertPetpictures(Petpictures petpictures)
        {
            return databaseConnections.InsertPetpictures(petpictures);
        }

        public bool InsertFavoritepets(Favoritepets favoritepets)
        {
            return databaseConnections.InsertFavoritepets(favoritepets);
        }

        public bool InsertLikes(Likes likes)
        {
            return databaseConnections.InsertLikes(likes);
        }

        public bool InsertFollowing(Following following)
        {
            return databaseConnections.InsertFollowing(following);
        }

        public bool InsertHashtags(Hashtags hashtags)
        {
            return databaseConnections.InsertHashtags(hashtags);
        }

        #endregion

        #region UpdateFunctions

        public bool UpdateUser(int ID, User user)
        {
            return databaseConnections.UpdateUser(ID, user);
        }

        public bool UpdatePet(int ID, Pet pet)
        {
            return databaseConnections.UpdatePet(ID, pet);
        }

        public bool UpdateDonates(int ID, Donates donates)
        {
            return databaseConnections.UpdateDonates(ID, donates);
        }

        public bool UpdatePetpictures(int ID, Petpictures petpictures)
        {
            return databaseConnections.UpdatePetpictures(ID, petpictures);
        }

        public bool UpdateFavoritepets(int ID, Favoritepets favoritepets)
        {
            return databaseConnections.UpdateFavoritepets(ID, favoritepets);
        }

        public bool UpdateLikes(int ID, Likes likes)
        {
            return databaseConnections.UpdateLikes(ID, likes);
        }

        public bool UpdateFollowing(int ID, Following following)
        {
            return databaseConnections.UpdateFollowing(ID, following);
        }

        #endregion

        #region DeleteFunctions

        public bool DeleteUser(User user)
        {
            return databaseConnections.DeleteUser(user);
        }

        public bool DeletePet(Pet pet)
        {
            return databaseConnections.DeletePet(pet);
        }

        public bool DeleteDonates(Donates donates)
        {
            return databaseConnections.DeleteDonates(donates);
        }

        public bool DeleteFavoritepets(Favoritepets favoritepets)
        {
            return databaseConnections.DeleteFavoritepets(favoritepets);
        }

        public bool DeleteLikes(Likes likes)
        {
            return databaseConnections.DeleteLikes(likes);
        }

        public bool DeleteLikesNotByParam(Likes likes)
        {
            return databaseConnections.DeleteLikesNotByParam(likes);
        }

        public bool DeletePetpictures(Petpictures petpictures)
        {
            return databaseConnections.DeletePetpictures(petpictures);
        }

        public bool DeleteFollowing(int userid, int petid)
        {
            return databaseConnections.DeleteFollowing(userid, petid);
        }

        public bool DeleteAccount(int UserID)
        {
            return databaseConnections.DeleteAccount(UserID);
        }

        public bool DeletePetpictures(int PetID, int userid)
        {
            return databaseConnections.DeletePetpictures(PetID, userid);
        }

        public User GetUserByFacebookID(string facebookID)
        {
            return databaseConnections.GetUserByFacebookID(facebookID);
        }

        #endregion
    }
}