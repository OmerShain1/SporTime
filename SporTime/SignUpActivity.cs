using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using SporTime.Business_Logic;
using SporTime.ViewModel; // Important: This lets us use FireBaseHelper
using System;

namespace SporTime
{
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : Activity
    {
        EditText UEmail, UPassword;
        Button btnRegister, btnGoToSignIn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Make sure you have a layout file named 'sign_up_page.axml' or '.xml'
            SetContentView(Resource.Layout.sign_up_page);

            InitializeViews();
        }

        private void InitializeViews()
        {
            // Note: Make sure these IDs match what is in your XML layout!
            UEmail = FindViewById<EditText>(Resource.Id.etEmail);
            UPassword = FindViewById<EditText>(Resource.Id.etPassword);
            // If you don't have a confirm password field in XML, remove the next line
            
            btnRegister = FindViewById<Button>(Resource.Id.btnSignUp);
            btnGoToSignIn = FindViewById<Button>(Resource.Id.btnSignIn);

            btnRegister.Click += BtnRegister_Click;
            btnGoToSignIn.Click += BtnGoToSignIn_Click;
        }

        private void BtnGoToSignIn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignInActivity));
        }

        private async void BtnRegister_Click(object sender, EventArgs e)
        {
            string email = UEmail.Text.Trim();
            string password = UPassword.Text.Trim();
            

            // 1. Basic Validation
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Toast.MakeText(this, "Please fill all fields", ToastLength.Short).Show();
                return;
            }

            //if(ValidateInputs.ValidateEmail(email) == false || ValidateInputs.ValidatePhoneNumber())
            //{
            //    Toast.MakeText(this, "Please enter a valid email", ToastLength.Short).Show();
            //    return;
            //}
            // 3. Register with Firebase
            try
            {
                var user = await FireBaseHelper.RegisterAsync(email, password);

                if (user != null)
                {
                    Toast.MakeText(this, "Registration Successful!", ToastLength.Short).Show();
                    // Go back to login screen or main menu
                    Finish();
                }
            }
            catch (Exception ex)
            {
                // Display error (e.g., "Email already in use")
                Log.Error("OmerApp", ex.ToString());
                Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long).Show();
            }

        }
    }
}