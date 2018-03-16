using SQLite;

namespace AppMancsXamarinForms.LocalDB
{
    public class LastIndex
    {
        [PrimaryKey, AutoIncrement]
        public int id
        {
            get;
            set;
        }

        public int LastIndexInTheWall
        {
            get;
            set;
        }
    }
}
