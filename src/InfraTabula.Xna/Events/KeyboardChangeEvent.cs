using System;
using System.Collections.Generic;
using System.Linq;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class KeyboardChangeEvent : EventBase<KeyboardChangeEventArgs>
    {
        public KeyboardChangeEvent(Action<KeyboardChangeEventArgs> callback)
            : base(callback)
        {
            
        }


        protected override bool Check(out KeyboardChangeEventArgs args)
        {
            args = null;
            var inputStateManager = Game.Services.GetService(typeof(InputStateManager)) as InputStateManager;
            if (inputStateManager == null)
                return false;
            var keyComparison = inputStateManager.CompareKeyboard();
            var stateComparisons = keyComparison.ButtonComparisions
                .Where(x => x.Value.Changed)
                .ToDictionary(x => x.Key, x => x.Value);
            
            args = new KeyboardChangeEventArgs
            {
                StateComparisions = stateComparisons
            };
            var c = args.StateComparisions.Any();
            return c;
        }
    }
}
