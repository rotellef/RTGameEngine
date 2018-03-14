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

		public bool IsPlayer { set; get; } = false;

		public bool IsInside(int w, int h)
		{
			var brCorner = new Point(Position.X + HitBox.Width, Position.Y + HitBox.Height);
			return Position.IsInside(w, h) || brCorner.IsInside(w,h);
		}
		public override string ToString() => $"({Position.X},{Position.Y})";
	}
}
