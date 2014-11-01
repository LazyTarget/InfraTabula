using Microsoft.Xna.Framework;

namespace InfraTabula.Xna.Win
{
    public class Game1 : GameBase
    {
        public Game1()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }


        //private readonly Color _backColor = XnaLibrary.Extensions.FromColor(System.Drawing.SystemColors.Control);
        private readonly Color _backColor = Color.CornflowerBlue;


        protected override void Debug(string message)
        {
            //message = string.Format("Game.{0}", message);
            Program.Debug(message);
        }


        protected override void Initialize()
        {
            // todo: detect current resolution
            GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
            GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
            GraphicsDeviceManager.ApplyChanges();
            
            base.Initialize();
        }



        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backColor);
            
            base.Draw(gameTime);
        }
        

    }
}
