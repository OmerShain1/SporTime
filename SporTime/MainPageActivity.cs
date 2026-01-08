using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SporTime.Model;
using SporTime.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SporTime
{
    [Activity(Label = "MainPageActivity")]
    public class MainPageActivity : Activity
    {
        ListView lvReservations;
        List<Reservation> myReservations;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Safety check for Firebase if navigating from login
            if (Firebase.FirebaseApp.Instance == null)
                Firebase.FirebaseApp.InitializeApp(this);

            SetContentView(Resource.Layout.main_page);

            lvReservations = FindViewById<ListView>(Resource.Id.lvReservations);
            

            // 1. Create data using your new class structure
            myReservations = new List<Reservation>
        {
            new Reservation
            {
                Reservation_Id = "101",
                FieldName = "Main Soccer Field",
                Start = new DateTime(2026, 1, 15, 18, 0, 0),
                End = new DateTime(2026, 1, 15, 19, 0, 0)
            },
            new Reservation
            {
                Reservation_Id = "102",
                FieldName = "Tennis Court B",
                Start = new DateTime(2026, 1, 16, 10, 0, 0),
                End = new DateTime(2026, 1, 16, 11, 0, 0)
            }
        };

            // 2. Set the Adapter
            lvReservations.Adapter = new ReservationAdapter(this, myReservations);

            InitializeNavigation();
            
        }

        

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