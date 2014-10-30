﻿using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class KeyboardKeyDownEvent : EventBase<KeyStateComparision>
    {
        public Keys Key { get; private set; }

        public KeyboardKeyDownEvent(Action<KeyStateComparision> callback, Keys key) : base(callback)
        {
            Key = key;
        }


        protected override bool Check(out KeyStateComparision arg)
        {
            var inputStateManager = Game.InputState;
            var keyComparison = inputStateManager.CompareKeyboard();
            var c = keyComparison.ButtonComparisions[Key];
            if (c.OldState == KeyState.Up &&
                c.CurrentState == KeyState.Down)
            {
                arg = c;
                return true;
            }
            arg = null;
            return false;
        }
    }
}