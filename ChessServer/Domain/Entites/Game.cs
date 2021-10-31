using ChessServer.Domain.Entites.ChessboardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites
{
    public sealed class Game
    {
        public Chessboard Chessboard { get; private set; }
        public (bool, Colors) IsCheck { get; private set; }
        private List<(Cell, Colors)> _positionsList;

        public Game()
        {
            Chessboard = new Chessboard();

            UpdatePositions();
            SubscribingToEvents();

            Chessboard.ChessPieces[0].Move(new Cell(1, 4));
        }

        /// <summary>
        /// Проверка соблюдения правил.
        /// </summary>
        private void CheckGameRules()
        {
            var whiteKing = Chessboard[PieceNames.King, Colors.White];
        }

        /// <summary>
        /// Подписка на события.
        /// </summary>
        private void SubscribingToEvents()
        {
            Chessboard.ChessPieces.ForEach(f => f.IsMove += UpdatePositions);
        }

        /// <summary>
        /// Отписка от событий.
        /// </summary>
        private void UnsubscribingFromEvents()
        {
            Chessboard.ChessPieces.ForEach(f => f.IsMove -= UpdatePositions);
        }
        
        /// <summary>
        /// Обновление позиций.
        /// </summary>
        private void UpdatePositions()
        {
            _positionsList = new List<(Cell, Colors)>();

            Chessboard.ChessPieces.ForEach(f => _positionsList.Add((f.CurrentPosition, f.Color)));

            Chessboard.ChessPieces.ForEach(f => f.GetAsyncPositions(_positionsList).Wait());
        }
    }
}
