using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XnaLibrary.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace InfraTabula.Xna
{
    public class ItemScreen : GameScreen
    {
        private SimpleBrowserForm _browserForm;
        private readonly ItemSprite _itemSprite;

        public ItemScreen(ItemSprite itemSprite)
        {
            _itemSprite = itemSprite;
            IsPopup = true;
        }

        
        public override void LoadContent()
        {
            base.LoadContent();

            //var browserForm = new TabbedBrowserForm();
            _browserForm = new SimpleBrowserForm(_itemSprite.Item.Url);
            _browserForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            _browserForm.MainMenuStrip.Hide();
            // todo: fullscreen

            //_browserForm.LoadUrl(_itemSprite.Item.Url);
            _browserForm.Show();
            _browserForm.FormClosed += (sender, args) => ExitScreen();
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


            var scrollStrength = 40;
            //if (args.StateComparisions.TryGetValue(Keys.Up, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
            //{
            //    var oldScrollPos = _browserForm.GetVerticalScrollPosition();
            //    var scrollPos = Math.Max(0, oldScrollPos - scrollStrength);
            //    if (oldScrollPos != scrollPos)
            //        _browserForm.SetVerticalScrollPosition(scrollPos);
            //    args.Handled = true;
            //}
            //if (args.StateComparisions.TryGetValue(Keys.Down, out keyState) && keyState.OldState == KeyState.Up && keyState.CurrentState == KeyState.Down)
            //{
            //    var scrollPos = _browserForm.GetVerticalScrollPosition() + scrollStrength;
            //    _browserForm.SetVerticalScrollPosition(scrollPos);
            //    args.Handled = true;
            //}
            
            base.OnKeyboardChange(args);
        }


    }
}
