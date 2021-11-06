using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Desktop.Models
{
    public static class Server
    {
        private static string Url { get; set; } = "https://localhost:44352/api/Game/";

        public static IEnumerable<ChessPiece> GetChessPiecesPositions()
        {
            var data = GetDataAsync("Positions");
            return JsonConvert.DeserializeObject<IEnumerable<ChessPiece>>(data.Result);
        }

        public static Cell[] GetChessboard()
        {
            var data = GetDataAsync("Chessboard");
            return JsonConvert.DeserializeObject<Cell[]>(data.Result);
        }

        public static void SendMove(string oldPositionTitle, string newPositionTitle)
        {
            HttpStatusCode statusCode;
            do
            {
                statusCode = SetDataAsync($"SendMove/{oldPositionTitle}/{newPositionTitle}").Result;
            }
            while (statusCode != HttpStatusCode.OK);
        }

        private static async Task<string> GetDataAsync(string request = "")
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

        private static async Task<HttpStatusCode> SetDataAsync(string request)
        {
            HttpStatusCode statusCode;
            using (HttpClient client = new HttpClient())
            {
                var respones = await client.PutAsync(Url + request, null).ConfigureAwait(false);
                statusCode = respones.StatusCode;
            }

            return statusCode;
        }
    }
}
