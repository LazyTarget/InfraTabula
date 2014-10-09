using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Win
{
    public class Sprite
    {
        private static readonly Random _random = new Random();
        

        public Sprite(Texture2D texture, Color color, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Color = color;
        }


        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }
        public Color Color { get; set; }


        public static Sprite GetRandom(GraphicsDevice graphicsDevice)
        {
            var width = _random.Next(50, 250);
            var height = _random.Next(50, 250);
            
            var posX = _random.Next(graphicsDevice.Viewport.Width - width);
            var posY = _random.Next(graphicsDevice.Viewport.Height - height);
            var pos = new Vector2(posX, posY);


            var r = _random.Next(255);
            var g = _random.Next(255);
            var b = _random.Next(255);
            var a = _random.Next(10, 255);
            var color = new Color(r, g, b, a);

            //Random randomGen = new Random();
            //var names = (System.Drawing.KnownColor[])Enum.GetValues(typeof(System.Drawing.KnownColor));
            //var randomColorName = names[randomGen.Next(names.Length)];
            //System.Drawing.Color randomColor = System.Drawing.Color.FromKnownColor(randomColorName);
            //var color = XnaLibrary.Extensions.FromColor(randomColor);


            //var texture = new Texture2D(graphicsDevice, width, height);
            var texture = App.CreateRectangle(graphicsDevice, width, height, color);

            var s = new Sprite(texture, color, pos);
            return s;
        }

    }
}
