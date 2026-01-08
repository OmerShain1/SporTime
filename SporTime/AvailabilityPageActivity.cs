using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using SporTime.Model;

namespace SporTime
{
    [Activity(Label = "Availability")]
    public class AvailabilityPageActivity : Activity
    {
        // UI Elements
        LinearLayout dateContainer;
        ListView lvTimeSlots;
        LinearLayout navHome, navNew, navAccount;

        // Data
        DateTime selectedDate = DateTime.Now;
        List<string> timeSlots;
        string selectedFieldId;
        string selectedFieldName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.availability_page);

            // 1. Get data passed from the previous NewReservationActivity
            selectedFieldId = Intent.GetStringExtra("FieldId") ?? "0";
            selectedFieldName = Intent.GetStringExtra("FieldName") ?? "Field";

            InitializeViews();
            GenerateDateButtons();
            LoadTimeSlots();
        }

        private void InitializeViews()
        {
            dateContainer = FindViewById<LinearLayout>(Resource.Id.dateContainer);
            lvTimeSlots = FindViewById<ListView>(Resource.Id.lvTimeSlots);

            // Bottom Navigation setup
            navHome = FindViewById<LinearLayout>(Resource.Id.navHome);
            navNew = FindViewById<LinearLayout>(Resource.Id.navNew);
            navAccount = FindViewById<LinearLayout>(Resource.Id.navAccount);

            // Navigation Click Events
            navHome.Click += (s, e) => StartActivity(typeof(MainPageActivity));
            navNew.Click += (s, e) => StartActivity(typeof(PickFieldPageActivity));
            navAccount.Click += (s, e) => StartActivity(typeof(AccountPageActivity));

            // Time Slot Selection
            lvTimeSlots.ItemClick += LvTimeSlots_ItemClick;
        }

        private void GenerateDateButtons()
        {
            dateContainer.RemoveAllViews();

            // Create buttons for the next 7 days
            for (int i = 0; i < 7; i++)
            {
                var date = DateTime.Now.AddDays(i);
                Button btnDate = new Button(this);

                // Styling the button to be a square/block
                var lp = new LinearLayout.LayoutParams(180, 180);
                lp.SetMargins(10, 5, 10, 5);
                btnDate.LayoutParameters = lp;

                // Format: "15/01 Mon"
                btnDate.Text = date.ToString("dd/MM\nddd");
                btnDate.TextSize = 12;

                // Highlight the selected date
                if (date.Date == selectedDate.Date)
                {
                    btnDate.SetBackgroundColor(Android.Graphics.Color.ParseColor("#6200EE")); // Your theme purple
                    btnDate.SetTextColor(Android.Graphics.Color.White);
                }
                else
                {
                    btnDate.SetBackgroundColor(Android.Graphics.Color.White);
                    btnDate.SetTextColor(Android.Graphics.Color.Black);
                }

                btnDate.Click += (s, e) =>
                {
                    selectedDate = date;
                    GenerateDateButtons(); // Refresh UI to show new selection
                    LoadTimeSlots();       // Refresh time slots for this date
                };

                dateContainer.AddView(btnDate);
            }
        }

        private void LoadTimeSlots()
        {
            timeSlots = new List<string>();

            // Business Rule: Fixed 1-hour blocks from 08:00 to 22:00
            for (int h = 8; h < 22; h++)
            {
                string slot = $"{h:D2}:00 - {h + 1:D2}:00";
                timeSlots.Add(slot);
            }

            // Using a simple ArrayAdapter with your custom time_slot_item layout
            var adapter = new ArrayAdapter<string>(this, Resource.Layout.day_item, Resource.Id.lvTimeSlots, timeSlots);
            lvTimeSlots.Adapter = adapter;
        }

        private void LvTimeSlots_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string pickedTime = timeSlots[e.Position];

            // Create a confirmation dialog
            var alert = new AlertDialog.Builder(this);
            alert.SetTitle("Confirm Reservation");
            alert.SetMessage($"Do you want to book {selectedFieldName} on {selectedDate.ToShortDateString()} at {pickedTime}?");

            alert.SetPositiveButton("Confirm", (senderAlert, args) => {
                // TODO: Here you will send the POST request to your ASP.NET Backend
                Toast.MakeText(this, "Reservation Saved Successfully!", ToastLength.Long).Show();

                // Return to Home screen
                //StartActivity(typeof(PickTimeActivity));
                Finish();
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) => { });

            alert.Show();
        }
    }
}