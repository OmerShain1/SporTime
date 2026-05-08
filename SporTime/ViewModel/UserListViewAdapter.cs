using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using SporTime.Model;
using SporTime.ViewModel;
using SporTime.Service;
using System.Collections.Generic;

namespace SporTime.ViewModel
{
    public class UserListViewAdapter : BaseAdapter<User>
    {
        List<User> items;
        Activity context;

        public UserListViewAdapter(Activity context, List<User> items)
        {
            this.context = context;
            this.items = items;
        }

        public override User this[int position] => items[position];
        public override int Count => items.Count;
        public override long GetItemId(int position) => position;
        public override Java.Lang.Object GetItem(int position) => null;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.user_listview_item, null);

            view.FindViewById<TextView>(Resource.Id.tvUserEmail).Text = item.Email;

            var btnDelete = view.FindViewById<Button>(Resource.Id.btnDeleteUser);

            btnDelete.Click -= OnDeleteClick;
            btnDelete.Click += async (s, e) =>
            {
                ApiService apiService = new ApiService();
                apiService.DeleteUserAsync(item.UserId);
                FireBaseHelper.DeleteUserAsync();


            };

            return view;
        }

        private void OnDeleteClick(object sender, System.EventArgs e) { }
    }
}