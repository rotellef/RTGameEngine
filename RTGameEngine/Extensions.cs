using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTGameEngine
{
	/// <summary>
	/// A collection of extension methods for known classes in .Net
	/// </summary>
	public static class Extensions
	{
		private readonly static Random Rng = new Random();
		public static Point Randomize(this Point p) => new Point(p.X + Rng.Next(-1, 2), p.Y + Rng.Next(-1, 2));
		public static Point Up(this Point p, int v) => new Point(p.X, p.Y - v);
		public static Point Down(this Point p, int v) => new Point(p.X, p.Y + v);
		public static Point Left(this Point p, int v) => new Point(p.X - v, p.Y);
		public static Point Right(this Point p, int v) => new Point(p.X + v, p.Y);
		public static bool IsCollition(this Rectangle a, Rectangle b)  {
			bool OutsideBottom = a.Bottom < b.Top;
			bool OutsideTop = a.Top > b.Bottom;
			bool OutsideLeft = a.Left > b.Right;
			bool OutsideRight = a.Right < b.Right;
			return !(OutsideBottom || OutsideTop || OutsideLeft || OutsideRight);
		}

			//Left, right, top, bottom
		public static bool IsInside(this Point p, int width, int height) =>
			0 <= p.X && p.X <= width && 0<= p.Y && p.Y <= height;

	}
}
