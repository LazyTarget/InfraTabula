using System;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseMoveEvent : EventBase<MouseMoveEventArgs>
    {
        public MouseMoveEvent(Action<MouseMoveEventArgs> callback) : base(callback) { }


        protected override bool Check(out MouseMoveEventArgs arg)
        {
            arg = null;
            var inputStateManager = Game.Services.GetService(typeof(InputStateManager)) as InputStateManager;
            if (inputStateManager == null)
                return false;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.PositionComparision;
            if (c.Changed)
            {
                arg = new MouseMoveEventArgs
                {
                    PositionComparision = c
                };
                return true;
            }
            return false;
        }
    }
}
