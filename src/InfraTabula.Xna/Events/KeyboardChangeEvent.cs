using System;
using System.Collections.Generic;
using System.Linq;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class KeyboardChangeEvent : EventBase<IEnumerable<KeyStateComparision>>
    {
        public KeyboardChangeEvent(Action<IEnumerable<KeyStateComparision>> callback) : base(callback)
        {
            
        }


        protected override bool Check(out IEnumerable<KeyStateComparision> changes)
        {
            changes = null;
            var inputStateManager = Game.Services.GetService(typeof(InputStateManager)) as InputStateManager;
            if (inputStateManager == null)
                return false;
            var keyComparison = inputStateManager.CompareKeyboard();
            changes = keyComparison.ButtonComparisions.Where(x => x.Value.Changed).Select(x => x.Value).ToList();
            var c = changes.Any();
            return c;
        }
    }
}
