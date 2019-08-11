using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TicTacToe
{
    public delegate void TicTacToeMoveStateHandler(object sender, TicTacToeMoveEventArgs e);

    public delegate void TicTacToeWinStateHandler(object sender, TicTacToeWinEventArgs e);

    public class TicTacToeMoveEventArgs
    {
        // точка текущего хода
        // 
        public int X, Y;

        //команда что только что походила
        public TeamsXO.Team CurrentTeam  {get; private set;}

        public TicTacToeMoveEventArgs(TicTacToe game, int x, int y)
        {
            X = x;
            Y = y;
            CurrentTeam = game.Side.Current;
            
        }
    }

    public class TicTacToeWinEventArgs
    {
        public IEnumerable<Point> lineWin { get; private set; }

        //команда что только что походила
        public TeamsXO.Team CurrentTeam { get; private set; }

        public TicTacToeWinEventArgs(TicTacToe game, IEnumerable<Point> lineWin)
        {
            this.lineWin = lineWin;
            CurrentTeam = game.Side.Current;

        }
    }
}
