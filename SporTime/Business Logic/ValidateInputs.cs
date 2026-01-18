using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SporTime.Business_Logic
{
    public static class ValidateInputs
    {
        public static bool ValidateEmail(string email)
        {
            // Basic email validation: check for presence of "@" and "."
            if (string.IsNullOrWhiteSpace(email))
                return false;
            int atIndex = email.IndexOf('@');
            int dotIndex = email.LastIndexOf('.');
            return atIndex > 0 && dotIndex > atIndex + 1 && dotIndex < email.Length - 1;
        }
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            // Basic phone number validation: check if it contains only digits and has a length of 10
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length == 10;
        }
        public static bool ValidateTime(DateTime dateTime) 
        {
            return dateTime >= DateTime.Now;
        }
    }
}