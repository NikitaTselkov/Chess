using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites.ChessboardModels
{
    [DebuggerDisplay("{Value}")]
    public sealed class Cells
    {
        public Cell[,] Value { get; set; }


        public Cells(int _column, int _row)
        {
            Value = new Cell[_column, _row];
        }

        public Cell this[string title]
        {
            get
            {
                foreach (var cell in Value)
                {
                    if (cell.Title == title)
                    {
                        return cell;
                    }
                }

                throw new ArgumentNullException("Такого элемента не существует.");
            }
        }

        public Cell this[int i, int j]
        {
            get { return Value[i, j]; }
            set { Value[i, j] = value; }
        }
    }
}
