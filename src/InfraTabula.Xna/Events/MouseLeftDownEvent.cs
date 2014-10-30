using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseLeftDownEvent : EventBase<MouseButtonStateComparision>
    {
        public MouseLeftDownEvent(Action<MouseButtonStateComparision> callback) : base(callback) { }

        
        protected override bool Check(out MouseButtonStateComparision arg)
        {
            var inputStateManager = Game.InputState;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.ButtonComparisions[MouseButtons.Left];
            if (c.OldState == ButtonState.Released &&
                c.CurrentState == ButtonState.Pressed)
            {
                arg = c;
                return true;
            }
            arg = null;
            return false;
        }
    }
}
