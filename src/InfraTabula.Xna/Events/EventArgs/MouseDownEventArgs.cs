using System;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseDownEventArgs : EventArgs
    {
        public MouseButtonStateComparision StateComparision { get; set; }
    }
}
