﻿using System;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;

namespace InfraTabula.Xna
{
    public class MouseLeftDownEvent : EventBase
    {
        public MouseLeftDownEvent(Action callback) : base(callback) { }


        protected override bool Check()
        {
            var inputStateManager = Game.InputState;
            var mouseComparison = inputStateManager.CompareMouse();
            var c = mouseComparison.ButtonComparisions[MouseButtons.Left];
            if (c.OldState == ButtonState.Released &&
                c.CurrentState == ButtonState.Pressed)
                return true;
            return false;
        }
    }
}
