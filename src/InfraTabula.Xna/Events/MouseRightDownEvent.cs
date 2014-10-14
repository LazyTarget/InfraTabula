using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;

namespace InfraTabula.Xna
{
    public class MouseRightDownEvent : EventBase
    {
        public MouseRightDownEvent(Action callback) : base(callback) { }
        

        protected override bool Check()
        {
            var inputStateManager = Game.InputState;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.ButtonComparisions[MouseButtons.Right];
            if (c.OldState == ButtonState.Released &&
                c.CurrentState == ButtonState.Pressed)
                return true;
            return false;
        }
    }
}
