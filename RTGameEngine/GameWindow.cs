using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RTGameEngine
{
	public partial class GameWindow : Form
	{
		private Game _game = new Game();
        private bool _graphicsStarted = false;

		public GameWindow()
		{
			InitializeComponent();
		}

		private void canvas_Paint(object sender, PaintEventArgs e)
		{
            if(!_graphicsStarted)
            {
			    _game.StartGraphics(canvas.CreateGraphics());
                _graphicsStarted = true;

            }
		}

		private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			_game.StopGame();
		}
		private void GameWindow_Load(object sender, EventArgs e)
		{
		
		}

		

	}
}
