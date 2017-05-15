using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace RTGameEngine
{
	class GEngine
	{
		private Graphics drawHandle;
		private Thread renderThread;

		private Bitmap circle;

		public GEngine(Graphics g)
		{
			drawHandle = g;
		}

		public void Init()
		{
			LoadAssets();

			// Start render thread
			renderThread = new Thread(new ThreadStart(Render));
			renderThread.Start();
		}

		private void LoadAssets()
		{
			circle = RTGameEngine.Properties.Resources.circle;
		}

		public void Stop()
		{
			renderThread.Abort();
		}

		private void Render()
		{
			int framesRendered = 0;
			long startTime = Environment.TickCount;

			Bitmap frame = new Bitmap(Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);
			Graphics frameGraphics = Graphics.FromImage(frame);

			while (true)
			{
				frameGraphics.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);

				for (int i = 0; i < 10; i++)
				{
					frameGraphics.DrawImage(circle, i*100, 100);
				}

				drawHandle.DrawImage(frame, 0, 0);

				// Benchmarking
				framesRendered++;
				if (Environment.TickCount >= startTime + 1000)
				{
					Console.WriteLine($"GEngine {framesRendered} fps");
					framesRendered = 0;
					startTime = Environment.TickCount;
				}
			}
		}
	}
}
