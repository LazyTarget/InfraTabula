using System;
using System.ComponentModel;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseDownEventArgs : HandledEventArgs
    {
        public MouseButtonStateComparision StateComparision { get; set; }
    }
}
