using System;
using Model;
using SQLite;

namespace AppMancsXamarinForms.LocalDB
{
    public class WallItem
    {
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get;
            set;
        }
        
        public int petpicturesid { get; set; }

        public int howmanylikes { get; set; }

        public bool haveILiked { get; set; }

        public string name { get; set; }

        public string ProfilePictureURL { get; set; }
    }
}
