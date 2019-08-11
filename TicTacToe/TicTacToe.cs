using System;
using TicTacToe.TeamsXO;
using System.Drawing;
using System.Collections.Generic;

namespace TicTacToe
{


    public class TicTacToe
    {
        public event TicTacToeMoveStateHandler Moved = delegate { };

        public event TicTacToeWinStateHandler Winner = delegate { };

        private Board _board;
        private int _seriesWin;
        private Side _side;


        public Board Board
        {
            get
            {
                return _board;
            }
        }
        public Side Side
        {
            get
            {
                return _side;
            }
        }


        private TicTacToe(int height = 3, int windth = 3, int seriesWin = 3)
        {
            if (seriesWin > height || seriesWin > windth) throw new BoardExeption(_board, "серия больше доски");
            _seriesWin = seriesWin;
            _board = Board.Create(height, windth);
            _side = Side.Create();
        }

        public static TicTacToe Create(int size, int _seriesWin)
        {
            return new TicTacToe(size, _seriesWin);
        }
        public static TicTacToe Create(int height, int windth, int _seriesWin)
        {
            return new TicTacToe(height, windth, _seriesWin);
        }


        #region делается очередной ход

        public void Move(int x, int y)
        {
            _board[x, y] = _side.Current.Symbol;

            Moved(this, new TicTacToeMoveEventArgs(this, x, y));
            List<Point> g = new List<Point>();
            if (Result(x, y, ref g))
            {
                
                Winner(this, new TicTacToeWinEventArgs(this, g)); 
                //return true;
            }
            _side.changeTeam();
            //делается ход
            //return false;
        }

        #region разделение функция для разных направлений

        private bool checkHorizontal(int x, int y, ref List<Point> LineWin)
        {
            XO team = _side.Current.Symbol;

            int jStart = 0, jFinish = _board.Width - 1;

            int kToWin; //число подряд идущих одинаковых знаков

            int jTempCoords;

            int j; //не столбец а счётчик развилки


            //поиск победы по горизонтали

            kToWin = 1;

            //увеличиваем счётчик на  и проверяем элементы по бокам
            //если элемент вышел а рамки массива или не пренадлежит к текущему знаку( Х О )
            //тогда мы перестаём идти в эту сторону
            //цикл работает пока мы можем идти в какую нибудь и сторон

            bool Right, Left;
            Right = Left = true;

            LineWin.Add(new Point(x, y));

            j = 1;
            while (Right || Left)
            {
                if (Right)
                {
                    jTempCoords = y + j;
                    if (jTempCoords <= jFinish && _board[x, jTempCoords] == team)
                    {
                        kToWin++;
                        LineWin.Add(new Point(x, jTempCoords));
                    }
                    else Right = false;
					if (kToWin == _seriesWin) break;
                }
                if (Left)
                {
                    jTempCoords = y - j;
                    if (jTempCoords >= jStart && _board[x, jTempCoords] == team)
                    {
                        kToWin++;
                        LineWin.Add(new Point(x, jTempCoords));
                    }
                    else Left = false;
                    if (kToWin == _seriesWin) break;
                }
                j++;
            }
			if(kToWin!=_seriesWin) LineWin.Clear();
			return kToWin ==_seriesWin;
        }

		private bool checkVertical(int x, int y, ref List<Point> LineWin)
        {
            XO team = _side.Current.Symbol;

            int iStart = 0, iFinish = _board.Height - 1;


            int kToWin; //число подряд идущих одинаковых знаков

            int iTempCoords;

            int j; //не столбец а счётчик развилки
			
			//поиск победы по вертикали

            kToWin = 1;

            //увеличиваем счётчик на  и проверяем элементы по бокам
            //если элемент вышел а рамки массива или не пренадлежит к текущему знаку( Х О )
            //тогда мы перестаём идти в эту сторону
            //цикл работает пока мы можем идти в какую нибудь и сторон
            LineWin.Add(new Point(x, y));

            bool Up, Down;
            Up = Down = true;

            j = 1;
            while (Down || Up)
            {
                if (Down)
                {
                    iTempCoords = x + j;
                    if (iTempCoords <= iFinish && _board[iTempCoords, y] == team)
                    {
                        kToWin++;
                        LineWin.Add(new Point(iTempCoords, y));
                    }
                    else Down = false;
                    if (kToWin == _seriesWin) break;
                }
                if (Up)
                {
                    iTempCoords = x - j;
                    if (iTempCoords >= iStart && _board[iTempCoords, y] == team)
                    {
                        kToWin++;
                        LineWin.Add(new Point(iTempCoords, y));
                    }
                    else Up = false;
                    if (kToWin == _seriesWin) break;
                }
                j++;
            }
            if (kToWin != _seriesWin) LineWin.Clear();
            return kToWin == _seriesWin;
        }
		
		private bool checkDiagonal(int x, int y, ref List<Point> LineWin)
        {
            XO team = _side.Current.Symbol;

			int jStart =0, jFinish = _board.Width-1;
            int iStart = 0, iFinish = _board.Height - 1;


            int kToWin; //число подряд идущих одинаковых знаков

            int jTempCoords, iTempCoords;

            int j; //не столбец а счётчик развилки


            //поиск победы по главной диагонали

            kToWin = 1;
            LineWin.Add(new Point(x, y));

            bool UpLeft, DownRight;
            UpLeft = DownRight = true;

            j = 1;
            while (DownRight || UpLeft)
            {
                if (DownRight)
                {
                    iTempCoords = x + j;
                    jTempCoords = y + j;
                    if ((iTempCoords <= iFinish && jTempCoords <= jFinish) &&
                        _board[iTempCoords, jTempCoords] == team)
                    {
                        kToWin++;
                        LineWin.Add(new Point(iTempCoords, jTempCoords));
                    }
                    else DownRight = false;
					if (kToWin == _seriesWin) break;
                }
                if (UpLeft)
                {
                    iTempCoords = x - j;
                    jTempCoords = y - j;
                    if ((iTempCoords >= iStart && jTempCoords >= jStart) &&
                        _board[iTempCoords, jTempCoords] == team)
                    {
                        kToWin++;
                        LineWin.Add(new Point(iTempCoords, jTempCoords));
                    }
                    else UpLeft = false;
					if (kToWin == _seriesWin) break;
                }
                j++;
            }
            if (kToWin != _seriesWin) LineWin.Clear();
            return kToWin == _seriesWin;
        }

		private bool checkSecondDiagonal(int x, int y, ref List<Point> LineWin)
        {
            XO team = _side.Current.Symbol;

			int jStart =0, jFinish = _board.Width-1;
            int iStart = 0, iFinish = _board.Height - 1;


            int kToWin; //число подряд идущих одинаковых знаков

            int jTempCoords, iTempCoords;

            int j; //не столбец а счётчик развилки


            //поиск победы по побочной диагонали

            kToWin = 1;

            iStart = 0 ;                iFinish = _board.Height-1;
            jStart = _board.Width - 1;  jFinish = 0;

            LineWin.Add(new Point(x, y));

            bool UpRight, DownLeft;
            UpRight = DownLeft = true;

            j = 1;
            while (UpRight || DownLeft)
            {
                if (UpRight)
                {
                    iTempCoords = x - j;
                    jTempCoords = y + j;
                    if ((iTempCoords >= iStart && jTempCoords <= jStart) &&
                        _board[iTempCoords, jTempCoords] == team)
                    {
                        kToWin++;
                        LineWin.Add(new Point(iTempCoords, jTempCoords));
                    }
                    else UpRight = false;
                    if (kToWin == _seriesWin) break;
                }
                if (DownLeft)
                {
                    iTempCoords = x + j;
                    jTempCoords = y - j;
                    if ((iTempCoords <= iFinish && jTempCoords >= jFinish) &&
                        _board[iTempCoords, jTempCoords] == team)
                    {
                        kToWin++;
                        LineWin.Add(new Point(iTempCoords, jTempCoords));
                    }
                    else DownLeft = false;
                    if (kToWin == _seriesWin) break;
                }
                j++;
            }
            if (kToWin != _seriesWin) LineWin.Clear();
            return kToWin == _seriesWin;
        }

        #endregion

        private bool Result(int x, int y, ref List<Point> LineWin)
        {
            if (checkHorizontal(x, y, ref LineWin))
            {

                return true;
            }
            else if (checkVertical(x, y, ref LineWin))
            {

                return true;
            }
            else if (checkDiagonal(x, y, ref LineWin))
            {

                return true;
            }
            else if (checkSecondDiagonal(x, y, ref LineWin))
            {

                return true;
            }



            return false;
        }

        #endregion


        public void Reset()
        {
            Board.Clear();
            Side.Reset();
        }
    }
}
