using ChessServer.Domain.Entites.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites.ChessPieces
{
    /// <summary>
    /// Ладья.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class Rook : AbstractChessPiece
    {
        private const string _name = "Ладья";

        public Rook(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition)
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
                    if (row != i)
                    {
                        allPositions.Add(new Cell(column, i));
                    }
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
