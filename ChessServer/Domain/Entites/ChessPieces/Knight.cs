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
        private const PieceNames _name = PieceNames.Knight;

        public Knight(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition) { }


        /// <summary>
        /// Получает все позиции.
        /// </summary>
        /// <param name="positions"> Занятые позиции и цвета фигур. </param>
        public override async Task GetAsyncPositions(List<(Cell, Colors)> positions)
        {
            await Task.Run(() =>
            {
                base.AllPositions = new List<Cell>();
                base.PossiblePositions = new List<Cell>();

                int column = CurrentPosition.Column;
                int row = CurrentPosition.Row;

                bool n1 = false;
                bool n2 = false;
                bool e1 = false;
                bool e2 = false;
                bool s1 = false;
                bool s2 = false;
                bool w1 = false;
                bool w2 = false;

                // Перемещение вправо.
                if (column + 2 <= 8)
                {
                    if (row + 1 <= 8)
                        SetValueToPositions(positions, new Cell(column + 2, row + 1), ref e1);
                    if (row - 1 >= 1)
                        SetValueToPositions(positions, new Cell(column + 2, row - 1), ref e2);
                }

                // Перемещение влево.
                if (column - 2 >= 1)
                {
                    if (row + 1 <= 8)
                        SetValueToPositions(positions, new Cell(column - 2, row + 1), ref w1);
                    if (row - 1 >= 1)
                        SetValueToPositions(positions, new Cell(column - 2, row - 1), ref w2);
                }

                // Перемещение вверх.
                if (row + 2 <= 8)
                {
                    if (column - 1 >= 1)
                        SetValueToPositions(positions, new Cell(column - 1, row + 2), ref n1);
                    if (column + 1 <= 8)
                        SetValueToPositions(positions, new Cell(column + 1, row + 2), ref n2);
                }

                // Перемещение вниз.
                if (row - 2 >= 1)
                {
                    if (column - 1 >= 1)
                        SetValueToPositions(positions, new Cell(column - 1, row - 2), ref s1);
                    if (column + 1 <= 8)
                        SetValueToPositions(positions, new Cell(column + 1, row - 2), ref s2);
                }
            });
        }
    }
}
