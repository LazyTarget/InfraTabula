using System;
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

        public ItemSprite FocusedItem
        {
            get { return _focusedItemSprite; }
            private set
            {
                if (_focusedItemSprite != null)
                    _focusedItemSprite.SpriteTexture.TrySetTextureState("default");

                _focusedItemSprite = value;
                _focusedItemSprite.SpriteTexture.TrySetTextureState("focused");
            }
        }



        public override void LoadContent()
        {
            base.LoadContent();

            
            var items = Game.Api.GetItems();
            _items = items.Take(8).ToList();


            Sprites.Clear();
            var spriteFactory = new SpriteFactory(ScreenManager.Game);
            var prevPos = GetRelative(0.05f, 0.30f);
            foreach (var item in _items)
            {
                var s = spriteFactory.Create<ItemSprite>(item);
                var textureSize = GetRelative(0.9f / _items.Count, 0.40f).ToPoint();
                
                var defaultTexture = spriteFactory.CreateFilledRectangle(textureSize, Utils.RandomColor());
                var focusedTexture = spriteFactory.CreateFilledRectangle(textureSize, Color.Orange);
                //s.SpriteTexture = defaultTexture;
                s.SpriteTexture = new MultiStateSpriteTexture2D<string>(new Dictionary<string, ISpriteTexture>
                {
                    { "default", defaultTexture },
                    { "focused", focusedTexture }
                });

                s.OnClicked += (sender, args) =>
                {
                    if (FocusedItem == s)
                        OpenItem(s);
                    else
                        FocusedItem = s;
                };

                s.Position = prevPos;
                prevPos = new Vector2(prevPos.X + s.Bounds.Width, prevPos.Y);
                Sprites.Add(s);
            }
        }


        public override void ExitScreen()
        {
            base.ExitScreen();

            if (!ScreenManager.GetScreens().Any())      // As list screen is the MainScreen, if closed then application should terminate
                Game.Exit();
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
            // todo: support holding down the button for X milliseconds

            KeyStateComparision keyState;
            if (args.StateComparisions.TryGetValue(Keys.Left, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
            {
                SelectLeft();
                args.Handled = true;
            }
            if (args.StateComparisions.TryGetValue(Keys.Right, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
            {
                SelectRight();
                args.Handled = true;
            }
            if (args.StateComparisions.TryGetValue(Keys.Enter, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
            {
                OpenItem(_focusedItemSprite);
                args.Handled = true;
            }

            base.OnKeyboardChange(args);
        }


        public override void OnGamePadChange(GamePadChangeEventArgs args)
        {
            var playerIndexes = Enum.GetValues(typeof(PlayerIndex)).Cast<PlayerIndex>();
            foreach (var playerIndex in playerIndexes)
            {
                var comparison = args.StateComparisions[playerIndex];

                // todo: support holding down the button for X milliseconds

                GamePadButtonStateComparision buttonState;
                if (comparison.ButtonComparisions.TryGetValue(Buttons.DPadLeft, out buttonState) && buttonState.OldState == ButtonState.Released && buttonState.CurrentState == ButtonState.Pressed)
                {
                    SelectLeft();
                    args.Handled = true;
                }
                if (comparison.ButtonComparisions.TryGetValue(Buttons.DPadRight, out buttonState) && buttonState.OldState == ButtonState.Released && buttonState.CurrentState == ButtonState.Pressed)
                {
                    SelectRight();
                    args.Handled = true;
                }
                if (comparison.ButtonComparisions.TryGetValue(Buttons.A, out buttonState) && buttonState.OldState == ButtonState.Released && buttonState.CurrentState == ButtonState.Pressed)
                {
                    OpenItem(_focusedItemSprite);
                    args.Handled = true;
                }
            }

            base.OnGamePadChange(args);
        }


        private void SelectLeft()
        {
            var index = 0;
            if(_focusedItemSprite != null)
                index = _items.IndexOf(_focusedItemSprite.Item) - 1;
            SelectIndex(index);
        }

        private void SelectRight()
        {
            var index = 0;
            if (_focusedItemSprite != null)
                index = _items.IndexOf(_focusedItemSprite.Item) + 1;
            SelectIndex(index);
        }

        private void SelectIndex(int index)
        {
            var oldIndex = -1;
            if (_focusedItemSprite != null)
                oldIndex = _items.IndexOf(_focusedItemSprite.Item);
            if (oldIndex != index)
            {
                if (index >= 0 && index < _items.Count)
                {
                    var newItem = _items.ElementAt(index);
                    var itemSprite = Sprites.OfType<ItemSprite>().SingleOrDefault(x => x.Item == newItem);
                    FocusedItem = itemSprite;
                }
            }
        }


        private void OpenItem(ItemSprite itemSprite)
        {
            var itemScreen = new ItemScreen(itemSprite);
            ScreenManager.AddScreen(itemScreen);
        }

    }
}
