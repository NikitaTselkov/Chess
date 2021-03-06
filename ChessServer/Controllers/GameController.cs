using ChessServer.Domain.Entites;
using ChessServer.Domain.Entites.Abstract;
using ChessServer.Domain.Entites.ChessPieces;
using ChessServer.Domain.Entites.ChessboardModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        // GET: api/Values/Positions
        [HttpGet("Positions")]
        public IEnumerable<AbstractChessPiece> GetPositions()
        {
            return Game.Chessboard.ChessPieces;
        }

        // GET: api/Values/Chessboard
        [HttpGet("Chessboard")]
        public IEnumerable<Cell> GetChessboard()
        {
            var array = new Cell[64];
            var k = 0;

            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    array[k] = Game.Chessboard.Cells[i, j];
                    k++;
                }
            }

            return array;
        }

        // PUT api/Values/SendMove/{oldPositionTitle}/{newPositionTitle}
        [HttpPut("SendMove/{oldPositionTitle}/{newPositionTitle}")]
        public void Put(string oldPositionTitle, string newPositionTitle)
        {
            var currentCell = Game.Chessboard.Cells[oldPositionTitle];
            var newCell = Game.Chessboard.Cells[newPositionTitle];
            var chessPiese = Game.Chessboard.ChessPieces.FirstOrDefault(f => f.CurrentPosition == currentCell);

            Game.MoveWithCheckGameRules(chessPiese, newCell);
        }
    }
}
