using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMancsXamarinForms.BLL.ViewModel
{
    public class SearchFragmentViewModel
    {
        public List<Hashtags> GetHashtags()
        {
            return DependencyService.Get<IDBAccess.IBlobStorage>().GetHashtags();
        }

        public List<SearchModel> GetSearchModel()
        {
            List<Hashtags> hashtags = GetHashtags();

            var hastagsordered = hashtags.OrderBy(a => a.hashtag).GroupBy(a => a.hashtag);

            List<SearchModel> searchModelList = new List<SearchModel>();

            List<Petpictures> petpictures = new List<Petpictures>();

            SearchModel searchModel = new SearchModel();

            foreach (var item in hastagsordered)
            {
                foreach (var item1 in item)
                {
                    petpictures.Add(DependencyService.Get<IDBAccess.IBlobStorage>().GetOnePetpicturesByID(item1.petpicturesid));
                }

                searchModel.hashtag = item.Key;
                searchModel.petpicturesList = petpictures;

                searchModelList.Add(searchModel);

                petpictures = new List<Petpictures>();

                searchModel = new SearchModel();
            }

            return searchModelList;
        }

        public List<SearchModel> GetSearchModelWithKeyword(string searchword, List<SearchModel> searchModelList)
        {
            List<SearchModel> searchablelist = new List<SearchModel>();

            if (!String.IsNullOrEmpty(searchword))
            {
                searchablelist = (

                                 from g
                                 in searchModelList
                                 where g.hashtag.Contains(searchword)
                                 select g

                                 ).ToList();
            }
            else
            {
                return searchModelList;
            }

            return searchablelist;
        }

    }
}
