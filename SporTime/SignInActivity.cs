using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SporTime.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SporTime
{
    [Activity(Label = "SignInActivity")]
    public class SignInActivity : Activity
    {
        private const string TAG = "OmerApp";
        string Email = "1234@nga.com", Password = "123456";
        bool DebugMode = true;
        EditText UEmail, UPassword;
        Button btnSignIn, btnGoToSignUp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.sign_in_page);

            // Create your application here
            InitializeViews();
        }

        private void InitializeViews()
        {
            UEmail = FindViewById<EditText>(Resource.Id.etEmail);
            UPassword = FindViewById<EditText>(Resource.Id.etPassword);
            if (DebugMode)
            {
                UEmail.Text = Email;
                UPassword.Text = Password;
            }

            btnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            btnGoToSignUp = FindViewById<Button>(Resource.Id.btnGoToSignUp);

            btnGoToSignUp.Click += BtnGoToSignUp_Click;
            btnSignIn.Click += BtnSignIn_Click;

        }

        private async void BtnSignIn_Click(object sender, EventArgs e)
        {
            string email = UEmail.Text.Trim();
            string password = UPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Toast.MakeText(this, "Please fill all fields", ToastLength.Short).Show();
                return;
            }

            try
            {
                // Call your helper
                var user = await FireBaseHelper.LoginAsync(email, password);

                if (user != null)
                {
                    
                    Toast.MakeText(this, "Login Successful!", ToastLength.Short).Show();
                    StartActivity(typeof(MainPageActivity));
                    // TODO: Navigate to the main app screen (e.g., FieldListActivity)
                    // StartActivity(typeof(FieldListActivity));
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Login Failed: " + ex.Message, ToastLength.Long).Show();
            }
        }

        private void BtnGoToSignUp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUpActivity));
        }
    }
}