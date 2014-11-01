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



        public SpriteTexture2D CreateFilledRectangle(int width, int height, Color colori)
        {
            Texture2D rectangleTexture = new Texture2D(_graphicsDevice, width, height);     // create the rectangle texture, ,but it will have no color! lets fix that
            Color[] color = new Color[width * height];                                      //set the color to the amount of pixels in the textures
            for (int i = 0; i < color.Length; i++)                                          //loop through all the colors setting them to whatever values we want
                color[i] = colori;
            rectangleTexture.SetData(color);                                                //set the color data on the texture
            
            var spriteTexture = new SpriteTexture2D(rectangleTexture);
            spriteTexture.Color = colori;
            return spriteTexture;
        }

    }
}
