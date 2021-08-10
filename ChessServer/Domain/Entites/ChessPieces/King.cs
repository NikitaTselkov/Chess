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
    /// Король.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class King : AbstractChessPiece
    {
        private const string _name = "Король";

        public King(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition)
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

                // Если король вправом верхнем углу.
                if (column + 1 > 8 && row + 1 > 8)
                {
                    // Перемещение влево.
                    allPositions.Add(new Cell(column - 1, row));
                    // Перемещение вниз.
                    allPositions.Add(new Cell(column, row - 1));
                    // Перемещение влево вниз.
                    allPositions.Add(new Cell(column - 1, row - 1));
                }
                // Если король влевом верхнем углу.
                else if (column - 1 < 1 && row + 1 > 8)
                {
                    // Перемещение вниз.
                    allPositions.Add(new Cell(column, row - 1));
                    // Перемещение вправо.
                    allPositions.Add(new Cell(column + 1, row));
                    // Перемещение вправо вниз.
                    allPositions.Add(new Cell(column + 1, row - 1));
                }
                // Если король влевом нижнем углу.
                else if (column - 1 < 1 && row - 1 < 1)
                {
                    // Перемещение вверх.
                    allPositions.Add(new Cell(column, row + 1));
                    // Перемещение вправо.                  
                    allPositions.Add(new Cell(column + 1, row));
                    // Перемещение вправо вверх.    
                    allPositions.Add(new Cell(column + 1, row + 1));
                }
                // Если король вправом нижнем углу.
                else if (column + 1 > 8 && row - 1 < 1)
                {
                    // Перемещение вверх.
                    allPositions.Add(new Cell(column, row + 1));
                    // Перемещение влево.
                    allPositions.Add(new Cell(column - 1, row));
                    // Перемещение влево вверх.
                    allPositions.Add(new Cell(column - 1, row + 1));
                }
                // Если король у правой стороны доски.
                else if (column + 1 > 8)
                {
                    // Перемещение влево.
                    allPositions.Add(new Cell(column - 1, row + 1));
                    allPositions.Add(new Cell(column - 1, row));
                    allPositions.Add(new Cell(column - 1, row - 1));
                    // Перемещение вверх.
                    allPositions.Add(new Cell(column, row + 1));
                    // Перемещение вниз.
                    allPositions.Add(new Cell(column, row - 1));
                }
                // Если король у левой стороны доски.
                else if (column - 1 < 1)
                {
                    // Перемещение вправо.
                    allPositions.Add(new Cell(column + 1, row + 1));
                    allPositions.Add(new Cell(column + 1, row));
                    allPositions.Add(new Cell(column + 1, row - 1));
                    // Перемещение вверх.
                    allPositions.Add(new Cell(column, row + 1));
                    // Перемещение вниз.
                    allPositions.Add(new Cell(column, row - 1));
                }
                // Если король у верхней стороны доски.
                else if (row + 1 > 8)
                {
                    // Перемещение вниз.
                    allPositions.Add(new Cell(column + 1, row - 1));
                    allPositions.Add(new Cell(column, row - 1));
                    allPositions.Add(new Cell(column - 1, row - 1));
                    // Перемещение вправо.
                    allPositions.Add(new Cell(column + 1, row));
                    // Перемещение влево.
                    allPositions.Add(new Cell(column - 1, row));
                }
                // Если король у нижней стороны доски.
                else if (row - 1 < 1)
                {
                    // Перемещение вверх.
                    allPositions.Add(new Cell(column + 1, row + 1));
                    allPositions.Add(new Cell(column, row + 1));
                    allPositions.Add(new Cell(column - 1, row + 1));
                    // Перемещение вправо.
                    allPositions.Add(new Cell(column + 1, row));
                    // Перемещение влево.
                    allPositions.Add(new Cell(column - 1, row));
                }
                else
                {
                    // Перемещение вверх.
                    allPositions.Add(new Cell(column, row + 1));

                    // Перемещение вниз.
                    allPositions.Add(new Cell(column, row - 1));

                    // Перемещение влево.                   
                    allPositions.Add(new Cell(column - 1, row));
                    
                    // Перемещение вправо.
                    allPositions.Add(new Cell(column + 1, row));
                    
                    // Перемещения по диагоналям.
                    allPositions.Add(new Cell(column + 1, row + 1));
                    allPositions.Add(new Cell(column - 1, row + 1));
                    allPositions.Add(new Cell(column + 1, row - 1));
                    allPositions.Add(new Cell(column - 1, row - 1));
                }
            });

            return allPositions;
        }
    }
}
