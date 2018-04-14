using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class PeopleSearchPageViewModel
    {
        public List<User> GetUserWithKeyword(string keyword)
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetUsersByKeyword(keyword);
        }

        public List<UserJustWithPicAndName> GetUserByKeyWord(string keyword, List<UserJustWithPicAndName> users)
        {
            List<UserJustWithPicAndName> searchablelist = new List<UserJustWithPicAndName>();

            if (!String.IsNullOrEmpty(keyword))
            {
                searchablelist = (
                                 from g
                                 in users
                                 where g.Name.Contains(keyword)
                                 select g
                                 ).ToList();
            }
            else
            {
                return searchablelist;
            }

            return searchablelist;
        }
    }
}