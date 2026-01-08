using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace SporTime
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button GoToSignIn, GoToSignUp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.start_page);

            InitializeViews();
        }

        private void InitializeViews()
        {
            GoToSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            GoToSignUp = FindViewById<Button>(Resource.Id.btnSignUp);

            GoToSignIn.Click += GoToSignIn_Click;
            GoToSignUp.Click += GoToSignUp_Click;
        }

        private void GoToSignUp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUpActivity));
        }

        private void GoToSignIn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignInActivity));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}