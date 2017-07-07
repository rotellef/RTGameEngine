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
    }
}
