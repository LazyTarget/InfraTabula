using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework.Input;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class KeyboardChangeEventArgs : HandledEventArgs
    {
        public KeyboardChangeEventArgs()
        {
            StateComparisions = new Dictionary<Keys, KeyStateComparision>();
        }

        public Dictionary<Keys, KeyStateComparision> StateComparisions { get; set; }
    }
}
