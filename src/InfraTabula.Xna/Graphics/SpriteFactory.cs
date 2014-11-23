using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class SpriteFactory
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly Game _game;

        public SpriteFactory(Game game)
        {
            _game = game;
            _graphicsDevice = _game.GraphicsDevice;
        }


        //public static Sprite GetRandom(GraphicsDevice graphicsDevice)
        //{
        //    var width = _random.Next(50, 250);
        //    var height = _random.Next(50, 250);

        //    var posX = _random.Next(graphicsDevice.Viewport.Width - width);
        //    var posY = _random.Next(graphicsDevice.Viewport.Height - height);
        //    var pos = new Vector2(posX, posY);


        //    var r = _random.Next(255);
        //    var g = _random.Next(255);
        //    var b = _random.Next(255);
        //    var a = _random.Next(10, 255);
        //    var color = new Color(r, g, b, a);

        //    //Random randomGen = new Random();
        //    //var names = (System.Drawing.KnownColor[])Enum.GetValues(typeof(System.Drawing.KnownColor));
        //    //var randomColorName = names[randomGen.Next(names.Length)];
        //    //System.Drawing.Color randomColor = System.Drawing.Color.FromKnownColor(randomColorName);
        //    //var color = XnaLibrary.Extensions.FromColor(randomColor);


        //    //var texture = new Texture2D(graphicsDevice, width, height);
        //    //var texture = App.CreateRectangle(graphicsDevice, width, height, color);
        //    var texture = new Texture2D(null, 0, 0);

        //    var s = new Sprite(texture, color, pos);
        //    return s;
        //}



        public T Create<T>()
            where T : ISprite, new ()
        {
            var r = new T();
            var sprite = r as Sprite;
            if (sprite != null)
            {
                sprite.Game = _game;
            }
            return r;
        }

        public T Create<T>(params object[] args)
            where T : ISprite
        {
            var r = (T) System.Activator.CreateInstance(typeof (T), args);
            var sprite = r as Sprite;
            if (sprite != null)
            {
                sprite.Game = _game;
            }
            return r;
        }

        public T Create<T>(SpriteTexture2D spriteTexture)
            where T : ISprite, new()
        {
            var r = Create<T>();
            var sprite = r as Sprite;
            if (sprite != null)
            {
                sprite.SpriteTexture = spriteTexture;
            }
            return r;
        }

        public T Create<T>(SpriteTexture2D spriteTexture, params object[] args)
            where T : ISprite, new()
        {
            var r = Create<T>(args);
            var sprite = r as Sprite;
            if (sprite != null)
            {
                sprite.SpriteTexture = spriteTexture;
            }
            return r;
        }


        public SpriteTexture2D CreateFilledRectangle(Point size, Color colori)
        {
            var spriteTexture = CreateFilledRectangle(size.X, size.Y, colori);
            return spriteTexture;
        }

        public SpriteTexture2D CreateFilledRectangle(int width, int height, Color colori)
        {
            var rectangleTexture = new Texture2D(_graphicsDevice, width, height);       // create the rectangle texture, ,but it will have no color! lets fix that
            var color = new Color[width * height];                                      //set the color to the amount of pixels in the textures
            for (int i = 0; i < color.Length; i++)                                      //loop through all the colors setting them to whatever values we want
                color[i] = colori;
            rectangleTexture.SetData(color);                                            //set the color data on the texture
            
            var spriteTexture = new SpriteTexture2D(rectangleTexture);
            spriteTexture.Color = colori;
            return spriteTexture;
        }

        public SpriteTexture2D CreateFilledRectangleWithBorder(Point size, Color fillColor, Color borderColor, Point borderSize)
        {
            var spriteTexture = CreateFilledRectangleWithBorder(size.X, size.Y, fillColor, borderColor, borderSize);
            return spriteTexture;
        }

        public SpriteTexture2D CreateFilledRectangleWithBorder(int width, int height, Color fillColor, Color borderColor, Point borderSize)
        {
            var spriteTexture = CreateFilledRectangle(width, height, fillColor);
            Texture2D rectangleTexture = spriteTexture.Texture;


            var colorData = new Color[width * height];                 //set the color to the amount of pixels in the textures
            rectangleTexture.GetData(colorData);
            for (int i = 0; i < colorData.Length; i++)
            {
                var row = (int) Math.Floor(i/(double) width);
                var col = i % width;

                //var pixelColor = fillColor;
                if (row < borderSize.Y || 
                    row > (height - borderSize.Y))
                {
                    //pixelColor = borderColor;
                    colorData[i] = borderColor;
                }
                else if (col < borderSize.X ||
                         col > (width - borderSize.X))
                {
                    //pixelColor = borderColor;
                    colorData[i] = borderColor;
                }
                else
                {
                    //pixelColor = Color.Transparent;
                }
                //colorData[i] = pixelColor;                                                      //loop through all the colors setting them to whatever values we want
            }
            rectangleTexture.SetData(colorData);                                                //set the color data on the texture

            //var spriteTexture = new SpriteTexture2D(rectangleTexture);
            //spriteTexture.Color = fillColor;
            return spriteTexture;
        }



        public SpriteTexture2D CreateFilledCircle(int radius, Color fillColor)
        {
            var texture = new Texture2D(_graphicsDevice, radius, radius);
            var colorData = new Color[radius * radius];

            float diam = radius / 2;
            float diamsq = diam * diam;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diam, y - diam);
                    if (pos.LengthSquared() <= diamsq)
                    {
                        colorData[index] = fillColor;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }
            texture.SetData(colorData);


            var spriteTexture = new SpriteTexture2D(texture);
            spriteTexture.Color = fillColor;
            return spriteTexture;
        }


    }
}
