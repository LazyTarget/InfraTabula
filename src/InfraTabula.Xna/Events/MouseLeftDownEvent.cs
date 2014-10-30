using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseLeftDownEvent : EventBase
    {
        private readonly Func<InputStateManager> _inputStateManagerFunc;

        public MouseLeftDownEvent(Action evt, Func<InputStateManager> inputState) : base(evt)
        {
            this._inputStateManagerFunc = inputState;
        }

        public override bool Check()
        {
            var inputStateManager = _inputStateManagerFunc.Invoke();
            var comparison = inputStateManager.CompareMouse();
            var c = comparison.ButtonComparisions[MouseButtons.Left];
            if (c.OldState == ButtonState.Released &&
                c.CurrentState == ButtonState.Pressed)
                return true;
            return false;
        }
    }
}
