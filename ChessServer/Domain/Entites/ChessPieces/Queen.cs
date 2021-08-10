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
    /// Ферзь.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class Queen : AbstractChessPiece
    {
        private const string _name = "Ферзь";

        public Queen(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition)
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

                for (int i = 1; i <= 8; i++)
                {
                    // Если ферзь может ходить по верхней правой диагонали.
                    if (column + i <= 8 && row + i <= 8)
                    {
                        allPositions.Add(new Cell(column + i, row + i));
                    }
                    // Если ферзь может ходить по нижней левой диагонали.
                    if (column - i >= 1 && row - i >= 1)
                    {
                        allPositions.Add(new Cell(column - i, row - i));
                    }
                    // Если ферзь может ходить по верхней левой диагонали.
                    if (column - i >= 1 && row + i <= 8)
                    {
                        allPositions.Add(new Cell(column - i, row + i));
                    }
                    // Если ферзь может ходить по нижней правой диагонали.
                    if (column + i <= 8 && row - i >= 1)
                    {
                        allPositions.Add(new Cell(column + i, row - i));
                    }
                    // Если ферзь может ходить по вертикали.
                    if (row != i)
                    {
                        allPositions.Add(new Cell(column, i));
                    }
                    // Если ферзь может ходить по горизонтали.
                    if (column != i)
                    {
                        allPositions.Add(new Cell(i, row));
                    }
                }
            });

            return allPositions;
        }
    }
}
