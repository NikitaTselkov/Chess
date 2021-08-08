using ChessServer.Domain.Entites.Abstract;
using ChessServer.Domain.Entites.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites
{
    public sealed class Chessboard
    {
        /// <summary>
        /// Клетки доски.
        /// </summary>
        public Cells Cells { get; private set; }

        /// <summary>
        /// Фигуры.
        /// </summary>
        public List<AbstractChessPiece> ChessPieces { get; private set; }


        public Chessboard()
        {
            Cells = new Cells(8, 8);
            ChessPieces = new List<AbstractChessPiece>();

            InitChessboard();
            InitPawns();
            InitRooks();
        }

        /// <summary>
        /// Создание доски.
        /// </summary>
        private void InitChessboard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cells[i, j] = new Cell(j + 1, i + 1);
                }
            }
        }

        /// <summary>
        /// Создание пешек.
        /// </summary>
        private void InitPawns()
        {
            // Белые пешки.
            ChessPieces.Add(new Pawn(Colors.White, Cells["A2"]));
            ChessPieces.Add(new Pawn(Colors.White, Cells["B2"]));
            ChessPieces.Add(new Pawn(Colors.White, Cells["C2"]));
            ChessPieces.Add(new Pawn(Colors.White, Cells["D2"]));
            ChessPieces.Add(new Pawn(Colors.White, Cells["E2"]));
            ChessPieces.Add(new Pawn(Colors.White, Cells["F2"]));
            ChessPieces.Add(new Pawn(Colors.White, Cells["G2"]));
            ChessPieces.Add(new Pawn(Colors.White, Cells["H2"]));

            // Черные пешки.
            ChessPieces.Add(new Pawn(Colors.Black, Cells["A7"]));
            ChessPieces.Add(new Pawn(Colors.Black, Cells["B7"]));
            ChessPieces.Add(new Pawn(Colors.Black, Cells["C7"]));
            ChessPieces.Add(new Pawn(Colors.Black, Cells["D7"]));
            ChessPieces.Add(new Pawn(Colors.Black, Cells["E7"]));
            ChessPieces.Add(new Pawn(Colors.Black, Cells["F7"]));
            ChessPieces.Add(new Pawn(Colors.Black, Cells["G7"]));
            ChessPieces.Add(new Pawn(Colors.Black, Cells["H7"]));
        }

        /// <summary>
        /// Создание ладей.
        /// </summary>
        private void InitRooks()
        {
            // Белые ладьи.
            ChessPieces.Add(new Rook(Colors.White, Cells["A1"]));
            ChessPieces.Add(new Rook(Colors.White, Cells["H1"]));

            // Черные ладьи.
            ChessPieces.Add(new Rook(Colors.White, Cells["A8"]));
            ChessPieces.Add(new Rook(Colors.White, Cells["H8"]));
        }
    }
}
