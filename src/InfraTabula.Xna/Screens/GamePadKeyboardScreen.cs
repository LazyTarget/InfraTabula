using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    public class GamePadKeyboardScreen : GameScreen
    {
        private Vector2 componentCenter;
        private int dialRadius;
        private Vector2 cursorPos = Vector2.Zero;
        private SpriteFont font;

        private Sprite circleSprite;
        private Sprite cursorSprite;
        private Sprite rectSprite;

        public GamePadKeyboardScreen()
        {
            IsPopup = true;
        }


        public override void LoadContent()
        {
            //font = Game.Content.Load<SpriteFont>("GameFont");


            var spriteFactory = new SpriteFactory(Game);
            var bounds = Game.GraphicsDevice.Viewport.Bounds;
            componentCenter = new Vector2(bounds.Width / 2, bounds.Height / 2);
            componentCenter = GetRelative(0.5f, 0.5f);
            componentCenter = GetRelative(0.75f, 0.75f);
            dialRadius = (int)(Game.GraphicsDevice.Viewport.Width * 0.4f);
            dialRadius = (int) GetRelative(0.1f, 0).X;
            var full = GetRelative(1, 1);
            var margin = GetRelative(0.11f, 0.15f);
            componentCenter = new Vector2(full.X - margin.X, full.Y - margin.Y);
            


            // Draw contant sprites
            var circlePos = componentCenter - (new Vector2(dialRadius) / 2);
            var circleTexture = spriteFactory.CreateFilledCircle(dialRadius, Color.Gray);
            circleSprite = spriteFactory.Create<Sprite>(circleTexture);
            circleSprite.Position = circlePos;
            Sprites.Add(circleSprite);


            var rectTexture = spriteFactory.CreateFilledRectangle(3, 3, Color.Orange);
            rectSprite = spriteFactory.Create<Sprite>(rectTexture);
            rectSprite.Position = componentCenter;
            Sprites.Add(rectSprite);


            var cPos = componentCenter + (cursorPos * dialRadius * 0.5f);
            var cursorTexture = spriteFactory.CreateFilledRectangle(5, 5, Color.Blue);
            cursorSprite = spriteFactory.Create<Sprite>(cursorTexture);
            cursorSprite.Position = cPos;
            Sprites.Add(cursorSprite);


            base.LoadContent();
        }

        
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            var cPos = componentCenter + (cursorPos * dialRadius * 0.5f);
            cursorSprite.Position = cPos;

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }




        public override void OnGamePadChange(GamePadChangeEventArgs args)
        {
            var newCursorPos = Vector2.Zero;

            var playerIndexes = Enum.GetValues(typeof(PlayerIndex)).Cast<PlayerIndex>();
            foreach (var playerIndex in playerIndexes)
            {
                var comparison = args.StateComparisions[playerIndex];

                GamePadButtonStateComparision buttonState;
                if (comparison.ButtonComparisions.TryGetValue(Buttons.RightStick, out buttonState) && buttonState.Changed)
                {
                    var stickPos = Game.InputState.CurrentState.GamePad[playerIndex].ThumbSticks.Right;
                    newCursorPos.X += stickPos.X;
                    newCursorPos.Y -= stickPos.Y;
                    args.Handled = true;
                }
            }

            cursorPos = newCursorPos;


            // ignore Back button for close
            //base.OnGamePadChange(args);
        }




        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


    }
}
