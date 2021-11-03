using ChessServer.Domain.Entites;
using ChessServer.Domain.Entites.Abstract;
using ChessServer.Domain.Entites.ChessPieces;
using ChessServer.Domain.Entites.ChessboardModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ChessServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private Game _game = new Game();

        // GET: api/Values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>();
        }

        // GET: api/Values/Positions
        [HttpGet("Positions")]
        public IEnumerable<AbstractChessPiece> GetPositions()
        {
            return _game.Chessboard.ChessPieces;
        }

        // GET: api/Values/Chessboard
        [HttpGet("Chessboard")]
        public IActionResult GetChessboard()
        {
            var array = new Cell[8][];

            for (int i = 0; i < 8; i++)
            {
                array[i] = new Cell[8];
                for (int j = 0; j < 8; j++)
                {
                    array[i][j] = _game.Chessboard.Cells[i, j];
                }
            }

            return Ok(array);
        }

        // GET api/Values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
