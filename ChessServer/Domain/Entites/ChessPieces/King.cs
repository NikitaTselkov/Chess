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
        private const PieceNames _name = PieceNames.King;
        public bool IsMoved { get; set; }
        public King(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition) { }


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
                bool ne = false;
                bool e = false;
                bool se = false;
                bool s = false;
                bool sw = false;
                bool w = false;
                bool nw = false;

                // Если король может ходить по верхней правой диагонали.
                if (column + 1 <= 8 && row + 1 <= 8)
                {
                    SetValueToPositions(positions, new Cell(column + 1, row + 1), ref ne);
                }
                // Если король может ходить по нижней левой диагонали.
                if (column - 1 >= 1 && row - 1 >= 1)
                {
                    SetValueToPositions(positions, new Cell(column - 1, row - 1), ref sw);
                }
                // Если король может ходить по верхней левой диагонали.
                if (column - 1 >= 1 && row + 1 <= 8)
                {
                    SetValueToPositions(positions, new Cell(column - 1, row + 1), ref nw);
                }
                // Если король может ходить по нижней правой диагонали.
                if (column + 1 <= 8 && row - 1 >= 1)
                {
                    SetValueToPositions(positions, new Cell(column + 1, row - 1), ref se);
                }
                // Если король может ходить вверх.
                if (row + 1 <= 8)
                {
                    SetValueToPositions(positions, new Cell(column, row + 1), ref n);
                }
                // Если король может ходить вниз.
                if (row - 1 >= 1)
                {
                    SetValueToPositions(positions, new Cell(column, row - 1), ref s);
                }
                // Если король может ходить влево.
                if (column - 1 >= 1)
                {
                    SetValueToPositions(positions, new Cell(column - 1, row), ref w);
                }
                // Если король может ходить вправо.
                if (column + 1 <= 8)
                {
                    SetValueToPositions(positions, new Cell(column + 1, row), ref e);
                }
            });
        }
    }
}
