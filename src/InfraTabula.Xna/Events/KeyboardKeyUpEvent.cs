using System;
using Microsoft.Xna.Framework.Input;

namespace InfraTabula.Xna
{
    public class KeyboardKeyUpEvent : EventBase
    {
        public Keys Key { get; private set; }

        public KeyboardKeyUpEvent(Action callback, Keys key) : base(callback)
        {
            Key = key;
        }


        protected override bool Check()
        {
            var inputStateManager = Game.InputState;
            var keyComparison = inputStateManager.CompareKeyboard();
            var c = keyComparison.ButtonComparisions[Key];
            if (c.OldState == KeyState.Down &&
                c.CurrentState == KeyState.Up)
                return true;
            return false;
        }
    }
}
