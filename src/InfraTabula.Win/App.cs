using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaLibrary;
using KeyEventArgs = XnaLibrary.KeyEventArgs;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace InfraTabula.Win
{
    public class App : XnaAppBase
    {
        public App()
        {
            _graphics = new GraphicsDeviceManager(this);

            ////// Mouse
            //////base.OnMouseDown += _OnMouseDown;
            ////base.OnMouseUp += _OnMouseUp;

            ////// Keyboard
            ////base.OnKeyDown += _OnKeyDown;
            ////base.OnKeyUp += _OnKeyUp;

            ////// GamePad
            ////base.OnButtonDown += _OnButtonDown;
            ////base.OnButtonUp += _OnButtonUp;

            Window.AllowUserResizing = true;
        }


        private readonly Color _backColor = XnaLibrary.Extensions.FromColor(System.Drawing.SystemColors.Control);
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly List<Sprite> _sprites = new List<Sprite>();
        private int _velocity = 5;



        protected API API { get { return API.Instance; } }



        private void Debug(string message)
        {
            message = string.Format("App.{0}", message);
            Program.Debug(message);
        }





        protected override void Initialize()
        {
            base.Initialize();
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            //API.Authenticate();
            //var items = API.GetItems().ToList();


            _sprites.Add(new Sprite(CreateRectangle(GraphicsDevice.Viewport.Width - 10, 10, Color.Lime), Color.Lime, new Vector2(5, 5)));
            _sprites.Add(Sprite.GetRandom(GraphicsDevice));
            _sprites.Add(new Sprite(CreateRectangle(70, 150, Color.Blue), Color.Blue, new Vector2(10, 20)));
            _sprites.Add(new Sprite(CreateRectangle(70, 150, Color.Red), Color.Red, new Vector2(90, 70)));
            for (var i = 0; i < 5; i++)
                _sprites.Add(Sprite.GetRandom(GraphicsDevice));
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //var state = base.GetKeyboardState();
            //if (state.IsKeyDown(Keys.Up))
            //    GoUp();
            //if (state.IsKeyDown(Keys.Right))
            //    GoRight();
            //if (state.IsKeyDown(Keys.Down))
            //    GoDown();
            //if (state.IsKeyDown(Keys.Left))
            //    GoLeft();


            //var state2 = base.GetGamePadState(PlayerIndex.One);
            //if (state2.IsButtonDown(Buttons.DPadUp))
            //    GoUp();
            //else if (state2.IsButtonDown(Buttons.DPadRight))
            //    GoRight();
            //else if (state2.IsButtonDown(Buttons.DPadDown))
            //    GoDown();
            //else if (state2.IsButtonDown(Buttons.DPadLeft))
            //    GoLeft();
        }


        #region Drawing

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(_backColor);

            _spriteBatch.Begin();

            foreach (var s in _sprites)
                _spriteBatch.Draw(s.Texture, s.Position, s.Color);

            _spriteBatch.End();


            base.Draw(gameTime);
        }


        private Texture2D CreateRectangle(int width, int height, Color colori)
        {
            return CreateRectangle(GraphicsDevice, width, height, colori);
        }

        internal static Texture2D CreateRectangle(GraphicsDevice graphicsDevice, int width, int height, Color colori)
        {
            Texture2D rectangleTexture = new Texture2D(graphicsDevice, width, height);  // create the rectangle texture, ,but it will have no color! lets fix that
            Color[] color = new Color[width * height];              //set the color to the amount of pixels in the textures
            for (int i = 0; i < color.Length; i++)                  //loop through all the colors setting them to whatever values we want
            {
                color[i] = colori;
            }
            rectangleTexture.SetData(color);//set the color data on the texture
            return rectangleTexture;//return the texture
        }


        #endregion



        #region Input management


        private void _OnMouseDown(object sender, MouseButtonEventArgs args)
        {

        }
        private void _OnMouseUp(object sender, MouseButtonEventArgs args)
        {

        }


        private void _OnKeyDown(object sender, KeyEventArgs args)
        {
            var stateTracker = (KeyboardStateTracker) sender;
            var state = (KeyboardState) stateTracker.GetCurrentState();
            if (state.IsKeyDown(Keys.LeftAlt) && state.IsKeyDown(Keys.Enter))
            {
                _graphics.ToggleFullScreen();
                Debug("IsFullScreen " + _graphics.IsFullScreen);
                //Debug("Viewport: " + Newtonsoft.Json.JsonConvert.SerializeObject(_graphics.GraphicsDevice.Viewport));
            }

            //if (args.Key == Keys.Up)
            //    GoUp();
            //else if (args.Key == Keys.Right)
            //    GoRight();
            //else if (args.Key == Keys.Down)
            //    GoDown();
            //else if (args.Key == Keys.Left)
            //    GoLeft();
        }

        private void _OnKeyUp(object sender, KeyEventArgs args)
        {

        }


        private void _OnButtonDown(object sender, GamePadButtonEventArgs args)
        {
            if (args.Button.HasFlag(Buttons.DPadUp))
                GoUp();
            else if (args.Button.HasFlag(Buttons.DPadRight))
                GoRight();
            else if (args.Button.HasFlag(Buttons.DPadDown))
                GoDown();
            else if (args.Button.HasFlag(Buttons.DPadLeft))
                GoLeft();
        }

        private void _OnButtonUp(object sender, GamePadButtonEventArgs args)
        {

        }


        #endregion



        private void GoUp()
        {
            Debug("GoUp()");
            foreach (var s in _sprites)
            {
                s.Position = new Vector2(s.Position.X, Math.Max(0, s.Position.Y - _velocity));
            }
        }

        private void GoRight()
        {
            Debug("GoRight()");
            foreach (var s in _sprites)
            {
                s.Position = new Vector2(Math.Min(GraphicsDevice.Viewport.Width - s.Texture.Width, s.Position.X + _velocity),
                    s.Position.Y);
            }
        }

        private void GoDown()
        {
            Debug("GoDown()");
            foreach (var s in _sprites)
            {
                s.Position = new Vector2(s.Position.X,
                    Math.Min(GraphicsDevice.Viewport.Height - s.Texture.Height, s.Position.Y + _velocity));
            }
        }

        private void GoLeft()
        {
            Debug("GoLeft()");
            foreach (var s in _sprites)
            {
                s.Position = new Vector2(Math.Max(0, s.Position.X - _velocity), s.Position.Y);
            }
        }



    }
}
