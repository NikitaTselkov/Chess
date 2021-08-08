using ChessServer.Domain.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites.ChessPieces
{
    /// <summary>
    /// Пешка.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class Pawn : AbstractChessPiece
    {
        private const string _name = "Пешка";

        public Pawn(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition)
        {
            base.AllPositions.AddRange(GetAllPositions());        }

        /// <summary>
        /// Получает все позиции.
        /// </summary>
        /// <returns> Все позиции. </returns>
        public override List<Cell> GetAllPositions()
        {
            List<Cell> allPositions = new List<Cell>();

            int column = CurrentPosition.Column;
            int row = CurrentPosition.Row;

            if (Color == Colors.White)
            {
                // Если пешка дошла до конца поля.
                if (row == 8)
                {
                    return default;
                }
                // Если пешка находится на левом краю доски.
                if (column == 1)
                {
                    allPositions.Add(new Cell(column + 1, row + 1));
                }
                // Если пешка находится на правом краю доски.
                else if (column == 8)
                {
                    allPositions.Add(new Cell(column - 1, row + 1));
                }
                // Если пешка не возле края доски.
                else
                {
                    allPositions.Add(new Cell(column + 1, row + 1));
                    allPositions.Add(new Cell(column - 1, row + 1));
                }
                // Если пешка на стартовой позиции.
                if (row == 2)
                {
                    allPositions.Add(new Cell(column, row + 2));
                }

                allPositions.Add(new Cell(column, row + 1));
            }
            else if (Color == Colors.Black)
            {
                // Если пешка дошла до конца поля.
                if (row == 1)
                {
                    return default;
                }
                // Если пешка находится на левом краю доски.
                if (column == 8)
                {
                    allPositions.Add(new Cell(column - 1, row - 1));
                }
                // Если пешка находится на правом краю доски.
                else if (column == 1)
                {
                    allPositions.Add(new Cell(column + 1, row - 1));
                }
                // Если пешка не возле края доски.
                else
                {
                    allPositions.Add(new Cell(column - 1, row - 1));
                    allPositions.Add(new Cell(column + 1, row - 1));
                }
                // Если пешка на стартовой позиции.
                if (row == 7)
                {
                    allPositions.Add(new Cell(column, row - 2));
                }

                allPositions.Add(new Cell(column, row - 1));
            }

            return allPositions;
        }
    }
}
