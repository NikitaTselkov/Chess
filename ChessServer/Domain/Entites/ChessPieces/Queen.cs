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

        public Queen(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition) { }


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

                for (int i = 1; i < 8; i++)
                {
                    // Если ферзь может ходить по верхней правой диагонали.
                    if (column + i <= 8 && row + i <= 8)
                    {
                        SetValueToPositions(positions, new Cell(column + i, row + i), ref ne);
                    }
                    // Если ферзь может ходить по нижней левой диагонали.
                    if (column - i >= 1 && row - i >= 1)
                    {
                        SetValueToPositions(positions, new Cell(column - i, row - i), ref sw);
                    }
                    // Если ферзь может ходить по верхней левой диагонали.
                    if (column - i >= 1 && row + i <= 8)
                    {
                        SetValueToPositions(positions, new Cell(column - i, row + i), ref nw);
                    }
                    // Если ферзь может ходить по нижней правой диагонали.
                    if (column + i <= 8 && row - i >= 1)
                    {
                        SetValueToPositions(positions, new Cell(column + i, row - i), ref se);
                    }
                    // Если ферзь может ходить вверх.
                    if (row + i <= 8)
                    {
                        SetValueToPositions(positions, new Cell(column, row + i), ref n);
                    }
                    // Если ферзь может ходить вниз.
                    if (row - i >= 1)
                    {
                        SetValueToPositions(positions, new Cell(column, row - i), ref s);
                    }
                    // Если ферзь может ходить влево.
                    if (column - i >= 1)
                    {
                        SetValueToPositions(positions, new Cell(column - i, row), ref w);
                    }
                    // Если ферзь может ходить вправо.
                    if (column + i <= 8)
                    {
                        SetValueToPositions(positions, new Cell(column + i, row), ref e);
                    }
                }
            });
        }
    }
}
