using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public void StartGraphics(Graphics g)
		{
			Console.WriteLine("GEngine created and initiated with Graphics g.");
			gEngine = new GEngine(g);
			gEngine.Init();
		}

		public void StopGame()
		{
			gEngine.Stop();
		}
	}
}
