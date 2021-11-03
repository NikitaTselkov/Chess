using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Desktop.Models
{
    public struct Cell
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
    }
}
