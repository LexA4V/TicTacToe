using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TicTac = TicTacToe.TicTacToe;
using TicTacToe;

namespace TicTacToe_WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {

            InitializeComponent();

            game = TicTac.Create((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);

            game.Moved += MovedEvent;
            game.Winner+=game_Winner;

            viewGame = new ViewTic();
            viewGame.Create(game, tLP, button_Click);
        }


        ViewTic viewGame;
        TicTac game;

   
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            Button B = (sender as Button);

            Point f = viewGame.gg.Where(x => x.Value.Equals(B)).Single().Key;

            game.Move(f.X, f.Y);
        }

        private void MovedEvent(object sender, TicTacToe.TicTacToeMoveEventArgs e)
        {
            Point gk = new Point(e.X, e.Y);

            Button t = viewGame.gg[gk];

            t.BackgroundImageLayout = ImageLayout.Stretch;

            if (e.CurrentTeam.Symbol == TicTacToe.XO.X)
            {
                t.BackgroundImage = Properties.Resources.cross;
                //t.BackColor = Color.Red;
                pBCurrentSide.BackgroundImage = Properties.Resources.zero;
            }
            else if (e.CurrentTeam.Symbol == TicTacToe.XO.O)
            {
                t.BackgroundImage = Properties.Resources.zero;
                //t.BackColor = Color.Green;
                pBCurrentSide.BackgroundImage = Properties.Resources.cross;
            }

            t.Enabled = false;
        }

        private void game_Winner(object sender, TicTacToeWinEventArgs e)
        {
            foreach (var a in e.lineWin)
            {
                viewGame.gg[a].BackColor = Color.Green;
            }
            tLP.Enabled = false;
        }

        private void Refrech(object sender, EventArgs e)
        {
            game.Reset();
            IEnumerable<Button> BtnE = tLP.Controls.OfType<Button>();
            foreach (Button b in BtnE)
            {
                b.Text = "";
                b.BackgroundImage = null;
                b.BackColor = Color.FromKnownColor(KnownColor.Control);
                b.Enabled = true;
            }
            tLP.Enabled = true;
            pBCurrentSide.BackgroundImage = Properties.Resources.cross;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About h = new About();
            h.Show();
        }

        #region сплин выхода нет
        string splin = @"Сколько лет прошло, все о том же гудят провода, все того же ждут самолеты.
Девочка с глазами из самого синего льда, тает под огнем пулемета.
Должен же растаять хоть кто-то.

Скоро рассвет, выхода нет, ключ поверни и полетели.
Нужно вписать в чью-то тетрадь, кровью, как в метрополитене:
""Выхода нет"", выхода нет!

Где-то мы расстались, не помню, в каких городах, словно это было в похмелье.
Через мои песни идут, идут поезда, исчезая в темном тоннеле.
Лишь бы мы проснулись в одной постели.

Скоро рассвет, выхода нет, ключ поверни и полетели.
Нужно вписать в чью-то тетрадь, кровью, как в метрополитене:
""Выхода нет"", выхода нет!

Сколько лет пройдет, все о том же гудеть проводам, все того же ждать самолетам.
Девочка с глазами из самого синего льда, тает под огнем пулемета.
Лишь бы мы проснулись с тобой в одной постели.

Скоро рассвет, выхода нет, ключ поверни и полетели.
Нужно вписать в чью-то тетрадь, кровью, как в метрополитене:
""Выхода нет"", выхода нет! Выхода нет! Выхода нет!";
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show( "Хочtшь выйти?", "Выход?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MessageBox.Show("Выхода нет!!!", "Выход?(ха-ха)",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                MessageBox.Show(splin, "Выход?",  MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Refrech(sender, e);

            viewGame.Clear(tLP);

            try
            {
                game = TicTac.Create((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            
            game.Moved += MovedEvent;
            game.Winner += game_Winner;
            viewGame.Create(game, tLP, button_Click);
            }
            catch { }
        }

        
    }
}
