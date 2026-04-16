using Android.App;
using Android.Views;
using Android.Widget;
using SporTime.Model;
using SporTime.Service;
using System;
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

        public override Reservation this[int position] => items[position];
        public override int Count => items.Count;
        public override long GetItemId(int position) => position;
        public override Java.Lang.Object GetItem(int position) => null;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.listview_reservation_item, null);

            view.FindViewById<TextView>(Resource.Id.tvFieldName).Text = item.FieldName;
            DateTime endTime = item.StartingTime.AddHours(1);
            view.FindViewById<TextView>(Resource.Id.tvDateTime).Text =
                $"{item.StartingTime:dd/MM/yyyy} at {item.StartingTime:HH:mm} - {endTime:HH:mm}";

            var btnDelete = view.FindViewById<Button>(Resource.Id.btnDelete);

            // Store the reservation id on the button so the handler can read it
            btnDelete.Tag = item.ReservationId;
            btnDelete.Enabled = true;

            // Now the -= actually removes the existing subscription
            btnDelete.Click -= OnDeleteClicked;
            btnDelete.Click += OnDeleteClicked;

            return view;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (!btn.Enabled) return;      // guard against any stray double-fire
            btn.Enabled = false;           // disable immediately

            int reservationId = ((Java.Lang.Integer)btn.Tag).IntValue();
            var item = items.Find(r => r.ReservationId == reservationId);
            if (item == null) return;

            var apiService = new ApiService();
            bool success = await apiService.DeleteReservationAsync(reservationId);

            if (success)
            {
                items.Remove(item);
                NotifyDataSetChanged();
                Toast.MakeText(context, "Reservation deleted", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(context, "Failed to delete reservation", ToastLength.Short).Show();
                btn.Enabled = true;
            }
        }
    }
}