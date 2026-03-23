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
        string OpeningHour, ClosingHour;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.availability_page);

            // 1. Get data passed from the previous PickFieldActivity
            selectedFieldId = Intent.GetStringExtra("FieldId") ?? "0";
            selectedFieldName = Intent.GetStringExtra("FieldName") ?? "Field";
            OpeningHour = Intent.GetStringExtra("OpeningHour") ?? "08:00:00";
            ClosingHour = Intent.GetStringExtra("ClosingHour") ?? "22:00:00";

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

                    //http request for availability here
                };

                dateContainer.AddView(btnDate);
            }
        }

        private void LoadTimeSlots()
        {
            timeSlots = new List<string>();

            // 1. Safely parse the string variables into C# TimeSpan objects.
            // We use TryParse so the app doesn't crash if the string is empty or malformed.
            if (!TimeSpan.TryParse(OpeningHour, out TimeSpan startTime))
            {
                startTime = new TimeSpan(8, 0, 0); // Fallback to 08:00 if parsing fails
            }

            if (!TimeSpan.TryParse(ClosingHour, out TimeSpan endTime))
            {
                endTime = new TimeSpan(22, 0, 0); // Fallback to 22:00 if parsing fails
            }

            // 2. Loop from the opening time to the closing time
            TimeSpan currentSlot = startTime;
            TimeSpan slotDuration = new TimeSpan(1, 0, 0); // 1-hour duration

            while (currentSlot < endTime)
            {
                TimeSpan nextSlot = currentSlot.Add(slotDuration);

                // 3. Format the strings to look clean, like "08:00 - 09:00"
                // The @"hh\:mm" format ensures it drops the seconds and adds leading zeros
                string slotText = $"{currentSlot.ToString(@"hh\:mm")} - {nextSlot.ToString(@"hh\:mm")}";

                timeSlots.Add(slotText);

                // Move to the next hour block
                currentSlot = nextSlot;
            }

            // 4. Update the UI
            var adapter = new ArrayAdapter<string>(this, Resource.Layout.day_item, Resource.Id.btnTimeSlot, timeSlots);
            lvTimeSlots.Adapter = adapter;

            // Note: I removed the `lvTimeSlots.ItemClick += LvTimeSlots_ItemClick;` from here.
            // You already have it inside InitializeViews(). If you subscribe to it twice, 
            // tapping a time slot will cause the confirmation popup to appear twice!
        }

        private void LvTimeSlots_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // 1. Get the specific string (e.g., "10:00 - 11:00") from the list
            string pickedTime = timeSlots[e.Position];

            // 2. Create a confirmation popup
            var alert = new AlertDialog.Builder(this);
            alert.SetTitle("Confirm Reservation");
            alert.SetMessage($"Do you want to book {selectedFieldName} on {selectedDate.ToShortDateString()} at {pickedTime}?");

            alert.SetPositiveButton("OK", (senderAlert, args) => {
                // --- THE NEW LOGIC STARTS HERE ---

                // 1. Create an Intent to return to the Main Page
                var intent = new Intent(this, typeof(MainPageActivity));

                // 2. Add the "Extras" (the data packet)
                
                intent.PutExtra("FieldName", selectedFieldName);
                intent.PutExtra("ReservationDate", selectedDate.ToString("dd/MM/yyyy"));
                intent.PutExtra("ReservationTime", pickedTime);

                // 3. Clear the Activity History
                // This prevents the user from clicking 'Back' and seeing the time picker again
                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

                // 4. Send the user home
                StartActivity(intent);

                // 5. Close this activity
                Finish();
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                // Do nothing if they cancel
            });

            alert.Show();
        }
    }
}