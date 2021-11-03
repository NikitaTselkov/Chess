using Chess.Desktop.Models;
using Chess.Desktop.ViewModels.Navigation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        public GameViewModel()
        {
            Chessboard = GetChessboardFromServer();
            ChessPieces = GetChessPiecesPositionsFromServer();
        }

        public IEnumerable<ChessPiece> GetChessPiecesPositionsFromServer()
        {
            var data = Server.GetDataFromServer("Positions");
            return JsonConvert.DeserializeObject<IEnumerable<ChessPiece>>(data.Result);
        }

        public Cell[] GetChessboardFromServer()
        {
            var data = Server.GetDataFromServer("Chessboard");
            var array = JsonConvert.DeserializeObject<Cell[][]>(data.Result);
            var result = new Cell[64];

            var k = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    result[k] = array[i][j];
                    k++;
                }
            }

            return result;
        }
    }
}
