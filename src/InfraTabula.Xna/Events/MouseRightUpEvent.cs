using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseRightUpEvent : EventBase
    {
        public MouseRightUpEvent(Action callback) : base(callback) { }
        

        protected override bool Check()
        {
            var inputStateManager = Game.Services.GetService(typeof(InputStateManager)) as InputStateManager;
            if (inputStateManager == null)
                return false;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.ButtonComparisions[MouseButtons.Right];
            if (c.OldState == ButtonState.Pressed &&
                c.CurrentState == ButtonState.Released)
                return true;
            return false;
        }
    }
}
