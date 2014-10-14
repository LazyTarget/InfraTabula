using System;

namespace InfraTabula.Xna
{
    public interface IEvent
    {
        void Update();
        //bool Check();
    }


    public abstract class EventBase : IEvent
    {
        public Action<EventBase> UpdateHandling { get; set; }
        public Action Callback { get; private set; }
        protected GameBase Game { get; private set; }

        protected EventBase(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");
            Callback = callback;
        }


        public void Bind(GameBase game)
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
            if (UpdateHandling != null)
                UpdateHandling(this);

            if (Game == null)
                throw new InvalidOperationException("Event has not been bound to a Game");

            var res = Check();
            if (res)
                Trigger();
        }

        protected abstract bool Check();

    }


    public static class EventExtensions
    {
        public static void SetUpdateHandling<T>(this T evt, Action<T> updateHandling)
            where T : EventBase
        {
            evt.UpdateHandling = (Action<EventBase>) updateHandling;
        }
    }




    //public abstract class EventBase<T> : IEvent
    //{
    //    public Action<T> Event { get; private set; }

    //    protected EventBase(Action<T> action)
    //    {
    //        if (action == null)
    //            throw new ArgumentNullException("action");
    //        Event = action;
    //    }


    //    private void Trigger()
    //    {
    //        Event(arg);
    //    }


    //    public void Update()
    //    {
    //        var res = Check();
    //        if (res)
    //            Trigger(arg);
    //    }

    //    public abstract bool Check();

    //}

}
