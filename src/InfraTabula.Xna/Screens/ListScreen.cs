﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class ListScreen : GameScreen
    {
        private List<Item> _items;
        private ItemSprite _focusedItemSprite;
        private ItemSprite _hoveredItemSprite;

        public ItemSprite FocusedItem
        {
            get { return _focusedItemSprite; }
            private set
            {
                if (_focusedItemSprite != null)
                    _focusedItemSprite.SpriteTexture.TrySetTextureState("default");

                _focusedItemSprite = value;
                if (_focusedItemSprite != null)
                    _focusedItemSprite.SpriteTexture.TrySetTextureState("focused");
            }
        }

        public ItemSprite HoveredItem
        {
            get { return _hoveredItemSprite; }
            private set
            {
                if (_hoveredItemSprite != null)
                {
                    if(_hoveredItemSprite == _focusedItemSprite)
                        _hoveredItemSprite.SpriteTexture.TrySetTextureState("focused");
                    else
                        _hoveredItemSprite.SpriteTexture.TrySetTextureState("default");
                }

                _hoveredItemSprite = value;
                if (_hoveredItemSprite != null)
                {
                    if (_hoveredItemSprite == _focusedItemSprite)
                        _hoveredItemSprite.SpriteTexture.TrySetTextureState("focused-hover");
                    else
                        _hoveredItemSprite.SpriteTexture.TrySetTextureState("hover");
                }
            }
        }


        private int itemsPage = 1;
        private int itemsPageSize = 8;
        private SpriteFont _itemFont;

        public override void LoadContent()
        {
            base.LoadContent();

            
            _itemFont = Game.Content.Load<SpriteFont>("GameFont");
            
            LoadPage();
        }


        private void LoadPage()
        {
            var request = new GetItemsRequest
            {
                Page = itemsPage,
                PageSize = itemsPageSize,
            };
            var items = Game.Api.GetItems(request).ToList();
            _items = items.Take(itemsPageSize).ToList();


            //var first = _items.First();

            //var res = Game.Api.RemoveTags(first.ID, "temp");

            //var it2 = Game.Api.GetItems(request).ToList();

            //foreach (var item in it2)
            //{
            //    var tags = item.Tags.ToList();
            //    var tStr = string.Join(",", tags);
            //}
            
            RenderItems();
        }


        private void RenderItems()
        {
            Sprites.RemoveAll(x => x is ItemSprite);

            var spriteFactory = new SpriteFactory(ScreenManager.Game);
            var prevPos = GetRelative(0.05f, 0.30f);
            foreach (var item in _items)
            {
                var textSprite = new TextSpriteTexture2D(_itemFont);
                var s = spriteFactory.Create<ItemSprite>(item, textSprite);
                var textureSize = GetRelative(0.9f / _items.Count, 0.40f);
                //var borderSize = textureSize.GetRelative(0.1f / _items.Count, 0.1f).ToPoint();
                var borderSize = new Point(5, 5);

                var defaultTexture = spriteFactory.CreateFilledRectangle(textureSize.ToPoint(), Utils.RandomColor());
                var focusedTexture = spriteFactory.CreateFilledRectangleWithBorder(textureSize.ToPoint(), Color.Orange, defaultTexture.Color, borderSize.Inflate(2));
                //var onHoverTexture = spriteFactory.CreateFilledRectangle(textureSize, Color.LightYellow);
                var onHoverTexture = spriteFactory.CreateFilledRectangleWithBorder(textureSize.ToPoint(), Color.LightYellow, defaultTexture.Color, borderSize);
                var onFocusedHoverTexture = spriteFactory.CreateFilledRectangleWithBorder(textureSize.ToPoint(), Color.LightYellow, Color.Orange, borderSize);
                //s.SpriteTexture = defaultTexture;
                s.SpriteTexture = new MultiStateSpriteTexture2D<string>(new Dictionary<string, ISpriteTexture>
                {
                    { "default", defaultTexture },
                    { "focused", focusedTexture },
                    { "hover", onHoverTexture },
                    { "focused-hover", onFocusedHoverTexture },
                });
                //s.SpriteTexture = new MultiStateSpriteTexture2D<string>(new Dictionary<string, ISpriteTexture>
                //{
                //    { "default", new TextSpriteTexture2D(itemFont) { Text = item.Title } },
                //    { "focused", new TextSpriteTexture2D(itemFont) { Text = item.Title } },
                //    { "hover", new TextSpriteTexture2D(itemFont) { Text = item.Title } },
                //    { "focused-hover", new TextSpriteTexture2D(itemFont) { Text = item.Title } },
                //});


                s.OnClicked += Item_OnClick;
                s.OnMouseEntered += Item_OnMouseEntered;
                s.OnMouseLeft += Item_OnMouseLeft;

                s.Position = prevPos;
                prevPos = new Vector2(prevPos.X + s.Bounds.Width, prevPos.Y);
                Sprites.Add(s);
            }
        }


        public override void ExitScreen()
        {
            base.ExitScreen();

            //if (!ScreenManager.GetScreens().Any())      // As list screen is the MainScreen, if closed then application should terminate
                //Game.Exit();
            ScreenManager.Exit();
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

        public override void OnMouseMove(MouseMoveEventArgs args)
        {
            //var current = args.PositionComparision.CurrentPosition;
            //var point = new Point(current.X, current.Y);
            //var sprite = ScreenManager.GetSpriteAt(point);
            //if (sprite == null)
            //{
            //    HoveredItem = null;
            //    return;
            //}

            //var itemSprite = sprite as ItemSprite;
            //if (itemSprite != null)
            //{
            //    HoveredItem = itemSprite;
            //}
            base.OnMouseMove(args);
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
                    if (_hoveredItemSprite != null)
                        OpenItem(_hoveredItemSprite);
                    else
                        OpenItem(_focusedItemSprite);
                    args.Handled = true;
                }
            }

            base.OnGamePadChange(args);
        }



        private void Item_OnClick(object sender, MouseDownEventArgs mouseDownEventArgs)
        {
            var s = (ItemSprite)sender;
            if (FocusedItem == s ||
                HoveredItem == s)
                OpenItem(s);
            else
                FocusedItem = s;
        }

        private void Item_OnMouseEntered(object sender, MouseMoveEventArgs mouseMoveEventArgs)
        {
            HoveredItem = (ItemSprite)sender;
        }

        private void Item_OnMouseLeft(object sender, MouseMoveEventArgs mouseMoveEventArgs)
        {
            HoveredItem = null;
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
                if (index == -1 && itemsPage > 1)
                {
                    itemsPage--;
                    LoadPage();
                    index = _items.Count - 1;
                }
                else if (index == _items.Count)
                {
                    itemsPage++;
                    LoadPage();
                    index = 0;
                }

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
            if (itemSprite == null)
                return;

            var itemScreen = new WebScreen(itemSprite);
            ScreenManager.AddScreen(itemScreen);
        }

    }
}
