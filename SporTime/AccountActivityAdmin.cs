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
    [Activity(Label = "AccountActivityAdmin")]
    public class AccountActivityAdmin : Activity
    {
        ListView lvUsers;
        ApiService _apiService = new ApiService();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.admin_layout);
            InitializeViews();
        }

        private async void InitializeViews()
        {
            lvUsers = FindViewById<ListView>(Resource.Id.lvUsers);
            List<User> users = await _apiService.GetUsersAsync();
            lvUsers.Adapter = new UserListViewAdapter(this, users);
        }
    }
}