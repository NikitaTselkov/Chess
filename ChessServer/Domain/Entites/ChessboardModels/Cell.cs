using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites.ChessboardModels
{
    [DebuggerDisplay("{Title}")]
    public struct Cell
    {
        /// <summary>
        /// Номер клетки.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Колонка.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Ряд.
        /// </summary>
        public int Row { get; private set; }

        public Cell(int _column, int _row)
        {
            if (_column > 9 || _column < 1)
                throw new ArgumentOutOfRangeException("Колонок не может быть больше 9 или меньше 1");

            Title = _column switch
            {
                1 => "A",
                2 => "B",
                3 => "C",
                4 => "D",
                5 => "E",
                6 => "F",
                7 => "G",
                8 => "H",
                _ => throw new NotImplementedException(nameof(_column))
            };

            Title += (_row).ToString();
            Column = _column;
            Row = _row;
        }

        public static bool operator ==(Cell a, Cell b)
        {
            return a.Title == b.Title;
        }

        public static bool operator !=(Cell a, Cell b)
        {
            return a.Title != b.Title;
        }
    }
}
