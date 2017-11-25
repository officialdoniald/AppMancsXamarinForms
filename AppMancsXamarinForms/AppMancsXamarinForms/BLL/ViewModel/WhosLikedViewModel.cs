using Model;
using AppMancsXamarinForms.BLL.IDBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        public bool IsMyPet(int petid, string userEmail)
        {
            var user = DependencyService.Get<IDBAccess.IBlobStorage>().GetUserByEmail(userEmail);

            var pet = DependencyService.Get<IDBAccess.IBlobStorage>().GetPetByID(petid);

            if (pet.Uploader == user.id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
