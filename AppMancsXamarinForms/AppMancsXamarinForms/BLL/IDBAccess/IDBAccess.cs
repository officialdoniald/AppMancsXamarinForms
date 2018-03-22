using Model;
using System.Collections.Generic;

namespace AppMancsXamarinForms.BLL.IDBAccess
{
    public interface IBlobStorage
    {
        #region GetFunctions

        List<User> GetUsers();

        List<Pet> GetPets();

        List<Donates> GetDonates();

        List<Petpictures> GetPetpictures();

        List<Likes> GetLikes();

        List<Favoritepets> GetFavoritepets();

        List<Following> GetFollowing();

        List<Hashtags> GetHashtags();

        #endregion

        #region GetByIDFunctions

        User GetUserByEmail(string Email);

        User GetUserByFacebookID(string facebookID);

        User GetUserByID(int ID);

        Pet GetPetByID(int ID);

        List<Pet> GetPetsByUserID(int UserID);

        Donates GetDonateByID(int ID);

        List<Petpictures> GetPetpictureByID(int ID);

        List<Likes> GetLikeByPetpicturesID(int ID);

        Likes GetLikeByUserID(int userid, int petpicturesid);

        Favoritepets GetFavoritepetByID(int ID);

        Following GetFollowingByID(int userID, int petid);

        Petpictures GetOnePetpicturesByID(int ID);

        List<Following> GetFollowingByfuserID(int userID, int petid);

        List<Following> GetFollowingByuserID(int userID);

        List<Hashtags> GetHashtagsByPetpictureID(int petpicturesid);

        #endregion

        #region InsertFunctions

        bool InsertUser(User user);

        int InsertPet(Pet pet);

        bool InsertDonates(Donates donates);

        int InsertPetpictures(Petpictures petpictures);

        bool InsertFavoritepets(Favoritepets favoritepets);

        bool InsertLikes(Likes likes);

        bool InsertFollowing(Following following);

        bool InsertHashtags(Hashtags hashtags);

        #endregion

        #region UpdateFunctions

        bool UpdateUser(int ID, User user);

        bool UpdatePet(int ID, Pet pet);

        bool UpdateDonates(int ID, Donates donates);

        bool UpdatePetpictures(int ID, Petpictures petpictures);

        bool UpdateFavoritepets(int ID, Favoritepets favoritepets);

        bool UpdateLikes(int ID, Likes likes);

        bool UpdateFollowing(int ID, Following following);

        #endregion

        #region DeleteFunctions

        bool DeleteUser(User user);

        bool DeletePet(Pet pet);

        bool DeleteDonates(Donates donates);

        bool DeleteFavoritepets(Favoritepets favoritepets);

        bool DeleteLikes(Likes likes);

        bool DeleteLikesNotByParam(Likes likes);

        bool DeletePetpictures(Petpictures petpictures);

        bool DeleteFollowing(int userid, int petid);

        bool DeleteAccount(int UserID);

        bool DeletePetpictures(int PetID, int userid);

        #endregion
    }
}
