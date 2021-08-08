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
        public List<Cell> PossiblePositions { get; private set; }

        /// <summary>
        /// Все позиции.
        /// </summary>
        public List<Cell> AllPositions { get; private set; }

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

            IsMove?.Invoke();

            return _newPosition;
        }

        /// <summary>
        /// Получает все позиции.
        /// </summary>
        /// <returns> Все позиции. </returns>
        public virtual async Task<List<Cell>> GetAsyncAllPositions()
        {
            // Заглушка.
            return await Task.Run(() => AllPositions);
        }

        /// <summary>
        /// Устанавливает все возможные позиции.
        /// </summary>
        /// <returns> Все возможные позиции. </returns>
        public abstract void SetPossiblePositions(List<Cell> _possiblePositions);
    }
}
