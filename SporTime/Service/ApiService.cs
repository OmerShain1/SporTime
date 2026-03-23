using Android.App;
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
using System.Threading.Tasks;
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
                string url = "http://192.168.1.56:8000/fields";

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

       
    }



}