using ChessServer.Domain.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites.ChessPieces
{
    /// <summary>
    /// Слон.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class Bishop : AbstractChessPiece
    {
        private const string _name = "Слон";

        public Bishop(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition)
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

                for (int i = 1; i < 8; i++)
                {
                    // Если слон может ходить по верхней правой диагонали.
                    if (column + i <= 8 && row + i <= 8)
                    {
                        allPositions.Add(new Cell(column + i, row + i));
                    }
                    // Если слон может ходить по нижней левой диагонали.
                    if (column - i >= 1 && row - i >= 1)
                    {
                        allPositions.Add(new Cell(column - i, row - i));
                    }
                    // Если слон может ходить по верхней левой диагонали.
                    if (column - i >= 1 && row + i <= 8)
                    {
                        allPositions.Add(new Cell(column - i, row + i));
                    }
                    // Если слон может ходить по нижней правой диагонали.
                    if (column + i <= 8 && row - i >= 1)
                    {
                        allPositions.Add(new Cell(column + i, row - i));
                    }
                }
            });

            return allPositions;
        }
    }
}
