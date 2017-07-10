using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using RTGameEngine.Visual;
using static RTGameEngine.Extensions;

namespace RTGameEngine
{
	class GEngine
	{
		private readonly static Random Rng = new Random();

		private Graphics _drawHandle;
		private Thread _renderThread;

		private Entity _dude;
		private List<Entity> _allEntities = new List<Entity>();

		public GEngine(Graphics g)
		{
			_drawHandle = g;
		}

		public void Init()
		{
			LoadAssets();

			// Start render thread
			_renderThread = new Thread(new ThreadStart(Render));
			_renderThread.Start();

		}

		private void LoadAssets()
		{
			_dude = new Entity()
			{
				Gfx = Properties.Resources.circle,
				Position = new Point(500, 500)
			};
			_allEntities.Add(_dude);

			for (int i = 0; i < 100; i++)
			{
				_allEntities.Add(
					new Entity()
					{
						Gfx = Properties.Resources.tick,
						Position = new Point(Rng.Next(1, Game.CANVAS_WIDTH), Rng.Next(1, Game.CANVAS_HEIGHT))
					}
				);

			}
		}

		public void Stop()
		{
			_renderThread.Abort();
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
					Update();

					frameGraphics.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);

					//Paint the entities
					foreach (var ent in _allEntities)
					{
						frameGraphics.DrawImage(ent.Gfx, ent.Position);
					}


					// Swap buffer
					_drawHandle.DrawImage(frame, 0, 0);


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

		private void Update()
		{

			foreach (var ent in _allEntities)
			{
				ent.Position = ent.Position.Randomize();
            }
		}

        
	}
}

