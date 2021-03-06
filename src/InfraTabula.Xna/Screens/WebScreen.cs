﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;
using XnaLibrary.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace InfraTabula.Xna
{
    public class WebScreen : GameScreen
    {
        private int _scrollStrength = 250;
        private SimpleBrowserForm _browserForm;
        private readonly ItemSprite _itemSprite;

        public WebScreen(ItemSprite itemSprite)
        {
            _itemSprite = itemSprite;
            IsPopup = true;
        }

        
        public override void LoadContent()
        {
            base.LoadContent();

            //_browserForm = new TabbedBrowserForm(_itemSprite.Item.Url);
            _browserForm = new SimpleBrowserForm(_itemSprite.Item.Url);
            _browserForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            // todo: fullscreen
            // todo: multi tab? open clicks inside tabs?
            // todo: add to "InfraTabula" button if user has navigated
            // todo: mark as read button (shortcut for controller)

            //_browserForm.LoadUrl(_itemSprite.Item.Url);
            _browserForm.Show();
            _browserForm.FormClosed += (sender, args) => ExitScreen();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            _browserForm.Close();
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
            KeyStateComparision keyState;
            if (args.StateComparisions.TryGetValue(Keys.Escape, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
            {
                _browserForm.Close();
                args.Handled = true;
                base.OnKeyboardChange(args);
                return;
            }


            //if (args.StateComparisions.TryGetValue(Keys.Up, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
            //{
            //    var oldScrollPos = _browserForm.GetVerticalScrollPosition();
            //    var scrollPos = Math.Max(0, oldScrollPos - _scrollStrength);
            //    if (oldScrollPos != scrollPos)
            //        _browserForm.SetVerticalScrollPosition(scrollPos);
            //    args.Handled = true;
            //}
            //if (args.StateComparisions.TryGetValue(Keys.Down, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
            //{
            //    var scrollPos = _browserForm.GetVerticalScrollPosition() + _scrollStrength;
            //    _browserForm.SetVerticalScrollPosition(scrollPos);
            //    args.Handled = true;
            //}

            base.OnKeyboardChange(args);
        }



        public override void OnGamePadChange(GamePadChangeEventArgs args)
        {
            var playerIndexes = Enum.GetValues(typeof(PlayerIndex)).Cast<PlayerIndex>();
            foreach (var playerIndex in playerIndexes)
            {
                var comparison = args.StateComparisions[playerIndex];
                
                GamePadButtonStateComparision buttonState;
                if (comparison.ButtonComparisions.TryGetValue(Buttons.DPadUp, out buttonState) && buttonState.OldState == ButtonState.Released && buttonState.CurrentState == ButtonState.Pressed)
                {
                    var oldScrollPos = _browserForm.GetVerticalScrollPosition();
                    var scrollPos = Math.Max(0, oldScrollPos - _scrollStrength);
                    if (oldScrollPos != scrollPos)
                        _browserForm.SetVerticalScrollPosition(scrollPos);
                    args.Handled = true;
                }
                if (comparison.ButtonComparisions.TryGetValue(Buttons.DPadDown, out buttonState) && buttonState.OldState == ButtonState.Released && buttonState.CurrentState == ButtonState.Pressed)
                {
                    var scrollPos = _browserForm.GetVerticalScrollPosition() + _scrollStrength;
                    _browserForm.SetVerticalScrollPosition(scrollPos);
                    args.Handled = true;
                    this.Log().Info("DPadDown Time unchanged: {0}", buttonState.TimeUnchanged);
                }


                if (comparison.ButtonComparisions.TryGetValue(Buttons.A, out buttonState) && buttonState.Changed)
                {
                    var cursorPos = new Vector2(
                        Game.InputState.CurrentState.Mouse.X,
                        Game.InputState.CurrentState.Mouse.Y);
                    // browser form offset

                    if (buttonState.CurrentState == ButtonState.Pressed && buttonState.OldState == ButtonState.Released)
                    {
                        Game.InvokeMouseDown(MouseButtons.Left);
                        args.Handled = true;
                    }
                    else if (buttonState.CurrentState == ButtonState.Released &&
                             buttonState.OldState == ButtonState.Pressed)
                    {
                        if (Game.InputState.OldState.Mouse.LeftButton == ButtonState.Pressed ||
                            Game.InputState.CurrentState.Mouse.LeftButton == ButtonState.Pressed)
                        {
                            Game.InvokeMouseUp(MouseButtons.Left);
                            //_browserForm.InvokeClick(cursorPos);
                            args.Handled = true;
                        }

                    }
                }
            }

            base.OnGamePadChange(args);
        }
    }
}
