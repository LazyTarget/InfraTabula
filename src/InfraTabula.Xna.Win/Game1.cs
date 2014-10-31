﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaLibrary.Input;

namespace InfraTabula.Xna.Win
{
    public class Game1 : GameBase
    {
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }


        //private readonly Color _backColor = XnaLibrary.Extensions.FromColor(System.Drawing.SystemColors.Control);
        private readonly Color _backColor = Color.CornflowerBlue;
        private readonly GraphicsDeviceManager _graphics;
        //private readonly List<ISprite> _sprites = new List<ISprite>();
        private int _velocity = 5;




        private void Debug(string message)
        {
            message = string.Format("GameInstance.{0}", message);
            Program.Debug(message);
        }





        protected override void Initialize()
        {
            var listScreen = new ListScreen();
            ScreenManager.AddScreen(listScreen);
            
            base.Initialize();
        }


        private void KeyboardChange_Callback(IEnumerable<KeyStateComparision> obj)
        {
            foreach (var s in obj)
            {
                Debug(string.Format("KeyboardChange_Callback() Key:{0}, Old:{1}, New:{2}", s.Key, s.OldState, s.CurrentState));
            }
        }

        private void KeyboardKeyDown_Enter_Callback(KeyStateComparision state)
        {
            var keyboardState = state.GetKeyboardState();
            Debug(string.Format("KeyboardKeyDown_Enter_Callback()"));
        }


        private void MouseLeftUp_Callback(MouseButtonStateComparision state)
        {
            var mouseState = state.GetMouseState();
            Debug(string.Format("MouseLeftUp_Callback() X:{0} Y:{1}", mouseState.New.X, mouseState.New.Y));
        }

        private void MouseLeftDown_Callback(MouseButtonStateComparision state)
        {
            var mouseState = state.GetMouseState();
            Debug(string.Format("MouseLeftDown_Callback() X:{0} Y:{1}", mouseState.New.X, mouseState.New.Y));
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //var state = base.GetKeyboardState();
            //if (state.IsKeyDown(Keys.Up))
            //    GoUp();
            //if (state.IsKeyDown(Keys.Right))
            //    GoRight();
            //if (state.IsKeyDown(Keys.Down))
            //    GoDown();
            //if (state.IsKeyDown(Keys.Left))
            //    GoLeft();


            //var state2 = base.GetGamePadState(PlayerIndex.One);
            //if (state2.IsButtonDown(Buttons.DPadUp))
            //    GoUp();
            //else if (state2.IsButtonDown(Buttons.DPadRight))
            //    GoRight();
            //else if (state2.IsButtonDown(Buttons.DPadDown))
            //    GoDown();
            //else if (state2.IsButtonDown(Buttons.DPadLeft))
            //    GoLeft();
        }


        #region Drawing

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(_backColor);

            //_spriteBatch.Begin();

            //foreach (var s in _sprites)
            //    s.Draw(_spriteBatch);

            //_spriteBatch.End();


            base.Draw(gameTime);
        }

        #endregion



        #region Input management


        //private void _OnMouseDown(object sender, MouseButtonEventArgs args)
        //{

        //}
        //private void _OnMouseUp(object sender, MouseButtonEventArgs args)
        //{

        //}


        //private void _OnKeyDown(object sender, KeyEventArgs args)
        //{
        //    var stateTracker = (KeyboardStateTracker) sender;
        //    var state = (KeyboardState) stateTracker.GetCurrentState();
        //    if (state.IsKeyDown(Keys.LeftAlt) && state.IsKeyDown(Keys.Enter))
        //    {
        //        _graphics.ToggleFullScreen();
        //        Debug("IsFullScreen " + _graphics.IsFullScreen);
        //        //Debug("Viewport: " + Newtonsoft.Json.JsonConvert.SerializeObject(_graphics.GraphicsDevice.Viewport));
        //    }

        //    //if (args.Key == Keys.Up)
        //    //    GoUp();
        //    //else if (args.Key == Keys.Right)
        //    //    GoRight();
        //    //else if (args.Key == Keys.Down)
        //    //    GoDown();
        //    //else if (args.Key == Keys.Left)
        //    //    GoLeft();
        //}

        //private void _OnKeyUp(object sender, KeyEventArgs args)
        //{

        //}


        //private void _OnButtonDown(object sender, GamePadButtonEventArgs args)
        //{
        //    if (args.Button.HasFlag(Buttons.DPadUp))
        //        GoUp();
        //    else if (args.Button.HasFlag(Buttons.DPadRight))
        //        GoRight();
        //    else if (args.Button.HasFlag(Buttons.DPadDown))
        //        GoDown();
        //    else if (args.Button.HasFlag(Buttons.DPadLeft))
        //        GoLeft();
        //}

        //private void _OnButtonUp(object sender, GamePadButtonEventArgs args)
        //{

        //}


        #endregion



        //private void GoUp()
        //{
        //    Debug("GoUp()");
        //    foreach (var s in _sprites)
        //    {
        //        s.Position = new Vector2(s.Position.X, Math.Max(0, s.Position.Y - _velocity));
        //    }
        //}

        //private void GoRight()
        //{
        //    Debug("GoRight()");
        //    foreach (var s in _sprites)
        //    {
        //        s.Position = new Vector2(Math.Min(GraphicsDevice.Viewport.Width - s.Bounds.Width, s.Position.X + _velocity),
        //            s.Position.Y);
        //    }
        //}

        //private void GoDown()
        //{
        //    Debug("GoDown()");
        //    foreach (var s in _sprites)
        //    {
        //        s.Position = new Vector2(s.Position.X,
        //            Math.Min(GraphicsDevice.Viewport.Height - s.Bounds.Height, s.Position.Y + _velocity));
        //    }
        //}

        //private void GoLeft()
        //{
        //    Debug("GoLeft()");
        //    foreach (var s in _sprites)
        //    {
        //        s.Position = new Vector2(Math.Max(0, s.Position.X - _velocity), s.Position.Y);
        //    }
        //}



    }
}
