using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class TextSpriteTexture2D : ISpriteTexture
    {
        private string _text;

        public TextSpriteTexture2D(SpriteFont font)
        {
            if (font == null)
                throw new ArgumentNullException("font");
            SpriteFont = font;
            Color = Color.Black;
        }


        public string Text
        {
            get { return _text ?? ""; }
            set { _text = value; }
        }

        public Color Color { get; set; }

        public SpriteFont SpriteFont { get; private set; }

        public Rectangle Bounds
        {
            get
            {
                var size = SpriteFont.MeasureString(Text);
                return new Rectangle(0, 0, (int) size.X, (int) size.Y);
            }
        }       // todo: verify correct?


        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.DrawString(SpriteFont, Text, position, Color);
        }

    }
}
