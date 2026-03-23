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
    public class Reservation
    {
        [JsonProperty("reservation_id")]
        public string ReservationId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        public string FieldId { get; set; }

        public string FieldName { get; set; } // this can maybe be used and maybe stored in database

        // DateTime handles both the specific day and the time of the reservation
        [JsonProperty("starting_time")]
        public DateTime StartingTime { get; set; }

    }
}