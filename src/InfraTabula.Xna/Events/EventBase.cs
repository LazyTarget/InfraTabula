using System;
using Microsoft.Xna.Framework;

namespace InfraTabula.Xna
{
    public interface IEvent
    {
        void Update();
        //bool Check();
    }


    public abstract class EventBase : IEvent
    {
        //public Action<EventBase> UpdateHandling { get; set; }
        protected Action Callback { get; private set; }
        protected Game Game { get; private set; }

        protected EventBase(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");
            Callback = callback;
        }


        public void Bind(Game game)
        {
            Game = game;

            var eventBoundbleGame = Game as IEventBoundable;
            if (eventBoundbleGame != null)
                eventBoundbleGame.BindEvent(this);
        }

        public void Unbind()
        {
            var eventBoundbleGame = Game as IEventBoundable;
            if (eventBoundbleGame != null)
                eventBoundbleGame.UnbindEvent(this);

            Game = null;
        }



        private void Trigger()
        {
            Callback();
        }


        public void Update()
        {
            //if (UpdateHandling != null)
            //    UpdateHandling(this);

            if (Game == null)
                throw new InvalidOperationException("Event has not been bound to a Game");

            var res = Check();
            if (res)
                Trigger();
        }

        protected abstract bool Check();
        
    }



    public abstract class EventBase<T> : IEvent
    {
        //public Action<EventBase> UpdateHandling { get; set; }
        protected Action<T> Callback { get; private set; }
        protected Game Game { get; private set; }

        protected EventBase(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");
            Callback = callback;
        }


        public void Bind(Game game)
        {
            // todo: Bind on IEventBoundable objects (ex MouseClick on Sprite)

            Game = game;

            var eventBoundbleGame = Game as IEventBoundable;
            if (eventBoundbleGame != null)
                eventBoundbleGame.BindEvent(this);
        }

        public void Unbind()
        {
            var eventBoundbleGame = Game as IEventBoundable;
            if (eventBoundbleGame != null)
                eventBoundbleGame.UnbindEvent(this);

            Game = null;
        }


        private void Trigger(T arg)
        {
            Callback(arg);
        }

        public void Update()
        {
            //if (UpdateHandling != null)
            //    UpdateHandling(this);

            if (Game == null)
                throw new InvalidOperationException("Event has not been bound to a Game");

            T arg;
            var res = Check(out arg);
            if (res)
                Trigger(arg);
        }

        protected abstract bool Check(out T arg);

    }



    public static class EventExtensions
    {
        public static void SetUpdateHandling<T>(this T evt, Action<T> updateHandling)
            where T : EventBase
        {
            //evt.UpdateHandling = (Action<EventBase>) updateHandling;
        }
    }

}
