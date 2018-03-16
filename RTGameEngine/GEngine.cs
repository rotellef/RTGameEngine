using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using RTGameEngine.Visual;
using static RTGameEngine.Extensions;
using System.Windows.Forms;

namespace RTGameEngine
{
	class GEngine
	{
		private readonly static Random Rng = new Random();

		private Graphics _drawHandle;
		private readonly CancellationToken _token;
		private Thread _renderThread;

		private Entity _dude;
		private List<Entity> _allEntities = new List<Entity>();

		public GEngine(Graphics g, CancellationToken token)
		{
			_drawHandle = g;
			_token = token;
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
				Position = new Point(500, 500),
				IsPlayer = true
			};
			_allEntities.Add(_dude);

			var tick = Properties.Resources.tick;
			for (int i = 0; i < 1000; i++)
			{
				var pos = new Point(Rng.Next(1, Game.CANVAS_WIDTH), Rng.Next(1, Game.CANVAS_HEIGHT));
				_allEntities.Add(new Entity() { Gfx = tick, Position = pos, HitBox = tick.Size });
			}
		}

		internal void KeyDown(object sender, KeyEventArgs e)
		{
			int oneStep = 5;
			switch (e.KeyData)
			{
				case Keys.W:
					_dude.Position = _dude.Position.Up(oneStep);
					break;
				case Keys.S:
					_dude.Position = _dude.Position.Down(oneStep);
					break;
				case Keys.A:
					_dude.Position = _dude.Position.Left(oneStep);
					break;
				case Keys.D:
					_dude.Position = _dude.Position.Right(oneStep);
					break;

			}
		}

		private void Render()
		{
			int framesRendered = 0;
			long startTime = Environment.TickCount;

			Bitmap frame = new Bitmap(Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);

			// The graphics instance we should use to draw sceen on
			Graphics frameGraphics = Graphics.FromImage(frame);
			frameGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			//frameGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;

			try
			{
				/**************
				 * MASTER LOOOP
				 **************/
				while (!_token.IsCancellationRequested)
				{
					Update();

					frameGraphics.FillRectangle(new SolidBrush(Color.Aqua), 0, 0, Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT);

					//Paint the entities
					var insideCanvas = from e in _allEntities where e.IsInside(Game.CANVAS_WIDTH, Game.CANVAS_HEIGHT) select e;
					foreach (var ent in insideCanvas)
					{
						frameGraphics.DrawImage(ent.Gfx, ent.Position.X, ent.Position.Y, ent.Gfx.Width, ent.Gfx.Height);
					}


					// Swap buffer
					_drawHandle.DrawImage(frame, 0, 0);


					// Benchmarking FPS
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
			var start = new Point(40, 40);
			var npcs = from e in _allEntities where !e.IsPlayer select e;
			foreach (var me in npcs)
			{
				// TODO: calc direction and speed.
				me.Position = me.Position.Randomize();
				// Collition checks
				var others = from i in npcs where i != me select i;
				
				foreach (var other in others)
				{
					if (me.IsCollition(other))
					{
						me.Position = start;
						other.Position = start;
					}
				}
			}

		}

        
	}
}

