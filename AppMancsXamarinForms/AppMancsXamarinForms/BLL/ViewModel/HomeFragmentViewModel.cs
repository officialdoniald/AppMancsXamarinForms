using AppMancsXamarinForms.BLL.Languages;
using Model;
using System.Collections.Generic;
using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class HomeFragmentViewModel
    {
        private English language = new English();

        public string GetHashtags(int petpicturesid)
        {
            var hashtags = DependencyService.Get<IDBAccess.IBlobStorage>().GetHashtagsByPetpictureID(petpicturesid);

            string returnString = "";

            foreach (var item in hashtags)
            {
                returnString = returnString + "#" + item.hashtag + " ";
            }

            return returnString;
        }

        public List<Wall> GetWallList()
        {
            List<Wall> wallList = new List<Wall>();

            List<Petpictures> petpictureList = new List<Petpictures>();

            List<Following> followingList = new List<Following>();

            followingList = DependencyService.Get<IDBAccess.IBlobStorage>().GetFollowingByuserID(GlobalVariables.ActualUser.id);

            List<Petpictures> petpictures = new List<Petpictures>();

            if (followingList is null)
            {
                return new List<Wall>();
            }
            else
            {
                foreach (var item in followingList)
                {
                    petpictures = new List<Petpictures>();

                    petpictures = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetpictureByID(item.FUserID);

                    foreach (var item2 in petpictures)
                    {
                        Wall wall = new Wall();
                        Pet pet = new Pet();

                        pet = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(item.FUserID);
                        wall.petpictures = item2;
                        wall.name = pet.Name;
                        wall.howmanylikes = DependencyService.Get<IDBAccess.IBlobStorage>().GetLikeByPetpicturesID(item2.id).Count;
                        wall.ProfilePictureURL = pet.ProfilePictureURL;

                        Likes like = DependencyService.Get<IDBAccess.IBlobStorage>().GetLikeByUserID(GlobalVariables.ActualUser.id, item2.id);

                        if (like is null)
                        {
                            wall.haveILiked = false;
                        }
                        else
                        {
                            wall.haveILiked = true;
                        }

                        wallList.Add(wall);
                    }
                }
                return wallList;
            }
        }

        public string Unlike(int petpicturesid)
        {
            Likes likes = new Likes()
            {
                UserID = GlobalVariables.ActualUser.id,
                Petpicturesid = petpicturesid
            };

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeleteLikesNotByParam(likes);

            if (!success)
            {
                return language.SomethingWentWrong();
            }
            else
            {
                return language.Empty();
            }
        }

        public string LikePicture(int petpicturesid)
        {
            Likes likes = new Likes()
            {
                UserID = GlobalVariables.ActualUser.id,
                Petpicturesid = petpicturesid
            };

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().InsertLikes(likes);

            if (!success)
            {
                return language.SomethingWentWrong();
            }
            else
            {
                return language.Empty();
            }
        }

    }
}
