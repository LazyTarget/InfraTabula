using Microsoft.Xna.Framework;

namespace InfraTabula.Xna.Win
{
    public class Game1 : GameBase
    {
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }


        //private readonly Color _backColor = XnaLibrary.Extensions.FromColor(System.Drawing.SystemColors.Control);
        private readonly Color _backColor = Color.CornflowerBlue;
        private readonly GraphicsDeviceManager _graphics;


        protected override void Debug(string message)
        {
            //message = string.Format("Game.{0}", message);
            Program.Debug(message);
        }


        protected override void Initialize()
        {
            var listScreen = new ListScreen();
            ScreenManager.AddScreen(listScreen);
            
            base.Initialize();
        }



        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(_backColor);
            
            base.Draw(gameTime);
        }
        

    }
}
