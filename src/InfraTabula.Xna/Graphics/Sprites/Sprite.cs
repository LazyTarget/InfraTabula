using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class Sprite : ISprite
    {
        public Sprite()
        {

        }

        public Sprite(ISpriteTexture spriteTexture)
            : this()
        {
            if (spriteTexture == null)
                throw new ArgumentNullException("spriteTexture");
            SpriteTexture = spriteTexture;
        }
        

        public ISpriteTexture SpriteTexture { get; internal set; }

        public Vector2 Position { get; set; }

        public Rectangle Bounds
        {
            get { return SpriteTexture.Bounds; }        // todo: set X, Y with Position?
        }


        public virtual void Update(GameTime gameTime)
        {
            SpriteTexture.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            SpriteTexture.Draw(spriteBatch, Position);
        }

    }
}
