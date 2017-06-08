using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using RTGameEngine.Visual;

namespace RTGameEngine
{
	class GEngine
	{
		private Graphics drawHandle;
		private Thread renderThread;

		private Entity dude;

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
			dude = new Entity() {
				Gfx = Properties.Resources.circle,
				Position = new Point(20, 20)
			};
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
			
			// The graphics instance we should use to draw sceen on
			Graphics frameGraphics = Graphics.FromImage(frame);

			try
			{	
				/**************
				 * MASTER LOOOP
				 **************/
				while (true)
				{
					frameGraphics.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);

					frameGraphics.DrawImage(dude.Gfx, dude.Position);
					

					// Swap buffer
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
			catch (Exception ex)
			{ 
				Console.WriteLine("Exception happened while drawing to screen: " + ex.StackTrace);
			}
		}
	}
}

