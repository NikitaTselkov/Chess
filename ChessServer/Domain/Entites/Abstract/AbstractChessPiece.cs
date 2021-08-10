using ChessServer.Domain.Entites.ChessboardModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessServer.Domain.Entites.Abstract
{
    public abstract class AbstractChessPiece
    {
        /// <summary>
        /// Фигура.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Цвет фигуры.
        /// </summary>
        public Colors Color { get; private set; }

        /// <summary>
        /// Текущая позиция.
        /// </summary>
        public Cell CurrentPosition { get; private set; }

        /// <summary>
        /// Возможные позиции.
        /// </summary>
        public List<Cell> PossiblePositions { get; set; }

        /// <summary>
        /// Все позиции.
        /// </summary>
        public List<Cell> AllPositions { get; set; }

        /// <summary>
        /// Событие перемещения.
        /// </summary>
        public delegate void Notify();
        public event Notify IsMove;

        public AbstractChessPiece() { }

        public AbstractChessPiece(string _name, Colors _color, Cell _currentPosition)
        {
            Name = _name;
            Color = _color;
            CurrentPosition = _currentPosition;
            PossiblePositions = new List<Cell>();
            AllPositions = new List<Cell>();
        }

        /// <summary>
        /// Перемещение.
        /// </summary>
        public virtual Cell Move(Cell _newPosition)
        {
            // Если такой ход не возможен.
            if (!PossiblePositions.Contains(_newPosition))
                throw new ArgumentNullException(nameof(_newPosition), "Такой ход не возможен.");

            CurrentPosition = _newPosition;
            IsMove?.Invoke();

            return _newPosition;
        }

        /// <summary>
        /// Получает все позиции.
        /// </summary>
        /// <param name="positions"> Занятые позиции и цвета фигур. </param>
        public virtual async Task GetAsyncPositions(List<(Cell, Colors)> positions)
        {
            // Заглушка.
            await Task.Run(() => AllPositions);
        }

        /// <summary>
        /// Устанавливает значения спискам позиций.
        /// </summary>
        /// <param name="positions"> Позиция фигуры и её цвет. </param>
        /// <param name="cell"> Устанавливаемое значение. </param>
        /// <param name="isContains"> Если поле впереди по диагонали занято. </param>
        public virtual void SetValueToPositions(List<(Cell, Colors)> positions, Cell cell, ref bool isContains)
        {
            Cell temp = cell;
            AllPositions.Add(temp);

            // Если на пути есть фигура.
            if (isContains == false)
            {
                if (positions.Any(a => a.Item1 == temp))
                {
                    var tmp = positions.First(f => f.Item1 == temp);

                    if (tmp.Item2 != Color)
                    {
                        PossiblePositions.Add(temp);
                    }
                    isContains = true;
                }
                else
                {
                    PossiblePositions.Add(temp);
                }
            }
        }

        // TODO: Удалить.
        /// <summary>
        /// Получает все позиции.
        /// </summary>
        /// <returns> Все позиции. </returns>
        public virtual async Task<List<Cell>> GetAsyncAllPositions()
        {
            // Заглушка.
            return await Task.Run(() => AllPositions);
        }
    }
}
