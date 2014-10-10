using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class ItemSprite : Sprite
    {
        public ItemSprite(PocketAPI.Item item)
        {
            Item = item;
        }

        public PocketAPI.Item Item { get; private set; }

        
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
