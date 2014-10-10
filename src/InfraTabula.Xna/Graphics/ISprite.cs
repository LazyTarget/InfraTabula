using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public interface ISprite
    {
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        //ISpriteTexture SpriteTexture { get; }

        Rectangle Bounds { get; }

        Vector2 Position { get; set; }

    }
}
