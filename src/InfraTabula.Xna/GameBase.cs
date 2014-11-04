using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class GameBase : XnaAppBase, IEventBoundable     //Microsoft.Xna.Framework.Game
    {
        private readonly List<IEvent> _events = new List<IEvent>();
        private Matrix _spriteScale;

        private Sprite _lastHovered;
        private Vector2 mouseVelocity;
        private Vector2 mouseNewVelocity;
        private const float MouseSpeed = 3;
        private const float StickDown_MouseSpeed = 10;


        public GameBase()
        {
            ScreenManager = new ScreenManager(this);
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            Window.ClientSizeChanged += Window_OnClientSizeChanged;
        }


        public API Api
        {
            get
            {
                var api = Services.GetService(typeof (API)) as API;
                return api;
            }
        }

        protected ScreenManager ScreenManager { get; private set; }
        
        protected GraphicsDeviceManager GraphicsDeviceManager { get; private set; }



        public new InputStateManager InputState
        {
            get { return base.InputState; }
        }


        protected override void Initialize()
        {
            Services.AddService(typeof(InputStateManager), InputState);

            Components.Add(ScreenManager);

            var listScreen = new ListScreen();
            ScreenManager.AddScreen(listScreen);
            

            EventExtensions.Bind(new MouseLeftDownEvent(MouseLeftDown_Callback), this);
            EventExtensions.Bind(new MouseLeftUpEvent(MouseLeftUp_Callback), this);
            EventExtensions.Bind(new MouseMoveEvent(MouseMove_Callback), this);
            EventExtensions.Bind(new KeyboardChangeEvent(KeyboardChange_Callback), this);
            EventExtensions.Bind(new KeyboardKeyDownEvent(KeyboardKeyDown_Enter_Callback, Keys.Enter), this);
            EventExtensions.Bind(new GamePadChangeEvent(GamePadChange_Callback), this);
            
            base.Initialize();
        }
        


        protected override void LoadContent()
        {
            var defaultscale = (float)GraphicsDeviceManager.DefaultBackBufferWidth /
                               (float)GraphicsDeviceManager.DefaultBackBufferHeight;
            var scale = (float)GraphicsDeviceManager.PreferredBackBufferWidth /
                        (float)GraphicsDeviceManager.PreferredBackBufferHeight;
            var screenscale = (float)GraphicsDevice.Viewport.Width /
                              (float)GraphicsDevice.Viewport.Height;
            _spriteScale = Matrix.CreateScale(screenscale, screenscale, 1);


            base.LoadContent();


            //var handle = Window.Handle;
            //var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(handle);
            //var b = form.Focus();
            //System.Diagnostics.Debug.WriteLine("Form got focus: " + b);
        }


        protected override void UnloadContent()
        {
            base.UnloadContent();

            foreach (var evt in _events)
                evt.Unbind();
        }



        public void BindEvent(IEvent evt)
        {
            if (_events.Contains(evt))
                throw new InvalidOperationException("Event already bound");
            _events.Add(evt);
        }


        public void UnbindEvent(IEvent evt)
        {
            var removed = _events.Remove(evt);
            //if (!removed)
            //    throw new InvalidOperationException("Event was not bound");
        }




        protected override void Update(GameTime gameTime)
        {
            mouseVelocity = Vector2.Zero;
            mouseNewVelocity = Vector2.Zero;

            base.Update(gameTime);      // HandleInput is called here

            if (mouseNewVelocity != Vector2.Zero)
            {
                var mouseState = Mouse.GetState();
                var oldPos = new Vector2(mouseState.X, mouseState.Y);
                var newPos = oldPos;


                //mouseVelocity += mouseNewVelocity;
                mouseVelocity = mouseNewVelocity;


                newPos += mouseVelocity;
                //newPos.X = MathHelper.Clamp(newPos.X, 0, GraphicsDevice.DisplayMode.Width);
                //newPos.Y = MathHelper.Clamp(newPos.Y, 0, GraphicsDevice.DisplayMode.Height);
                Mouse.SetPosition((int)newPos.X, (int)newPos.Y);
                this.Log().Info("New mouse pos = {0}, {1}", newPos.X, newPos.Y);

                mouseVelocity = mouseNewVelocity;
            }
            else
                mouseVelocity = Vector2.Zero;
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        protected override void HandleInput(GameTime gameTime, InputStateManager input)
        {
            base.HandleInput(gameTime, input);

            foreach (var evt in _events)
                evt.Update();
        }



        protected internal virtual void Debug(string message)
        {
            message = string.Format("GameBase.{0}", message);
            //System.Diagnostics.Debug.WriteLine(message);
            this.Log().Info(message);
        }



        private void MouseMove_Callback(MouseMoveEventArgs state)
        {
            var current = state.PositionComparision.CurrentPosition;
            var size = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            var percentage = new Vector2(current.X / size.X, current.Y / size.Y);

            Window.Title = string.Format("Mouse  X:{0} ({4:P2}), Y:{1} ({5:P2})   -   Window Size  X: {2}, Y:{3}", 
                current.X, current.Y,
                size.X, size.Y,
                percentage.X, percentage.Y);


            var sprite = ScreenManager.GetSpriteAt(current);
            if (sprite != null)
            {
                var baseSprite = sprite as Sprite;
                if (baseSprite != null)
                {
                    if (_lastHovered != null)
                        _lastHovered._InvokeMouseLeave(state);

                    baseSprite._InvokeMouseEnter(state);
                    _lastHovered = baseSprite;
                }
            }
            else if (_lastHovered != null)
            {
                _lastHovered._InvokeMouseLeave(state);
            }

            ScreenManager._InvokeMouseMove(state);
        }

        private void MouseLeftDown_Callback(MouseButtonStateComparision state)
        {
            var mouseState = state.GetMouseState();
            Debug(string.Format("MouseLeftDown_Callback() X:{0} Y:{1}", mouseState.New.X, mouseState.New.Y));
            var current = new Point(mouseState.New.X, mouseState.New.Y);
            var sprite = ScreenManager.GetSpriteAt(current);
            if (sprite == null)
                return;

            var baseSprite = sprite as Sprite;
            if (baseSprite != null)
            {
                var args = new MouseDownEventArgs
                {
                    StateComparision = state,
                };
                baseSprite._InvokeClick(args);
            }
        }

        private void MouseLeftUp_Callback(MouseButtonStateComparision state)
        {
            var mouseState = state.GetMouseState();
            Debug(string.Format("MouseLeftUp_Callback() X:{0} Y:{1}", mouseState.New.X, mouseState.New.Y));
        }

        

        private void KeyboardChange_Callback(KeyboardChangeEventArgs args)
        {
            foreach (var s in args.StateComparisions.Select(x => x.Value))
            {
                if(s.Key == Keys.F11 && s.OldState == KeyState.Up && s.CurrentState == KeyState.Down)
                    GraphicsDeviceManager.ToggleFullScreen();

                Debug(string.Format("KeyboardChange_Callback() Key:{0}, Old:{1}, New:{2}", s.Key, s.OldState, s.CurrentState));
            }


            //KeyStateComparision keyState;
            //if (args.StateComparisions.TryGetValue(Keys.LeftAlt, out keyState) && keyState.CurrentState == KeyState.Down)
            //{
            //    if (args.StateComparisions.TryGetValue(Keys.F4, out keyState) && keyState.CurrentState == KeyState.Down)
            //        Exit();
            //}

            ScreenManager._InvokeKeyboardChange(args);
        }
        
        private void KeyboardKeyDown_Enter_Callback(KeyStateComparision state)
        {
            var keyboardState = state.GetKeyboardState();
            Debug(string.Format("KeyboardKeyDown_Enter_Callback()"));
        }


        private void GamePadChange_Callback(GamePadChangeEventArgs state)
        {
            foreach (var pair in state.StateComparisions)
            {
                var playerIndex = pair.Key;
                var comparison = state.StateComparisions[playerIndex];
                foreach (var s in comparison.ButtonComparisions.Select(x => x.Value))
                {
                    if (!s.Changed)
                        continue;
                    Debug(string.Format("GamePadChange_Callback() Player:{3}, Button:{0}, Old:{1}, New:{2}", s.Button, s.OldState, s.CurrentState, s.Player));


                    if (s.Button == Buttons.LeftStick)
                    {
                        AppendNewMouseVelocity(s, Vector2.Zero);
                    }

                }
            }
            
            ScreenManager._InvokeGamePadChange(state);
        }





        private void AppendNewMouseVelocity(GamePadButtonStateComparision state, Vector2 buttonVelocity)
        {
            var gamePad = InputState.CurrentState.GamePad[state.Player];
            

            var stickDown = false;
            Vector2 velocity = Vector2.Zero;
            if (state.Button == Buttons.LeftStick)
            {
                velocity = gamePad.ThumbSticks.Left;
                stickDown = gamePad.IsButtonDown(state.Button);
            }
            //else if (state.Button == Buttons.RightStick)
            //{
            //    velocity = gamePad.ThumbSticks.Right;
            //    stickDown = gamePad.IsButtonDown(state.Button);
            //}
            else
                velocity = buttonVelocity;

            if (stickDown)
                velocity *= StickDown_MouseSpeed;
            else
                velocity *= MouseSpeed;

            Debug(string.Format("MoveMouse \t\t{0}", XnaLibrary.Extensions.VectorToString(velocity)));

            if (velocity == Vector2.Zero)
                mouseNewVelocity = Vector2.Zero;
            else
            {
                mouseNewVelocity.X += velocity.X;
                mouseNewVelocity.Y -= velocity.Y;
            }
        }



        private void Window_OnClientSizeChanged(object sender, EventArgs eventArgs)
        {
            //var defaultscale = (float) GraphicsDeviceManager.DefaultBackBufferWidth/
            //                   (float) GraphicsDeviceManager.DefaultBackBufferHeight;
            //var scale = (float) GraphicsDeviceManager.PreferredBackBufferWidth/
            //            (float) GraphicsDeviceManager.PreferredBackBufferHeight;
            //var screenscale = (float) GraphicsDevice.Viewport.Width/
            //                  (float) GraphicsDevice.Viewport.Height;
            //var matrixScale = Matrix.CreateScale(screenscale, screenscale, 1);
            
            GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.Viewport.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.Viewport.Height;
            GraphicsDeviceManager.ApplyChanges();
        }


    }
}
