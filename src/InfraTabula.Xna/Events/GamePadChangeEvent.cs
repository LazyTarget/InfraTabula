using System;
using System.Linq;
using Microsoft.Xna.Framework;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class GamePadChangeEvent : EventBase<GamePadChangeEventArgs>
    {
        public GamePadChangeEvent(Action<GamePadChangeEventArgs> callback)
            : base(callback)
        {
            
        }


        protected override bool Check(out GamePadChangeEventArgs args)
        {
            args = null;
            var inputStateManager = Game.Services.GetService(typeof(InputStateManager)) as InputStateManager;
            if (inputStateManager == null)
                return false;

            args = new GamePadChangeEventArgs();
            var hasChanges = false;
            var playerIndexes = Enum.GetValues(typeof (PlayerIndex)).Cast<PlayerIndex>();
            foreach (var playerIndex in playerIndexes)
            {
                var gamePadComparison = inputStateManager.CompareGamePad(playerIndex);

                var buttonComparisons = gamePadComparison.ButtonComparisions
                    .Where(x => x.Value.Changed)
                    .ToDictionary(x => x.Key, x => x.Value);
                gamePadComparison.ButtonComparisions = buttonComparisons;

                // todo: filter for only changed

                args.StateComparisions[playerIndex] = gamePadComparison;

                if (buttonComparisons.Any() || gamePadComparison.ConnectionChanged)
                    hasChanges = true;
            }
            var c = hasChanges;
            return c;
        }
    }
}
