using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public interface ISpriteTexture
    {
        Rectangle Bounds { get; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}
