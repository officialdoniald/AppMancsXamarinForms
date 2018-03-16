using SQLite;

namespace AppMancsXamarinForms.LocalDB
{
    public class MyPetsList
    {
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get;
            set;
        }

        public int petid { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string PetType { get; set; }

        public int HaveAnOwner { get; set; }

        public int Uploader { get; set; }

        public string ProfilePictureURL { get; set; }
    }
}
