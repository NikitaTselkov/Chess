using Chess.Desktop.Models;
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

        public IEnumerable<char> Numbers => "87654321";
        public IEnumerable<char> Letters => "ABCDEFGH";

        public RelayCommand ClickCellCommand { get; set; }

        public GameViewModel()
        {
            Chessboard = GetChessboardFromServer();
            ChessPieces = GetChessPiecesPositionsFromServer();

            ClickCellCommand = new RelayCommand(ClickMethod);

            UpdateChessboard();
        }

        public IEnumerable<ChessPiece> GetChessPiecesPositionsFromServer()
        {
            var data = Server.GetDataFromServerAsync("Positions");
            return JsonConvert.DeserializeObject<IEnumerable<ChessPiece>>(data.Result);
        }

        public Cell[] GetChessboardFromServer()
        {
            var data = Server.GetDataFromServerAsync("Chessboard");
            return JsonConvert.DeserializeObject<Cell[]>(data.Result);
        }

        public void SendMoveToServer(string oldPositionTitle, string newPositionTitle)
        {
            Server.SetDataToServerAsync($"SendMove/{oldPositionTitle}/{newPositionTitle}");
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

                SendMoveToServer(activeCell.Title, cell.Title);
                taskFactory.StartNew(() => ChessPieces = GetChessPiecesPositionsFromServer()).GetAwaiter().GetResult();
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
