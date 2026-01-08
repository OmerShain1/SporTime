using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SporTime.Model
{
    public class Field
    {
        public string Field_Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string SportType { get; set; }
        public DateTime OpeningHour { get; set; }
        public DateTime ClosingHour { get; set; }

    }
}