using Android.App;
using Android.Content;
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
        public void UpdateList(List<Field> newFields)
        {
            this.fields = newFields;
            NotifyDataSetChanged(); // Tells the ListView to redraw immediately
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var field = fields[position];
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.listview_field_item, null);

            view.FindViewById<TextView>(Resource.Id.tvFieldName).Text = field.Name;
            //view.FindViewById<TextView>(Resource.Id.tvSportType).Text = field.SportType;

            var btnAvailability = view.FindViewById<Button>(Resource.Id.btnAvailability);
            btnAvailability.Click += (s, e) =>
            {
                // Navigate to a page showing specific hours for this field
                Toast.MakeText(context, $"Opening schedule for {field.Name}", ToastLength.Short).Show();
                var intent = new Intent(context, typeof(AvailabilityPageActivity));

                // 2. Pack the field data as "Extras"
                // We pass the ID and Name so the next page knows which field it is looking at
                intent.PutExtra("FieldId", field.Field_Id.ToString());
                intent.PutExtra("FieldName", field.Name);

                // 3. Start the Activity
                context.StartActivity(intent);
            };

            return view;
        }
    }
}