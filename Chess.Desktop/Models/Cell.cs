using Chess.Desktop.ViewModels.Navigation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Desktop.Models
{
    public class Cell : NavigateViewModel
    {
        /// <summary>
        /// Номер клетки.
        /// </summary>
        [JsonProperty("Title")]
        public string Title { get; private set; }

        /// <summary>
        /// Колонка.
        /// </summary>
        [JsonProperty("Column")]
        public int Column { get; private set; }

        /// <summary>
        /// Ряд.
        /// </summary>
        [JsonProperty("Row")]
        public int Row { get; private set; }

        private State _state;
        public State State
        {
            get => _state;
            set
            {
                _state = value;
                RaisePropertyChanged(nameof(State));
            }
        }

        private bool _isActive;
        public bool IsActive 
        {
            get => _isActive;
            set
            {
                _isActive = value;
                RaisePropertyChanged(nameof(IsActive));
            }
        }

        public Cell(string title)
        {
            Title = title;
        }
    }
}
