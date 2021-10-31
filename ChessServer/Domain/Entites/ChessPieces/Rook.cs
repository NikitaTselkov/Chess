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
    /// Ладья.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class Rook : AbstractChessPiece
    {
        private const PieceNames _name = PieceNames.Rook;

        public Rook(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition) { }


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

                bool n = false;
                bool e = false;
                bool s = false;
                bool w = false;

                for (int i = 1; i < 8; i++)
                {
                    // Если ладья может ходить вверх.
                    if (row + i <= 8)
                    {
                        SetValueToPositions(positions, new Cell(column, row + i), ref n);
                    }
                    // Если ладья может ходить вниз.
                    if (row - i >= 1)
                    {
                        SetValueToPositions(positions, new Cell(column, row - i), ref s);
                    }
                    // Если ладья может ходить влево.
                    if (column - i >= 1)
                    {
                        SetValueToPositions(positions, new Cell(column - i, row), ref w);
                    }
                    // Если ладья может ходить вправо.
                    if (column + i <= 8)
                    {
                        SetValueToPositions(positions, new Cell(column + i, row), ref e);
                    }
                }
            });
        }
    }
}
