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
        EditText etFirstName, etLastName, etEmail;
        Button btnSave, btnLogout, btnDelete;
        LinearLayout navHome, navNew;

        ApiService _apiService = new ApiService();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.account_page);

            InitializeViews();
            LoadUserData();
            InitializeNavigation();
        }

        private void InitializeViews()
        {
            etFirstName = FindViewById<EditText>(Resource.Id.etFirstName);
            etLastName = FindViewById<EditText>(Resource.Id.etLastName);
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnLogout = FindViewById<Button>(Resource.Id.btnLogout);
            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            // Navigation
            navHome = FindViewById<LinearLayout>(Resource.Id.navHome);
            navNew = FindViewById<LinearLayout>(Resource.Id.navNew);

            btnSave.Click += BtnSave_Click;
            btnLogout.Click += BtnLogout_Click;
            btnDelete.Click += BtnDelete_Click;

            navHome.Click += (s, e) => StartActivity(typeof(MainPageActivity));
            navNew.Click += (s, e) => { /* Start NewReservationActivity */ };
        }

        private void LoadUserData()
        {
            //Get user data from Backend
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