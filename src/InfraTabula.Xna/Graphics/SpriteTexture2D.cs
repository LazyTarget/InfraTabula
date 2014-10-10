using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class SpriteTexture2D : ISpriteTexture
    {
        public SpriteTexture2D(Texture2D texture)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");
            Texture = texture;
            Bounds = texture.Bounds;
            Color = Color.Green;
        }


        public Rectangle Bounds { get; private set; }

        public Texture2D Texture { get; private set; }

        public Color Color { get; set; }



        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position, Color);
        }

    }
}
