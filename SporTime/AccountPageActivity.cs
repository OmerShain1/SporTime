using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using SporTime.Model;
using SporTime.Service;
using SporTime.ViewModel;
using SporTime.Model;
using System;

namespace SporTime
{
    [Activity(Label = "Account")]
    public class AccountPageActivity : Activity
    {

        Button btnLogout, btnDelete;
        LinearLayout navHome, navNew;

        ApiService _apiService = new ApiService();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.account_page);

            InitializeViews();
            InitializeNavigation();
        }

        private void InitializeViews()
        {

            btnLogout = FindViewById<Button>(Resource.Id.btnLogout);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            // Navigation
            navHome = FindViewById<LinearLayout>(Resource.Id.navHome);
            navNew = FindViewById<LinearLayout>(Resource.Id.navNew);

            
            btnLogout.Click += BtnLogout_Click;
            btnDelete.Click += BtnDelete_Click;

            navHome.Click += (s, e) => StartActivity(typeof(MainPageActivity));
            navNew.Click += (s, e) => { /* Start NewReservationActivity */ };
        }

        
        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Logic to send updated User object to your ASP.NET Backend
            Toast.MakeText(this, "Profile Updated!", ToastLength.Short).Show();
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            //FireBaseHelper.Logout();
            StartActivity(typeof(MainActivity));
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            _apiService.DeleteUserAsync(FireBaseHelper.UserId);
            FireBaseHelper.DeleteUserAsync();
            StartActivity(typeof(MainActivity));
        }
        private void InitializeNavigation()
        {
            var navNew = FindViewById<LinearLayout>(Resource.Id.navNew);
            var navAccount = FindViewById<LinearLayout>(Resource.Id.navAccount);
            var navHome = FindViewById<LinearLayout>(Resource.Id.navHome);

            navNew.Click += (s, e) => {
                StartActivity(typeof(PickFieldPageActivity));
            };

            navHome.Click += (s, e) => {
                StartActivity(typeof(MainPageActivity));
            };
        }
    }
}