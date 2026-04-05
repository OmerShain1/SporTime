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
    public class User
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        
    }
}