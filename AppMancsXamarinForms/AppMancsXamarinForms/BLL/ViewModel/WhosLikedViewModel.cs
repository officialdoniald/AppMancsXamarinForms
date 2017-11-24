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
    }
}
