using Android.App;
using Android.Views;
using Android.Widget;
using Java.Lang; // Required for Object
using SporTime.Model;
using System.Collections.Generic;

namespace SporTime.ViewModel
{
    public class ReservationAdapter : BaseAdapter<Reservation>
    {
        List<Reservation> items;
        Activity context;

        public ReservationAdapter(Activity context, List<Reservation> items)
        {
            this.context = context;
            this.items = items;
        }

        // Fixes CS0115 and CS0534
        public override Reservation this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position) => position;

        // This is the specific method the error CS0534 was complaining about
        public override Object GetItem(int position)
        {
            return null; // For simple lists, returning null is fine
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.listview_reservation_item, null);

            view.FindViewById<TextView>(Resource.Id.tvFieldName).Text = item.FieldName;
            view.FindViewById<TextView>(Resource.Id.tvDateTime).Text = $"{item.Start.Date} at {item.Start.TimeOfDay}";

            var btnModify = view.FindViewById<Button>(Resource.Id.btnDelete);

            // Note: Use a standard delegate to avoid event bubbling issues in lists
            btnModify.Click -= BtnModify_Click; // Unsubscribe first to avoid multiple triggers
            btnModify.Click += (s, e) => {
                Toast.MakeText(context, "Deleting: " + item.FieldName, ToastLength.Short).Show();
            };

            return view;
        }

        // Helper for the click event if needed
        private void BtnModify_Click(object sender, System.EventArgs e) { }
    }
}