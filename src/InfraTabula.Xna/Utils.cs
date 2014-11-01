using System;
using Microsoft.Xna.Framework;

namespace InfraTabula.Xna
{
    public static class Utils
    {
        private static readonly Random _random = new Random();



        public static Color RandomColor()
        {
            var color = RandomColor(false);
            return color;
        }

        public static Color RandomColor(bool randomAlpha)
        {
            var r = _random.Next(255);
            var g = _random.Next(255);
            var b = _random.Next(255);
            var a = 255;
            if (randomAlpha)
                a = _random.Next(10, 255);
            var color = new Color(r, g, b, a);
            return color;
        }



        public static Vector2 ToVector2(this Point point)
        {
            var res = new Vector2(point.X, point.Y);
            return res;
        }

        public static Point ToPoint(this Vector2 vector)
        {
            var res = new Point(
                (int) Math.Round(vector.X),
                (int) Math.Round(vector.Y));
            return res;
        }



        public static Point Inflate(this Point point, int increment)
        {
            var res = new Point(point.X + increment, point.Y + increment);
            return res;
        }


        public static Vector2 GetRelative(this Vector2 parent, float percentageX, float percentageY)
        {
            var x = parent.X * percentageX;
            var y = parent.Y * percentageY;
            var pnt = new Vector2(x, y);
            return pnt;
        }

    }
}
