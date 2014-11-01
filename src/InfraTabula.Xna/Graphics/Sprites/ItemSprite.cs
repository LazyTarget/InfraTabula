using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class ItemSprite : Sprite
    {
        public ItemSprite(Item item)
        {
            Item = item;
        }

        public Item Item { get; private set; }

        
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void OnClick(MouseDownEventArgs args)
        {
            //Position = new Vector2(Position.X, Position.Y + 2);
            //args.Handled = true;
        }
    }
}
