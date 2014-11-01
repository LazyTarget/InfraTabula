using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class GamePadChangeEventArgs : HandledEventArgs
    {
        public GamePadChangeEventArgs()
        {
            StateComparisions = new Dictionary<PlayerIndex, IGamePadStateComparison>();
        }

        public Dictionary<PlayerIndex, IGamePadStateComparison> StateComparisions { get; set; }
    }
}
