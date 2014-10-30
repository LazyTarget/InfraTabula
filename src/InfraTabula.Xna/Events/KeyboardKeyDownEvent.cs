using System;
using Microsoft.Xna.Framework.Input;

namespace InfraTabula.Xna
{
    public class KeyboardKeyDownEvent : EventBase
    {
        public Keys Key { get; private set; }

        public KeyboardKeyDownEvent(Action callback, Keys key) : base(callback)
        {
            Key = key;
        }


        protected override bool Check()
        {
            var inputStateManager = Game.InputState;
            var keyComparison = inputStateManager.CompareKeyboard();
            var c = keyComparison.ButtonComparisions[Key];
            if (c.OldState == KeyState.Up &&
                c.CurrentState == KeyState.Down)
                return true;
            return false;
        }
    }
}
