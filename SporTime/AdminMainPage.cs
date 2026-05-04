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
    public class AdminMainPage : Activity
    {
        Button BtnUserList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.admin_layout);
            InitializeViews();
        }

        private async void InitializeViews()
        {
            BtnUserList = FindViewById<Button>(Resource.Id.btnUsersList);
            

            BtnUserList.Click += BtnUsersListClick;
            
        }

        

        private void BtnUsersListClick(object sender, EventArgs e)
        {
            StartActivity(typeof(AdminUsersListActivity));
        }
        
    }
}