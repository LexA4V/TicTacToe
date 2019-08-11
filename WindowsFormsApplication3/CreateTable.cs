using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TicTacToe_WinForms
{
    class ViewTic
    {
        private Dictionary<Point, Button> list = new Dictionary<Point, Button>();

        public Dictionary<Point, Button> gg { get { return list; } }

        public void Create(TicTacToe.TicTacToe game, TableLayoutPanel tLP, EventHandler btnClick)
        {
            tLP.ColumnStyles.Clear();
            tLP.RowStyles.Clear();

            int column = game.Board.Width;
            int row = game.Board.Height;

            tLP.ColumnCount = column;
            tLP.RowCount = row;

            for (int i = 0; i <= column; i++)
                tLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            for (int i = 0; i <= row; i++)
                tLP.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

            Button tempBtn;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                {
                    tempBtn = new Button();
                    tempBtn.Dock = DockStyle.Fill;
                    //tempBtn.FlatAppearance.BorderSize = 0;
                    //tempBtn.FlatStyle = FlatStyle.Flat;



                    tempBtn.UseVisualStyleBackColor = true;

                    tempBtn.Click += btnClick;
                    tempBtn.Margin = new Padding(1);
                    tLP.Controls.Add(tempBtn, j, i);
                    list.Add(new Point(i, j), tempBtn);
                }
        }

        public void Clear(TableLayoutPanel tLP)
        {
            tLP.Controls.Clear();
            tLP.ColumnStyles.Clear();
            tLP.RowStyles.Clear();
            list.Clear();
        }
    }
}
