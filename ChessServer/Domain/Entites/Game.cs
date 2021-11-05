using ChessServer.Domain.Entites.Abstract;
using ChessServer.Domain.Entites.ChessboardModels;
using ChessServer.Domain.Entites.ChessPieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessServer.Domain.Entites
{
    public static class Game
    {
        public static Chessboard Chessboard { get; private set; }
        public static AbstractChessPiece CheckPiece
        {
            get 
            {
                var whiteKing = Chessboard[PieceNames.King, Colors.White].First();
                return Chessboard.ChessPieces.FirstOrDefault(f => f.PossiblePositions.Contains(whiteKing.CurrentPosition));
            }
        }
        public static int ImpossibleMoves { get; private set; }

        private static List<(Cell, Colors)> _positionsList;

        static Game()
        {
            Chessboard = new Chessboard();

            UpdatePositions();
            SubscribingToEvents();
        }

        /// <summary>
        /// Передвижение с проверкой соблюдения правил.
        /// </summary>
        public static void MoveWithCheckGameRules(AbstractChessPiece chessPiece, Cell newPosition)
        {
            var lastPosition = chessPiece.CurrentPosition;
            chessPiece.Move(newPosition);

            if (CheckPiece != null)
            {
                ImpossibleMoves++;
                chessPiece.MoveBackwards(lastPosition);
                UpdatePositions();
            }
            else if (chessPiece is King king)
            {
                king.IsMoved = true;
            }
            else if (chessPiece is Rook rook)
            {
                rook.IsMoved = true;
            }
            else if (chessPiece is Pawn pawn)
            {
                if (pawn.CurrentPosition.Row == 8 && pawn.Color == Colors.White)
                {
                    //TODO: Добавить выбор фигуры.
                    var piece = new Queen(pawn.Color, pawn.CurrentPosition);
                    ReplacePawnWithPiece(pawn, piece);
                }
                else if (pawn.CurrentPosition.Row == 1 && pawn.Color == Colors.Black)
                {
                    //TODO: Добавить выбор фигуры.
                    var piece = new Queen(pawn.Color, pawn.CurrentPosition);
                    ReplacePawnWithPiece(pawn, piece);
                }
            }
        }

        /// <summary>
        /// Подписка на события.
        /// </summary>
        private static void SubscribingToEvents()
        {
            Chessboard.ChessPieces.ForEach(f => f.IsMove += UpdatePositions);
        }

        /// <summary>
        /// Отписка от событий.
        /// </summary>
        private static void UnsubscribingFromEvents()
        {
            Chessboard.ChessPieces.ForEach(f => f.IsMove -= UpdatePositions);
        }
        
        /// <summary>
        /// Обновление позиций.
        /// </summary>
        private static void UpdatePositions()
        {
            _positionsList = new List<(Cell, Colors)>();

            Chessboard.ChessPieces.ForEach(f => _positionsList.Add((f.CurrentPosition, f.Color)));
            Chessboard.ChessPieces.ForEach(f => f.GetAsyncPositions(_positionsList).Wait());

            IsKingCanCastle(Colors.White);
            IsKingCanCastle(Colors.Black);
        }

        private static void ReplacePawnWithPiece(Pawn pawn, AbstractChessPiece piece)
        {
            var positionsList = new List<(Cell, Colors)> { (pawn.CurrentPosition, pawn.Color) };

            piece.IsMove += UpdatePositions;
            piece.GetAsyncPositions(positionsList).Wait();

            Chessboard.ChessPieces.Remove(pawn);
            Chessboard.ChessPieces.Add(piece);
        }

        /// <summary>
        /// Проверяет может ли король сделать рокировку.
        /// </summary>
        /// <param name="color"> Цвет короля. </param>
        private static void IsKingCanCastle(Colors color)
        {
            King king = (King)Chessboard[PieceNames.King, color].First();

            if (king.IsMoved == false)
            {
                List<Rook> rooks = Chessboard[PieceNames.Rook, color].Select(s => (Rook)s).ToList();

                var emptyColor = color == Colors.White ? Colors.Black : Colors.White;
                var row = color == Colors.White ? 1 : 8;

                foreach (var rook in rooks.Where(w => w.IsMoved == false))
                {
                    if (rook.IsMoved == false)
                    {
                        if (rook.CurrentPosition == new Cell(1, row))
                        {
                            // Если среди всех черных фигур никто не создает битое поле.
                            if (!Chessboard.ChessPieces.Any(a => a.Color == emptyColor && a.PossiblePositions.Any(a => a == new Cell(3, row) || a == new Cell(4, row))))
                            {
                                king.PossiblePositions.Add(new Cell(3, row));
                            }
                        }
                        else if (rook.CurrentPosition == new Cell(8, row))
                        {
                            // Если среди всех черных фигур никто не создает битое поле.
                            if (!Chessboard.ChessPieces.Any(a => a.Color == emptyColor && a.PossiblePositions.Any(a => a == new Cell(6, row) || a == new Cell(7, row))))
                            {
                                king.PossiblePositions.Add(new Cell(7, row));
                            }
                        }
                    }
                }
            }
        }
    }
}
