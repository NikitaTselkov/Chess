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
    /// Пешка.
    /// </summary>
    [DebuggerDisplay("{_name}, {base.Color}")]
    public sealed class Pawn : AbstractChessPiece
    {
        private const PieceNames _name = PieceNames.Pawn;

        public Pawn(Colors _color, Cell _currentPosition) : base(_name, _color, _currentPosition) { }


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

                if (Color == Colors.White)
                {
                    // Если пешка не дошла до конца поля.
                    if (row != 8)
                    {
                        // Если пешка находится на левом краю доски.
                        if (column == 1)
                        {
                            SetValueToPositions(positions, new Cell(column + 1, row + 1));
                        }
                        // Если пешка находится на правом краю доски.
                        else if (column == 8)
                        {
                            SetValueToPositions(positions, new Cell(column - 1, row + 1));
                        }
                        // Если пешка не возле края доски.
                        else
                        {
                            SetValueToPositions(positions, new Cell(column + 1, row + 1));
                            SetValueToPositions(positions, new Cell(column - 1, row + 1));
                        }
                        // Если пешка на стартовой позиции.
                        if (row == 2)
                        {
                            SetValueToPositions(positions, new Cell(column, row + 2), true);
                        }

                        SetValueToPositions(positions, new Cell(column, row + 1), true);
                    }
                }
                else if (Color == Colors.Black)
                {
                    // Если пешка дошла не до конца поля.
                    if (row != 1)
                    {
                        // Если пешка находится на левом краю доски.
                        if (column == 8)
                        {
                            SetValueToPositions(positions, new Cell(column - 1, row - 1));
                        }
                        // Если пешка находится на правом краю доски.
                        else if (column == 1)
                        {
                            SetValueToPositions(positions, new Cell(column + 1, row - 1));
                        }
                        // Если пешка не возле края доски.
                        else
                        {
                            SetValueToPositions(positions, new Cell(column - 1, row - 1));
                            SetValueToPositions(positions, new Cell(column + 1, row - 1));
                        }
                        // Если пешка на стартовой позиции.
                        if (row == 7)
                        {
                            SetValueToPositions(positions, new Cell(column, row - 2), true);
                        }

                        SetValueToPositions(positions, new Cell(column, row - 1), true);
                    }
                }
            });
        }

        /// <summary>
        /// Устанавливает значения спискам позиций.
        /// </summary>
        /// <param name="positions"> Позиция фигуры и её цвет. </param>
        /// <param name="cell"> Устанавливаемое значение. </param>
        /// <param name="isContains"> Если поле впереди по диагонали занято. </param>
        public void SetValueToPositions(List<(Cell, Colors)> positions, Cell cell, bool isContains = false)
        {
            Cell temp = cell;
            AllPositions.Add(temp);

            if (isContains)
            {
                if (!positions.Any(a => a.Item1 == temp))
                    PossiblePositions.Add(temp);
            }
            else
            {
                if (positions.Any(a => a.Item1 == temp && a.Item2 != Color))
                    PossiblePositions.Add(temp);
            }
        }
    }
}
