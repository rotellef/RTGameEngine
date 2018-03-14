using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTGameEngine
{
	/// <summary>
	/// https://www.youtube.com/playlist?list=PLjZCjpUWUuoCZ5oHGuM27KXnU3TBw5nUr
	/// </summary>
	class Game
	{
		public const int CANVAS_WIDTH = 1200;
		public const int CANVAS_HEIGHT = 700;

		private GEngine gEngine;
		private CancellationTokenSource source;

		public void StartGraphics(Graphics g)
		{
			Console.WriteLine("GEngine created and initiated with Graphics g.");
			source = new CancellationTokenSource();
			gEngine = new GEngine(g, source.Token);
			gEngine.Init();
		}

		public void StopGame()
		{
			source.Cancel();
			source.Dispose();
		}

		internal void KeyDown(object sender, KeyEventArgs e)
		{
			gEngine.KeyDown(sender, e);
		}
	}
}
