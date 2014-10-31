using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class ListScreen : GameScreen
    {
        private List<Item> _items;


        public override void LoadContent()
        {
            base.LoadContent();

            
            var items = Game.Api.GetItems();
            _items = items.ToList();


            Sprites.Clear();
            var spriteFactory = new SpriteFactory(ScreenManager.Game);
            var prevPos = Vector2.Zero;
            foreach (var item in _items)
            {
                var s = spriteFactory.Create<ItemSprite>(item);
                var texture = spriteFactory.CreateFilledRectangle(100, 50, Utils.RandomColor());
                s.SpriteTexture = texture;

                s.Position = prevPos;
                prevPos = new Vector2(prevPos.X + s.Bounds.Width, prevPos.Y);
                Sprites.Add(s);
            }
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            //foreach (var s in _sprites)
            //    s.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            //foreach (var s in _sprites)
            //    s.Draw(ScreenManager.SpriteBatch);
        }


        public override void HandleInput(InputState input)
        {
            base.HandleInput(input);
        }
    }
}
