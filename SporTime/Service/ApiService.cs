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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
                string url = "http://192.168.1.57:8000/fields";

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


        public async void CreateUserAsync(string email, string user_id)
        {
            try
            {
                string url = "http://192.168.1.578000/users";
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



        public async Task<List<Reservation>> GetReservationsAsync(int fieldId)
        {
            try
            {
                string url = $"http://192.168.1.57:8000/fields/{fieldId}/reservations";
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
    }
}