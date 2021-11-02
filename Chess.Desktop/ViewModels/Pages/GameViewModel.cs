using Chess.Desktop.Models;
using Chess.Desktop.ViewModels.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Desktop.ViewModels.Pages
{
    public class GameViewModel : NavigateViewModel
    {
        private string _data;
        public string Data
        {
            get => _data;
            set
            {
                _data = value;
                RaisePropertyChanged(nameof(Data));
            }
        }

       // public RelayCommand GetDataFromServer { get; set; }
        public GameViewModel()
        {
            GetDataFromServer("Chessboard");
            //GetDataFromServer();
        }

        public async void GetDataFromServer(string request = "")
        {
            using (HttpClient client = new HttpClient())
            {
                var respones = await client.GetAsync($"https://localhost:44352/api/Values/{request}");

                try
                {
                    respones.EnsureSuccessStatusCode();

                    Data = await respones.Content.ReadAsStringAsync();
                }
                catch (Exception)
                {
                    Data = $"Server error code: {respones.StatusCode}";
                }
            }
        }
    }
}
