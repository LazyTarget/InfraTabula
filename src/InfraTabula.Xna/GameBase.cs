using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XnaLibrary;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class GameBase : XnaAppBase, IEventBoundable     //Microsoft.Xna.Framework.Game
    {
        private readonly List<IEvent> _events = new List<IEvent>();
        private MouseLeftDownEvent _mouseLeftDownEvent;

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
            
            _mouseLeftDownEvent = new MouseLeftDownEvent(MouseLeftDown);
            _mouseLeftDownEvent.Bind(this);


            base.Initialize();
        }


        protected override void LoadContent()
        {
            base.LoadContent();
        }


        protected override void UnloadContent()
        {
            base.UnloadContent();

            _mouseLeftDownEvent.Unbind();
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


        private void MouseLeftDown(MouseButtonStateComparision state)
        {
            var mouseState = state.GetMouseState();
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



    }
}
