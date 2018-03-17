using Model;
using SQLite;
using Xamarin.Forms;

namespace AppMancsXamarinForms.LocalDB
{
    public class MyWall
    {
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get;
            set;
        }

        public int wallItemid { get; set; }

        //majd ezt a kettőt konvertálni kell
        public string profilepictureURL { get; set; }

        //majd ezt a kettőt konvertálni kell
        public string pictureURL { get; set; }

        public string hashtags { get; set; }

        public string followButtonText { get; set; }

        public string petName { get; set; }

        public string howManyLikes { get; set; }
    }
}
