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

        public API Api
        {
            get
            {
                var api = Services.GetService(typeof (API)) as API;
                return api;
            }
        }


        public new InputStateManager InputState
        {
            get { return base.InputState; }
        }




        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }


        protected override void HandleInput(GameTime gameTime, InputStateManager input)
        {
            base.HandleInput(gameTime, input);

            foreach (var evt in _events)
                evt.Update();
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


    }
}
