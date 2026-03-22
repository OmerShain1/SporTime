using Android.App;
using Android.OS;
using Android.Text;
using Android.Widget;
using SporTime.Model;
using SporTime.ViewModel;
using System.Collections.Generic;
using System.Linq; // Required for .Where()

namespace SporTime
{
    [Activity(Label = "PickFieldPageActivity")]
    public class PickFieldPageActivity : Activity
    {
        // UI Elements
        ListView lvFields;
        EditText etSearch;

        
        FieldAdapter adapter;
        List<Field> allFields;       // The master list (data)
        List<Field> displayedFields; // The filtered list (data)

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.pick_field_page);

            lvFields = FindViewById<ListView>(Resource.Id.lvFields);

            InitializeViews();

            // FIX 2: Initialize the master data list here
            allFields = new List<Field>
            {
                new Field { Field_Id = "12312", Name = "Arena Soccer 1" },
                new Field { Field_Id = "3123", Name = "Main Tennis Court"},
                new Field { Field_Id = "4444", Name = "Downtown Basketball"}
            };

            // Initially, the displayed list is a copy of everything
            displayedFields = new List<Field>(allFields);

            // FIX 3: Assign the adapter to the class variable 'adapter'
            adapter = new FieldAdapter(this, displayedFields);
            lvFields.Adapter = adapter;

            InitializeNavigation();
        }

        private void InitializeViews()
        {
            etSearch = FindViewById<EditText>(Resource.Id.etSearch);
            etSearch.TextChanged += etSearch_TextChanged;
        }

        private void etSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Get what the user typed
            string query = e.Text.ToString().ToLower();

            if (string.IsNullOrEmpty(query))
            {
                // If search is empty, reset to show everything
                displayedFields = new List<Field>(allFields);
            }
            else
            {
                // FIX 4: Filter 'allFields' (the data), NOT 'lvFields' (the UI widget)
                displayedFields = allFields
                    .Where(field => field.Name.ToLower().Contains(query) ||
                                    field.SportType.ToLower().Contains(query))
                    .ToList();
            }

            // FIX 5: Now 'adapter' exists, so we can call UpdateList
            if (adapter != null)
            {
                adapter.UpdateList(displayedFields);
            }
        }

        private void InitializeNavigation()
        {
            var navNew = FindViewById<LinearLayout>(Resource.Id.navNew);
            var navAccount = FindViewById<LinearLayout>(Resource.Id.navAccount);
            var navHome = FindViewById<LinearLayout>(Resource.Id.navHome);

            // Safety check in case navNew is null on this page
            if (navHome != null)
                navHome.Click += (s, e) => StartActivity(typeof(MainPageActivity));

            if (navAccount != null)
                navAccount.Click += (s, e) => StartActivity(typeof(AccountPageActivity));
        }
    }
}