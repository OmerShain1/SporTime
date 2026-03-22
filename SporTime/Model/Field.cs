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
using Newtonsoft.Json;

namespace SporTime.Model
{
    public class Field
    {
        [JsonProperty("field_id")]
        public string Field_Id { get; set; }

        [JsonProperty("field_name")]
        public string Name { get; set; }

        [JsonProperty("opening_time")]
        public DateTime OpeningHour { get; set; }

        [JsonProperty("closing_time")]
        public DateTime ClosingHour { get; set; }

    }
}