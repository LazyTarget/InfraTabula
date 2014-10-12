using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XnaLibrary;

namespace InfraTabula.Xna
{
    public class GameBase : XnaAppBase //Microsoft.Xna.Framework.Game
    {
        private readonly List<EventBase> _events = new List<EventBase>();


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var evt in _events)
                evt.Update();
        }


        protected void BindEvent(EventBase evt)
        {
            if (_events.Contains(evt))
                throw new InvalidOperationException("Event already bound");
            _events.Add(evt);
        }


        protected void UnbindEvent(EventBase evt)
        {
            var removed = _events.Remove(evt);
            //if (!removed)
            //    throw new InvalidOperationException("Event was not bound");
        }


    }
}
