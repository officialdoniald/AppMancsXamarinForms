﻿using AppMancsXamarinForms.BLL.Languages;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public List<Wall> GetWallList(string userEmail)
        {
            List<Wall> wallList = new List<Wall>();

            List<Petpictures> petpictureList = new List<Petpictures>();

            List<Following> followingList = new List<Following>();

            User user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail);

            followingList = DependencyService.Get<IDBAccess.IBlobStorage>().GetFollowingByuserID(user.id);

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

                        Likes like = DependencyService.Get<IDBAccess.IBlobStorage>().GetLikeByUserID(user.id, item2.id);

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

        public string Unlike(string userEmail, int petpicturesid)
        {
            User user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail);

            Likes likes = new Likes()
            {
                UserID = user.id,
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

        public string LikePicture(string userEmail, int petpicturesid)
        {
            User user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail);

            Likes likes = new Likes()
            {
                UserID = user.id,
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