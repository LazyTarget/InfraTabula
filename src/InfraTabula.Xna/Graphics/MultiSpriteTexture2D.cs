using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class MultiSpriteTexture2D : ISpriteTexture
    {
        public MultiSpriteTexture2D(IEnumerable<ISpriteTexture> spriteTextures)
        {
            SpriteTextures = spriteTextures.ToList();
            if (spriteTextures == null)
                throw new ArgumentNullException("spriteTextures");
            if (!SpriteTextures.Any())
                throw new ArgumentException("No sprite textures", "spriteTextures");
        }


        public Color Color { get; set; }

        public SpriteFont SpriteFont { get; private set; }

        public Rectangle Bounds { get { return SpriteTextures.First().Bounds; } }       // todo: improve?

        public List<ISpriteTexture> SpriteTextures { get; private set; }


        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            foreach (var spriteTexture in SpriteTextures)
            {
                // todo: verify positions
                spriteTexture.Draw(spriteBatch, position);
            }
        }

    }
}
