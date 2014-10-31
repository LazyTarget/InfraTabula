using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class ListScreen : GameScreen
    {
        private List<Item> _items;
        private ItemSprite _focusedItemSprite;



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
                s.OnClicked += (sender, args) =>
                {
                    _focusedItemSprite = s;
                };

                s.Position = prevPos;
                prevPos = new Vector2(prevPos.X + s.Bounds.Width, prevPos.Y);
                Sprites.Add(s);
            }
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        public override void HandleInput(InputState input)
        {
            base.HandleInput(input);
        }


        public override void OnKeyboardChange(KeyboardChangeEventArgs args)
        {
            if (_focusedItemSprite != null)
            {
                var index = _items.IndexOf(_focusedItemSprite.Item);
                var oldIndex = index;

                KeyStateComparision keyState;
                if (args.StateComparisions.TryGetValue(Keys.Left, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
                {
                    index--;
                }
                else if (args.StateComparisions.TryGetValue(Keys.Right, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
                {
                    index++;
                }
                else if (args.StateComparisions.TryGetValue(Keys.Enter, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
                {
                    Game.Debug("[Enter] " + _focusedItemSprite.Item);
                }


                if (oldIndex != index)
                    if (index >= 0 && index < _items.Count)
                    {
                        var newItem = _items.ElementAt(index);
                        var itemSprite = Sprites.OfType<ItemSprite>().SingleOrDefault(x => x.Item == newItem);
                        _focusedItemSprite = itemSprite;
                    }
            }

            base.OnKeyboardChange(args);
        }
    }
}
