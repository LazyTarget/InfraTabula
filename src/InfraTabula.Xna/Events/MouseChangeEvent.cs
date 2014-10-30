using System;
using System.Collections.Generic;
using System.Linq;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class MouseChangeEvent : EventBase<IEnumerable<MouseButtonStateComparision>>
    {
        public MouseChangeEvent(Action<IEnumerable<MouseButtonStateComparision>> callback)
            : base(callback)
        {
            
        }


        protected override bool Check(out IEnumerable<MouseButtonStateComparision> changes)
        {
            var inputStateManager = Game.InputState;
            var mouseComparison = inputStateManager.CompareMouse();
            changes = mouseComparison.ButtonComparisions.Where(x => x.Value.Changed).Select(x => x.Value).ToList();
            var c = changes.Any();
            return c;
        }
    }
}
