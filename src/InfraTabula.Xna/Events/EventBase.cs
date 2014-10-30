using System;

namespace InfraTabula.Xna
{
    public interface IEvent
    {
        void Update();
    }
    

    public abstract class EventBase : IEvent
    {
        public Action Event { get; private set; }

        protected EventBase(Action @event)
        {
            if (@event == null)
                throw new ArgumentNullException("event");
            Event = @event;
        }


        private void Trigger()
        {
            Event();
        }


        public virtual void Update()
        {
            var res = Check();
            if (res)
                Trigger();
        }

        public abstract bool Check();

    }




    public abstract class EventBase<T> : IEvent
    {
        public Action<T> Event { get; private set; }

        protected EventBase(Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            Event = action;
        }


        private void Trigger(T arg)
        {
            Event(arg);
        }


        public void Update()
        {
            var arg = default(T);
            Update(arg);
        }

        public void Update(T arg)
        {
            var res = Check();
            if (res)
                Trigger(arg);
        }

        public abstract bool Check();

    }
}
