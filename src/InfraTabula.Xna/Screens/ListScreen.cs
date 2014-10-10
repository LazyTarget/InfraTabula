using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class ListScreen : GameScreen
    {
        private List<PocketAPI.Item> _items;
        private List<ISprite> _sprites; 


        public override void LoadContent()
        {
            base.LoadContent();

            var items = API.Instance.GetItems();
            _items = items.ToList();

            _sprites = new List<ISprite>();
            var spriteFactory = new SpriteFactory(ScreenManager.Game);
            var prevPos = Vector2.Zero;
            foreach (var item in _items)
            {
                var s = spriteFactory.Create<ItemSprite>(item);
                var texture = spriteFactory.CreateFilledRectangle(100, 50, Utils.RandomColor());
                s.SpriteTexture = texture;

                s.Position = prevPos;
                prevPos = new Vector2(prevPos.X + s.Bounds.Width, prevPos.Y);
                _sprites.Add(s);
            }
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            foreach (var s in _sprites)
                s.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (var s in _sprites)
                s.Draw(ScreenManager.SpriteBatch);
        }


        public override void HandleInput(InputState input)
        {
            base.HandleInput(input);
        }
    }
}
