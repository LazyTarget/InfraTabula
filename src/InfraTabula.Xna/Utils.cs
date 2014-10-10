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

    }
}
