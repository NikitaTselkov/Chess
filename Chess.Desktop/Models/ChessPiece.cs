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
        public string Name { get; set; }

        /// <summary>
        /// Цвет фигуры.
        /// </summary>
        public Colors Color { get; set; }

        /// <summary>
        /// Текущая позиция.
        /// </summary>
        public Cell CurrentPosition { get; set; }

        /// <summary>
        /// Возможные позиции.
        /// </summary>
        public List<Cell> PossiblePositions { get; set; }

        /// <summary>
        /// Все позиции.
        /// </summary>
        public List<Cell> AllPositions { get; set; }
    }
}
