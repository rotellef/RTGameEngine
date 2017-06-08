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

		public Point Position { set; get; }

		public override string ToString()
		{
			return $"({Position.X},{Position.Y})";
		}
	}
}
