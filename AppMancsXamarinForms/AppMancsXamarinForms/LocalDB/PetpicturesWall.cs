using System;
using SQLite;

namespace AppMancsXamarinForms.LocalDB
{
    public class PetpicturesWall
    {
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get;
            set;
        }

        public int petpicturesid { get; set; }

        public int PetID { get; set; }

        public string PictureURL { get; set; }

        public string UploadDate { get; set; }
    }
}
