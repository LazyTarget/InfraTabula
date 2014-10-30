using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseLeftUpEvent : EventBase<MouseButtonStateComparision>
    {
        public MouseLeftUpEvent(Action<MouseButtonStateComparision> callback) : base(callback) { }


        protected override bool Check(out MouseButtonStateComparision arg)
        {
            var inputStateManager = Game.InputState;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.ButtonComparisions[MouseButtons.Left];
            if (c.OldState == ButtonState.Pressed &&
                c.CurrentState == ButtonState.Released)
            {
                arg = c;
                return true;
            }
            arg = null;
            return false;
        }
    }
}
