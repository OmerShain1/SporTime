using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SporTime.Model;
using SporTime.Service;
using SporTime.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SporTime
{
    [Activity(Label = "MainPageActivity", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class MainPageActivity : Activity
    {
        ListView lvReservations;
        ApiService _apiService = new ApiService();
        List<Reservation> myReservations = new List<Reservation>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Firebase.FirebaseApp.Instance == null)
                Firebase.FirebaseApp.InitializeApp(this);

            SetContentView(Resource.Layout.main_page);
            lvReservations = FindViewById<ListView>(Resource.Id.lvReservations);

            InitializeNavigation();
            await LoadUserReservations();

            //ProcessIntentData(Intent);
            lvReservations.Adapter = new ReservationAdapter(this, myReservations);
        }

        private async Task LoadUserReservations()
        {
            string userId = Firebase.Auth.FirebaseAuth.Instance.CurrentUser?.Uid;
            if (string.IsNullOrEmpty(userId)) return;

            myReservations = await _apiService.GetUserReservationsAsync(userId);
        }

        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);
        //    Intent = intent;
        //    ProcessIntentData(intent);
        //    ((BaseAdapter)lvReservations.Adapter).NotifyDataSetChanged();
        //}

        //private void ProcessIntentData(Intent intent)
        //{
        //    var fieldName = intent.GetStringExtra("FieldName");
        //    var dateStr = intent.GetStringExtra("ReservationDate");
        //    var timeStr = intent.GetStringExtra("ReservationTime");

        //    if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(dateStr) && !string.IsNullOrEmpty(timeStr))
        //    {
        //        try
        //        {
        //            var times = timeStr.Split(" - ");
        //            DateTime startParsed = DateTime.ParseExact(
        //                $"{dateStr} {times[0]}", "dd/MM/yyyy HH:mm",
        //                System.Globalization.CultureInfo.InvariantCulture);

        //            myReservations.Add(new Reservation
        //            {
        //                FieldName = fieldName,
        //                StartingTime = startParsed
        //            });

        //            intent.RemoveExtra("FieldName");
        //            intent.RemoveExtra("ReservationDate");
        //            intent.RemoveExtra("ReservationTime");
        //        }
        //        catch (Exception ex)
        //        {
        //            Android.Util.Log.Error("SporTime", "Parsing Error: " + ex.Message);
        //        }
        //    }
        //}

        private void InitializeNavigation()
        {
            var navNew = FindViewById<LinearLayout>(Resource.Id.navNew);
            var navAccount = FindViewById<LinearLayout>(Resource.Id.navAccount);
            var navHome = FindViewById<LinearLayout>(Resource.Id.navHome);

            navNew.Click += (s, e) => {
                StartActivity(typeof(PickFieldPageActivity));
            };

            navAccount.Click += (s, e) => {
                StartActivity(typeof(AccountPageActivity));
            };
        }
    }
}