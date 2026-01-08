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
    public class Reservation
    {
        public string Reservation_Id { get; set; }
        public string Field_Id { get; set; }
        public string FieldName { get; set; }
        public string User_Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}