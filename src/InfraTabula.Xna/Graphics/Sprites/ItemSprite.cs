using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InfraTabula.Xna
{
    public class ItemSprite : Sprite
    {
        public ItemSprite(Item item, TextSpriteTexture2D textSprite)
        {
            Item = item;
            TextSprite = textSprite;
        }

        public Item Item { get; private set; }

        public TextSpriteTexture2D TextSprite { get; private set; }
        
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            TextSprite.Text = Item.Title;
            TextSprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            var textPosition = Position.Copy();
            textPosition.Y += SpriteTexture.Bounds.Height / 2 - TextSprite.Bounds.Height / 2;
            TextSprite.Draw(spriteBatch, textPosition);
        }

        protected override void OnClick(MouseDownEventArgs args)
        {
            //Position = new Vector2(Position.X, Position.Y + 2);
            //args.Handled = true;
        }
    }
}
