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
    /// Слон.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class Bishop : AbstractChessPiece
    {
        private const PieceNames _name = PieceNames.Bishop;

        public Bishop(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition) { }


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

                bool ne = false;
                bool se = false;
                bool sw = false;
                bool nw = false;

                for (int i = 1; i < 8; i++)
                {
                    // Если слон может ходить по верхней правой диагонали.
                    if (column + i <= 8 && row + i <= 8)
                    {
                        SetValueToPositions(positions, new Cell(column + i, row + i), ref ne);
                    }
                    // Если слон может ходить по нижней левой диагонали.
                    if (column - i >= 1 && row - i >= 1)
                    {
                        SetValueToPositions(positions, new Cell(column - i, row - i), ref sw);
                    }
                    // Если слон может ходить по верхней левой диагонали.
                    if (column - i >= 1 && row + i <= 8)
                    {
                        SetValueToPositions(positions, new Cell(column - i, row + i), ref nw);
                    }
                    // Если слон может ходить по нижней правой диагонали.
                    if (column + i <= 8 && row - i >= 1)
                    {
                        SetValueToPositions(positions, new Cell(column + i, row - i), ref se);
                    }
                }
            });
        }
    }
}
