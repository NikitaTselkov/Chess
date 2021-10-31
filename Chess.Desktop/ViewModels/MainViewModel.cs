using Chess.Desktop.Models;
using Chess.Desktop.ViewModels.Navigation;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Desktop.ViewModels
{
    public class MainViewModel : NavigateViewModel
    {
        // Информация с сервера.
        private string _data;
        public string Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand GetDataFromServer { get; set; }
        public MainViewModel()
        {
            GetDataFromServer = new RelayCommand(GetDataFromServerMethod);
        }

        public async void GetDataFromServerMethod(object param)
        {
            using (HttpClient client = new HttpClient())
            {
                var respones = await client.GetAsync("https://localhost:44352/api/Values");
                respones.EnsureSuccessStatusCode();
                if (respones.IsSuccessStatusCode)
                {
                    Data = await respones.Content.ReadAsStringAsync();
                }
                else
                {
                    Data = $"Server error code: {respones.StatusCode}";
                }
            }
        }
    }
}
