using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SporTime.ViewModel
{
    internal static class FireBaseHelper
    {
        private static FirebaseAuth Auth =>
            FirebaseAuth.Instance;

        // ---------- STATE ----------
        public static FirebaseUser CurrentUser =>
            Auth.CurrentUser;

        public static bool IsLoggedIn =>
            CurrentUser != null;

        public static string UserId =>
            CurrentUser?.Uid;

        public static string Email =>
            CurrentUser?.Email;

        // ---------- AUTH ----------
        public static async Task<FirebaseUser> RegisterAsync(string email, string password)
        {
            var result = await Auth
                .CreateUserWithEmailAndPasswordAsync(email, password);

            return result.User;
        }
        public static async Task<FirebaseUser> LoginAsync(string email, string password)
        {
            var result = await Auth
                .SignInWithEmailAndPasswordAsync(email, password);

            return result.User;
        }

        public static void Logout()
        {
            Auth.SignOut();
        }
        // ---------- TOKEN ----------
        /// <summary>
        /// This is what you will later send to your backend
        /// </summary>
        public static async Task<string> GetIdTokenAsync(bool forceRefresh = false)
        {
            if (FirebaseAuth.Instance.CurrentUser == null)
                return null;

            var javaTask = FirebaseAuth.Instance
                .CurrentUser
                .GetIdToken(forceRefresh);

            // Convert Java Task → C# Task
            var result = await javaTask.AsAsync<GetTokenResult>();

            return result.Token;
        }
    }
}