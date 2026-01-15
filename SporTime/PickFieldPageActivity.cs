using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SporTime.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SporTime.Model;

namespace SporTime
{
    [Activity(Label = "PickFieldPageActivity")]
    public class PickFieldPageActivity : Activity
    {
        ListView lvFields;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pick_field_page);

            lvFields = FindViewById<ListView>(Resource.Id.lvFields);

            // Dummy data - later this comes from your DB
            var fieldList = new List<Field>
        {
            new Field { Field_Id = "12312", Name = "Arena Soccer 1", SportType = "Soccer" },
            new Field { Field_Id = "3123", Name = "Main Tennis Court", SportType = "Tennis" }
        };

            lvFields.Adapter = new FieldAdapter(this, fieldList);

            InitializeNavigation();
            InitializeViews();
        }

        private void InitializeViews()
        {
            
        }

        private void InitializeNavigation()
        {
            var navNew = FindViewById<LinearLayout>(Resource.Id.navNew);
            var navAccount = FindViewById<LinearLayout>(Resource.Id.navAccount);
            var navHome = FindViewById<LinearLayout>(Resource.Id.navHome);

            navHome.Click += (s, e) => {
                StartActivity(typeof(MainPageActivity));
            };

            navAccount.Click += (s, e) => {
                StartActivity(typeof(AccountPageActivity));
            };
        }
    }
}