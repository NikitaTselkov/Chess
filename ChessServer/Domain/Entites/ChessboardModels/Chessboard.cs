using ChessServer.Domain.Entites.Abstract;
using ChessServer.Domain.Entites.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites.ChessboardModels
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
            InitBishops();
            InitKnights();
            InitKings();
            InitQueens();
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
            ChessPieces.Add(new Rook(Colors.Black, Cells["A8"]));
            ChessPieces.Add(new Rook(Colors.Black, Cells["H8"]));
        }

        /// <summary>
        /// Создание слонов.
        /// </summary>
        private void InitBishops()
        {
            // Белые слоны.
            ChessPieces.Add(new Bishop(Colors.White, Cells["C1"]));
            ChessPieces.Add(new Bishop(Colors.White, Cells["F1"]));

            // Черные слоны.
            ChessPieces.Add(new Bishop(Colors.Black, Cells["C8"]));
            ChessPieces.Add(new Bishop(Colors.Black, Cells["F8"]));
        }
        
        /// <summary>
        /// Создание коней.
        /// </summary>
        private void InitKnights()
        {
            // Белые кони.
            ChessPieces.Add(new Knight(Colors.White, Cells["B1"]));
            ChessPieces.Add(new Knight(Colors.White, Cells["G1"]));

            // Черные кони.
            ChessPieces.Add(new Knight(Colors.Black, Cells["B8"]));
            ChessPieces.Add(new Knight(Colors.Black, Cells["G8"]));
        }

        /// <summary>
        /// Создание королей.
        /// </summary>
        private void InitKings()
        {
            // Белый король.
            ChessPieces.Add(new King(Colors.White, Cells["E1"]));

            // Черный король.
            ChessPieces.Add(new King(Colors.Black, Cells["E8"]));
        }
        
        /// <summary>
        /// Создание ферзей.
        /// </summary>
        private void InitQueens()
        {
            // Белый ферзь.
            ChessPieces.Add(new Queen(Colors.White, Cells["D1"]));

            // Черный ферзь.
            ChessPieces.Add(new Queen(Colors.Black, Cells["D8"]));
        }

        public IEnumerable<AbstractChessPiece> this[PieceNames name, Colors color]
        {
            get
            {
                return new List<AbstractChessPiece>(ChessPieces.Where(f => f.Name == name && f.Color == color));
            }
        }
    }
}
