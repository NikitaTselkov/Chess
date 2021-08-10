using ChessServer.Domain.Entites.Abstract;
using ChessServer.Domain.Entites.ChessboardModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites.ChessPieces
{
    /// <summary>
    /// Конь.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class Knight : AbstractChessPiece
    {
        private const string _name = "Конь";

        public Knight(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition)
        {
            base.AllPositions.AddRange(GetAsyncAllPositions().Result);
        }

        /// <summary>
        /// Получает все позиции.
        /// </summary>
        /// <returns> Все позиции. </returns>
        public override async Task<List<Cell>> GetAsyncAllPositions()
        {
            List<Cell> allPositions = new List<Cell>();

            await Task.Run(() =>
            {
                int column = CurrentPosition.Column;
                int row = CurrentPosition.Row;

                // Перемещение вправо.
                if (column + 2 <= 8)
                {
                    if (row + 1 <= 8)
                        allPositions.Add(new Cell(column + 2, row + 1));
                    if (row - 1 >= 1)
                        allPositions.Add(new Cell(column + 2, row - 1));
                }

                // Перемещение влево.
                if (column - 2 >= 1)
                {
                    if (row + 1 <= 8)
                        allPositions.Add(new Cell(column - 2, row + 1));
                    if (row - 1 >= 1)
                        allPositions.Add(new Cell(column - 2, row - 1));
                }

                // Перемещение вверх.
                if (row + 2 <= 8)
                {
                    if (column - 1 >= 1)
                        allPositions.Add(new Cell(column - 1, row + 2));
                    if (column + 1 <= 8)
                        allPositions.Add(new Cell(column + 1, row + 2));
                }

                // Перемещение вниз.
                if (row - 2 >= 1)
                {
                    if (column - 1 >= 1)
                        allPositions.Add(new Cell(column - 1, row - 2));
                    if (column + 1 <= 8)
                        allPositions.Add(new Cell(column + 1, row - 2));
                }
            });

            return allPositions;
        }
    }
}
