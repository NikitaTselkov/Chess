using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Desktop.Models
{
    public static class Server
    {
        private static string Url { get; set; } = "https://localhost:44352/api/Game/";

        public static async Task<string> GetDataFromServerAsync(string request = "")
        {
            var data = "";
            using (HttpClient client = new HttpClient())
            {
                var respones = client.GetAsync($"{Url}{request}").Result;

                if (respones.IsSuccessStatusCode)
                {
                    data = await respones.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }

            return data;
        }

        public static async void SetDataToServerAsync(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.PutAsync(Url + request, null).ConfigureAwait(false);
            }
        }
    }
}
