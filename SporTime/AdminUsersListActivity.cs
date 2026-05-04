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
using System.Linq;
using System.Text;

namespace SporTime
{
    [Activity(Label = "AdminUsersListActivity")]
    public class AdminUsersListActivity : Activity
    {
        private ListView lvUsers;
        Button btnRefresh;
        ApiService _apiService = new ApiService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.admin_users_list);

            InitializeViews();

            // Create your application here
        }

        private async void InitializeViews()
        {
            btnRefresh = FindViewById<Button>(Resource.Id.btnRefresh);
            btnRefresh.Click += BtnRefresh_Click;

            lvUsers = FindViewById<ListView>(Resource.Id.lvUsers);
                List<User> users = await _apiService.GetUsersAsync();
            lvUsers.Adapter = new UserListViewAdapter(this, users);
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            List<User> users = await _apiService.GetUsersAsync();
            lvUsers.Adapter = new UserListViewAdapter(this, users);
        }
    }
}