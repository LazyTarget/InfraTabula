using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;

namespace InfraTabula.Xna
{
    public class MouseLeftUpEvent : EventBase
    {
        public MouseLeftUpEvent(Action callback) : base(callback) { }


        protected override bool Check()
        {
            var inputStateManager = Game.InputState;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.ButtonComparisions[MouseButtons.Left];
            if (c.OldState == ButtonState.Pressed &&
                c.CurrentState == ButtonState.Released)
                return true;
            return false;
        }
    }
}
