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
        public static async Task<string> GetDataFromServer(string request = "")
        {
            var data = "";
            using (HttpClient client = new HttpClient())
            {
                var respones = client.GetAsync($"https://localhost:44352/api/Values/{request}").Result;

                if (respones.IsSuccessStatusCode)
                {
                    data = await respones.Content.ReadAsStringAsync();
                }
            }

            return data;
        }
    }
}
