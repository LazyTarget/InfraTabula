using System;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseMoveEvent : EventBase<MousePositionComparision>
    {
        public MouseMoveEvent(Action<MousePositionComparision> callback) : base(callback) { }


        protected override bool Check(out MousePositionComparision arg)
        {
            arg = null;
            var inputStateManager = Game.Services.GetService(typeof(InputStateManager)) as InputStateManager;
            if (inputStateManager == null)
                return false;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.PositionComparision;
            if (c.Changed)
            {
                arg = c;
                return true;
            }
            return false;
        }
    }
}
