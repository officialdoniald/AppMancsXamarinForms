using System;
using System.Collections.Generic;
using Model;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class FollowersViewModel
    {
        public List<User> GetUserList(int petpicturesid)
        {
            List<User> userList = new List<User>();

            List<Following> likeList = DependencyService.Get<IDBAccess.IBlobStorage>().GetFollowingByfuserID(petpicturesid);

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
