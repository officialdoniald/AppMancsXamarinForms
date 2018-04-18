﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class SeePictureFragmentViewModel
    {
        public string likeText;
        public string unlikeText;

        public SeePictureFragmentViewModel()
        {
            likeText = English.Like();
            unlikeText = English.UnLike();
        }

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

        public string LikeClick(int petpicturesid)
        {
            Likes likes = new Likes()
            {
                UserID = GlobalVariables.ActualUser.id,
                Petpicturesid = petpicturesid
            };

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().InsertLikes(likes);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                return English.Empty();
            }
        }

        public string UnLikeClick(int petpicturesid)
        {
            Likes likes = new Likes()
            {
                UserID = GlobalVariables.ActualUser.id,
                Petpicturesid = petpicturesid
            };

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeleteLikesNotByParam(likes);

            if (!success)
            {
                return English.SomethingWentWrong();
            }
            else
            {
                return English.Empty();
            }
        }

        public Pet GetPetById(int petid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);
        }

        public List<Likes> GetLikes(int petpicturesid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetLikeByPetpicturesID(petpicturesid);
        }

        public bool HaveILiked(int petpicturesid)
        {
            Likes like = DependencyService.Get<IDBAccess.IBlobStorage>().GetLikeByUserID(GlobalVariables.ActualUser.id, petpicturesid);

            if (like is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Petpictures GetPetpicturesByID(int id)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetOnePetpicturesByID(id);
        }

        public bool DeletePicture(Petpictures petpictures)
        {
            string asd = petpictures.PictureURL.Remove(0, GlobalVariables.blobstorageurl.Length);
            DependencyService.Get<IBlobStorage.IBlobStorage>().DeleteFileAsync(asd);

            return DependencyService.Get<IDBAccess.IBlobStorage>().DeletePetpictures(petpictures);
        }
    }
}
