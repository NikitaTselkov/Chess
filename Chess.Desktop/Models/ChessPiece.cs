using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Desktop.Models
{
    public sealed class ChessPiece
    {
        /// <summary>
        /// Фигура.
        /// </summary>
        [JsonProperty("Name")]
        public int NameCode { get; set; }

        /// <summary>
        /// Цвет фигуры.
        /// </summary>
        [JsonProperty("Color")]
        public Colors Color { get; set; }

        /// <summary>
        /// Текущая позиция.
        /// </summary>
        [JsonProperty("CurrentPosition")]
        public Cell CurrentPosition { get; set; }

        /// <summary>
        /// Возможные позиции.
        /// </summary>
        [JsonProperty("PossiblePositions")]
        public List<Cell> PossiblePositions { get; set; }

        /// <summary>
        /// Все позиции.
        /// </summary>
        [JsonProperty("AllPositions")]
        public List<Cell> AllPositions { get; set; }
    }
}
