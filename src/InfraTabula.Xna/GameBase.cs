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
        private MouseLeftDownEvent _mouseLeftDownEvent;
        private MouseLeftUpEvent _mouseLeftUpEvent;
        private KeyboardChangeEvent _keyboardChangeEvent;
        private KeyboardKeyDownEvent _keyboardEnterDownEvent;


        public GameBase()
        {
            ScreenManager = new ScreenManager(this);
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



        public new InputStateManager InputState
        {
            get { return base.InputState; }
        }


        protected override void Initialize()
        {
            Services.AddService(typeof(InputStateManager), InputState);

            Components.Add(ScreenManager);

            _mouseLeftDownEvent = EventExtensions.Bind(new MouseLeftDownEvent(MouseLeftDown_Callback), this);
            _mouseLeftUpEvent = EventExtensions.Bind(new MouseLeftUpEvent(MouseLeftUp_Callback), this);
            _keyboardChangeEvent = EventExtensions.Bind(new KeyboardChangeEvent(KeyboardChange_Callback), this);
            _keyboardEnterDownEvent = EventExtensions.Bind(new KeyboardKeyDownEvent(KeyboardKeyDown_Enter_Callback, Keys.Enter), this);


            base.Initialize();
        }


        protected override void LoadContent()
        {
            base.LoadContent();
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
            base.Update(gameTime);

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
            System.Diagnostics.Debug.WriteLine(message);
        }



        private void MouseLeftDown_Callback(MouseButtonStateComparision state)
        {
            var mouseState = state.GetMouseState();
            Debug(string.Format("MouseLeftDown_Callback() X:{0} Y:{1}", mouseState.New.X, mouseState.New.Y));
            var point = new Point(mouseState.New.X, mouseState.New.Y);
            var sprite = ScreenManager.GetSpriteAt(point);
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
                Debug(string.Format("KeyboardChange_Callback() Key:{0}, Old:{1}, New:{2}", s.Key, s.OldState, s.CurrentState));
            }
            ScreenManager._InvokeKeyboardChange(args);
        }

        private void KeyboardKeyDown_Enter_Callback(KeyStateComparision state)
        {
            var keyboardState = state.GetKeyboardState();
            Debug(string.Format("KeyboardKeyDown_Enter_Callback()"));
        }



    }
}
