
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Android.App;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SporTime.Model;
using static Android.Gms.Common.Apis.Api;
//147.236.126.156
namespace SporTime.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Field>> GetFieldsAsync()
        {
            Log.Debug("ApiService", "Starting to fetch fields from the server...");
            try
            {
                // 2. The URL of your Python server
                // IMPORTANT: See the note below about this address!
                string url = "https://sportime-backend.onrender.com/fields";

                // 3. Send the driver to get the data (this happens in the background)
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                Log.Debug("ApiService", $"Received response with status code: {response.StatusCode}");
                if (response.IsSuccessStatusCode)
                {
                    // 4. Open the box (read the JSON string)
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    // 5. Turn the JSON text into actual C# Field objects
                    List<Field> fields = JsonConvert.DeserializeObject<List<Field>>(jsonResult);

                    return fields;
                    Log.Debug("ApiService", $"Fetched {fields.Count} fields from the server.");
                }
            }
            catch (Exception ex)
            {
                Log.Debug("ApiService", $"Error fetching fields: {ex.Message}");
            }

            // Return an empty list if something went wrong
            return new List<Field>();
        }



        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                string url = "https://sportime-backend.onrender.com/users";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<User>>(jsonResult);
                }
            }
            catch (Exception ex)
            {
                Log.Debug("ApiService", $"Error fetching users: {ex.Message}");
            }
            return new List<User>();
        }

        public async void CreateUserAsync(string email, string user_id)
        {
            try
            {
                string url = "https://sportime-backend.onrender.com/users";
                HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(new { email, user_id }),
                    Encoding.UTF8,
                    "application/json"
                ));

            }
            catch (Exception ex)
            {
                Log.Debug("ApiService", $"Error creating user: {ex.Message}");
            }
        }

        public async void DeleteUserAsync(string userId)
        {
            try
            {
                string url = $"https://sportime-backend.onrender.com/users/{userId}";
                HttpResponseMessage response = await _httpClient.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Toast.MakeText(Application.Context, "User deleted successfully", ToastLength.Short).Show();
                }
                else
                {
                    Log.Debug("ApiService", $"Failed to delete user with ID: {userId}. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Log.Debug("ApiService", $"Error deleting user: {ex.Message}");
            }
        }


        public async Task<List<Reservation>> GetReservationsAsync(int fieldId)
        {
            try
            {
                string url = $"https://sportime-backend.onrender.com/fields/{fieldId}/reservations";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Reservation>>(jsonResult);
                }
            }
            catch (Exception ex)
            {
                Log.Debug("ApiService", $"Error fetching reservations: {ex.Message}");
            }
            return new List<Reservation>();
        }


        public async Task<(bool Success, string ErrorMessage)> CreateReservationAsync(string userId, int fieldId, DateTime startingTime)
        {
            try
            {
                string url = "https://sportime-backend.onrender.com/reservations";

                var body = new
                {
                    user_id = userId,
                    field_id = fieldId,
                    starting_time = startingTime.ToString("yyyy-MM-ddTHH:mm:ss")
                };

                HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(
                    JsonConvert.SerializeObject(body),
                    Encoding.UTF8,
                    "application/json"
                ));

                if (response.IsSuccessStatusCode)
                    return (true, null);

                // Read the error detail from the backend
                string errorJson = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(errorJson);
                string detail = errorObj?["detail"]?.ToString() ?? "Failed to create reservation";

                return (false, detail);
            }
            catch (Exception ex)
            {
                Log.Debug("ApiService", $"Error creating reservation: {ex.Message}");
                return (false, "An unexpected error occurred");
            }
        }

        public async Task<bool> DeleteReservationAsync(int reservationId)
        {
            try
            {
                string url = $"https://sportime-backend.onrender.com/reservations/{reservationId}";
                HttpResponseMessage response = await _httpClient.DeleteAsync(url); // DELETE, not GET

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Log.Debug("ApiService", $"Error deleting reservation: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Reservation>> GetUserReservationsAsync(string userId)
        {
            try
            {
                string url = $"https://sportime-backend.onrender.com/users/{userId}/reservations";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Reservation>>(jsonResult);
                }
            }
            catch (Exception ex)
            {
                Log.Debug("ApiService", $"Error fetching user reservations: {ex.Message}");
            }
            return new List<Reservation>();
        }
    }
}