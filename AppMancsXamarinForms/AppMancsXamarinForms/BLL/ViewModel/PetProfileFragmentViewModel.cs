using AppMancsXamarinForms.BLL.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class PetProfileFragmentViewModel
    {
        public string followText;
        public string unfollowText;
        
        public PetProfileFragmentViewModel()
        {
            followText = English.Follow();
            unfollowText = English.UnFollow();
        }

        public List<Petpictures> GetPetPictureURL(int petid)
        {
            List<Petpictures> petpictureList = new List<Petpictures>();

            petpictureList = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetpictureByID(petid);

            return petpictureList;
        }

        public Pet GetPetFromDBByID(int petid)
        {
            Pet pet = new Pet();

            pet = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);

            return pet;
        }

        public string UnFollow(string userEmail, int petid)
        {
            int userid = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail).id;

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().DeleteFollowing(userid, petid);

            if (success)
            {
                return English.Empty();
            }
            else
            {
                return English.SomethingWentWrong();
            }
        }

        public bool HaveIAlreadyFollow(string userEmail, int petid)
        {
            int userid = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail).id;

            Following following = DependencyService.Get<IDBAccess.IBlobStorage>().GetFollowingByID(userid, petid);

            if (following is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string FollowAPet(string userEmail, int petid)
        {
            int userid = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail).id;

            Following following = new Following()
            {
                FUserID = petid,
                UserID = userid
            };

            bool success = DependencyService.Get<IDBAccess.IBlobStorage>().InsertFollowing(following);

            if (success)
            {
                return English.Empty();
            }
            else
            {
                return English.SomethingWentWrong();
            }
        }

    }
}
