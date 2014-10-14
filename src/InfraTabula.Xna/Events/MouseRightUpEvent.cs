using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;

namespace InfraTabula.Xna
{
    public class MouseRightUpEvent : EventBase
    {
        public MouseRightUpEvent(Action callback) : base(callback) { }
        

        protected override bool Check()
        {
            var inputStateManager = Game.InputState;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.ButtonComparisions[MouseButtons.Right];
            if (c.OldState == ButtonState.Pressed &&
                c.CurrentState == ButtonState.Released)
                return true;
            return false;
        }
    }
}
