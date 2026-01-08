using Android.App;
using Android.Views;
using Android.Widget;
using SporTime.Model;
using System.Collections.Generic;

namespace SporTime.ViewModel
{
    public class FieldAdapter : BaseAdapter<Field>
    {
        List<Field> fields;
        Activity context;

        public FieldAdapter(Activity context, List<Field> fields)
        {
            this.context = context;
            this.fields = fields;
        }

        public override Field this[int position] => fields[position];
        public override int Count => fields.Count;
        public override long GetItemId(int position) => position;
        public override Java.Lang.Object GetItem(int position) => null;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var field = fields[position];
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.listview_field_item, null);

            view.FindViewById<TextView>(Resource.Id.tvFieldName).Text = field.Name;
            view.FindViewById<TextView>(Resource.Id.tvSportType).Text = field.SportType;

            var btnAvailability = view.FindViewById<Button>(Resource.Id.btnAvailability);
            btnAvailability.Click += (s, e) =>
            {
                // Navigate to a page showing specific hours for this field
                Toast.MakeText(context, $"Opening schedule for {field.Name}", ToastLength.Short).Show();
            };

            return view;
        }
    }
}