using AppMancsXamarinForms.BLL.Languages;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class SeePictureFragmentViewModel
    {
        private English language = new English();

        public string likeText;
        public string unlikeText;

        public SeePictureFragmentViewModel()
        {
            likeText = language.Like();
            unlikeText = language.UnLike();
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

        public string LikeClick(string userEmail, int petpicturesid)
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

        public string UnLikeClick(string userEmail, int petpicturesid)
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

        public Pet GetPetById(int petid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);
        }

        public List<Likes> GetLikes(int petpicturesid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetLikeByPetpicturesID(petpicturesid);
        }

        public bool HaveILiked(string userEMAIL, int petpicturesid)
        {
            int userid = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEMAIL).id;

            Likes like = DependencyService.Get<IDBAccess.IBlobStorage>().GetLikeByUserID(userid, petpicturesid);

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
    }
}
