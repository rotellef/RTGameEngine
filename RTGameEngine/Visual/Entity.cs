using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTGameEngine.Visual
{
	class Entity
	{
		public Bitmap Gfx { set; get; }
		public Size HitBox { set; get; }
		public Point Position { set; get; }
		public int Direction { set; get; }
		public bool IsPlayer { set; get; } = false;


		public bool IsInside(int w, int h)
		{
			var brCorner = new Point(Position.X + HitBox.Width, Position.Y + HitBox.Height);
			return Position.IsInside(w, h) || brCorner.IsInside(w,h);
		}
		public bool IsCollition(Entity o)
		{
			var a = new Rectangle(this.Position, this.Gfx.Size);
			var b = new Rectangle(o.Position, o.Gfx.Size);
			return a.IsCollition(b);
		}
		public override string ToString() => $"({Position.X},{Position.Y}) direction={Direction}";
	}
}
