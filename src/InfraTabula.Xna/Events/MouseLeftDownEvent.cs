using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseLeftDownEvent : EventBase<InputStateManager>
    {
        private readonly InputStateManager _inputStateManager;

        public MouseLeftDownEvent(Action<InputStateManager> evt, InputStateManager inputStateManager) : base(evt)
        {
            this._inputStateManager = inputStateManager;
        }

        public override bool Check()
        {
            var comparison = _inputStateManager.CompareMouse();
            var c = comparison.ButtonComparisions[MouseButtons.Left];
            if (c.OldState == ButtonState.Released &&
                c.CurrentState == ButtonState.Pressed)
                return true;
            return false;
        }
    }
}
