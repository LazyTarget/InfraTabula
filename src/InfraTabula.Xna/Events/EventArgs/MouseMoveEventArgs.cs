using System;
using System.ComponentModel;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseMoveEventArgs : HandledEventArgs
    {
        public MousePositionComparision PositionComparision { get; set; }
    }
}
