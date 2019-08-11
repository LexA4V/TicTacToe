using System;
using System.Drawing;


namespace TicTacToe
{
    public class Board
    {
        private sbyte[,] board;

        protected Board(int height, int width)
        {
            if (height < 3 || width < 3)
                throw new Exception("Слишком маленькая доска, попробуйте создать размерностью 3 или больше.");

            board = new sbyte[height, width];
        }

        public static Board Create(int height, int width)
        {
            return new Board(height, width);
        }

        public int Height
        {
            get { return board.GetLength(0); }
        }

        public int Width
        {
            get { return board.GetLength(1); }
        }

        public XO this[int i, int j]
        {
            get
            {
                if ((i < 0 || i >= Height) || (j < 0 || j >= Width)) throw new BoardExeption(this, "Выход за границы игрового поля!");
                return (XO)board[i, j];
            }

            internal set
            {
                if (board[i, j] == (sbyte)XO.Null) board[i, j] = (sbyte) value;
                else throw new BoardExeption(this, "Поле уже занято"); //другой ексепшенс без серьёзной ошибки

            }
        }

        internal void Clear()
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    board[i, j] = (sbyte)XO.Null;
        }
    }

    public class BoardExeption : Exception

    {
        Board board;
        public BoardExeption(Board board):this(board, null)
        {

        }

        public Point cell { get { return new Point(0, 0); } }

        public BoardExeption(Board board, string text) : base(text)
        {
            this.board = board;
        }

    }

}
