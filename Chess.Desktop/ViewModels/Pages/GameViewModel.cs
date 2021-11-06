﻿using Chess.Desktop.Models;
using Chess.Desktop.ViewModels.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chess.Desktop.ViewModels.Pages
{
    public class GameViewModel : NavigateViewModel
    {
        private Cell[] _chessboard;
        public Cell[] Chessboard
        {
            get => _chessboard;
            set
            {
                _chessboard = value;
                RaisePropertyChanged(nameof(Chessboard));
            }
        }

        private IEnumerable<ChessPiece> _chessPieces;
        public IEnumerable<ChessPiece> ChessPieces
        {
            get => _chessPieces;
            set
            {
                _chessPieces = value;
                RaisePropertyChanged(nameof(ChessPieces));
            }
        }

        public IEnumerable<char> Numbers => "12345678";
        public IEnumerable<char> Letters => "ABCDEFGH";

        public RelayCommand ClickCellCommand { get; set; }

        public GameViewModel()
        {
            Chessboard = Server.GetChessboard();
            ChessPieces = Server.GetChessPiecesPositions();

            ClickCellCommand = new RelayCommand(ClickMethod);

            UpdateChessboard();
        }

        private void ClickMethod(object param)
        {
            Cell cell = (Cell)param;
            Cell activeCell = Chessboard.FirstOrDefault(x => x.IsActive);
            var taskFactory = new TaskFactory();

            if (cell.State != State.Empty)
            {
                if (activeCell != null && !cell.IsActive)
                    activeCell.IsActive = false;
                cell.IsActive = !cell.IsActive;
            }
            else if (activeCell != null)
            {
                activeCell.IsActive = false;
                activeCell.State = State.Empty;

                Server.SendMove(activeCell.Title, cell.Title);            
                ChessPieces = Server.GetChessPiecesPositions();
                UpdateChessboard(); 
            }
        }

        private void UpdateChessboard()
        {
            foreach (var item in ChessPieces)
            {
                Chessboard.FirstOrDefault(f => f.Title == item.CurrentPosition.Title).State = ConvertToState(item.NameCode, item.Color);
            }
        }

        private static State ConvertToState(int nameCode, Colors color)
        {
            int result = nameCode + 1;

            if (result != 0 && color == Colors.Black)
            {
                result += 6;
            }

            return (State)result;
        }

        private static (int nameCode, Colors color) ConvertFromState(State state)
        {
            int nameCode = (int)state - 1;
            Colors color = Colors.White;

            if (nameCode > 6)
            {
                color = Colors.Black;
                nameCode -= 6;
            }

            return (nameCode, color);
        }
    }
}
