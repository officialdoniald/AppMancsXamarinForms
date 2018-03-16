﻿using System;
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

        public Wall wallItem { get; set; }

        public ImageSource profilepictureURL { get; set; }

        public ImageSource pictureURL { get; set; }

        public string hashtags { get; set; }

        public string followButtonText { get; set; }

        public string petName { get; set; }

        public string howManyLikes { get; set; }
    }
}
