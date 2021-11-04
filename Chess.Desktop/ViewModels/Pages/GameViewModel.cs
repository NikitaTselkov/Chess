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
            var data = Server.GetDataFromServer("Positions");
            return JsonConvert.DeserializeObject<IEnumerable<ChessPiece>>(data.Result);
        }

        public Cell[] GetChessboardFromServer()
        {
            var data = Server.GetDataFromServer("Chessboard");
            return JsonConvert.DeserializeObject<Cell[]>(data.Result);
        }

        private void ClickMethod(object parameter)
        {
            Cell cell = (Cell)parameter;
            Cell activeCell = Chessboard.FirstOrDefault(x => x.IsActive);
            if (cell.State != State.Empty)
            {
                if (!cell.IsActive && activeCell != null)
                    activeCell.IsActive = false;
                cell.IsActive = !cell.IsActive;
            }
            else if (activeCell != null)
            {
                activeCell.IsActive = false;
                cell.State = activeCell.State;
                activeCell.State = State.Empty;
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
    }
}
