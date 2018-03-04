using Model;
using AppMancsXamarinForms.BLL.IDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppMancsXamarinForms.BLL.Helper;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class WhosLikedViewModel
    {
        public List<User> GetUserList(int petpicturesid)
        {
            List<User> userList = new List<User>();

            List<Likes> likeList = DependencyService.Get<IDBAccess.IBlobStorage>().GetLikeByPetpicturesID(petpicturesid);

            foreach (var item in likeList)
            {
                User user = new User();

                user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByID(item.UserID);

                userList.Add(user);
            }

            return userList;
        }

        public bool IsMyPet(int petid)
        {
            var pet = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);

            if (pet.Uploader == GlobalVariables.ActualUser.id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Pet GetPetByPetId(int petpicturesid)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(DependencyService.Get<IDBAccess.IBlobStorage>().GetOnePetpicturesByID(petpicturesid).PetID);
        }
    }
}
