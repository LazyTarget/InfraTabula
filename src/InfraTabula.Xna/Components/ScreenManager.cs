using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaLibrary.Input;

namespace InfraTabula.Xna
{
    /// <summary>
    /// The screen manager is a component which manages one or more GameScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times, and automatically routes input to the
    /// topmost active screen.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        #region Fields

        private readonly List<GameScreen> screens = new List<GameScreen>();
        private readonly List<GameScreen> screensToUpdate = new List<GameScreen>();

        private readonly InputState input = new InputState();

        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D blankTexture;

        private bool isInitialized;
        private bool traceEnabled;

        private Vector2 _scale;

        #endregion



        #region Properties

        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }


        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }


        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }



        public new GameBase Game
        {
            get { return (GameBase) base.Game; }
        }


        #endregion

        #region Initialization


        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(Game game)
            : base(game)
        {
            // we must set EnabledGestures before we can query for them, but
            // we don't assume the game wants to read them.
            //TouchPanel.EnabledGestures = GestureType.None;
        }


        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            isInitialized = true;

            Game.Window.ClientSizeChanged += Window_OnClientSizeChanged;
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            //ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            //font = content.Load<SpriteFont>("menufont");
            //blankTexture = content.Load<Texture2D>("blank");

            // Tell each of the screens to load their content.
            foreach (GameScreen screen in screens)
            {
                screen.LoadContent();
            }
        }


        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }


        #endregion

        #region Update and Draw

        private void Window_OnClientSizeChanged(object sender, EventArgs args)
        {
            //var scale = (double) Game.Window.ClientBounds.Width/
            //            (double) Game.Window.ClientBounds.Height;
            //var ratio = GraphicsDevice.Viewport.AspectRatio;

            //Game.GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsDevice.Viewport.Width;
            //Game.GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsDevice.Viewport.Height;
            //Game.GraphicsDeviceManager.ApplyChanges();
        }


        /// <summary>
        /// Allows each screen to run logic.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Read the keyboard and gamepad.
            //input.Update();

            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
        }


        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }


        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            var defaultscale = (float)GraphicsDeviceManager.DefaultBackBufferWidth /
                               (float)GraphicsDeviceManager.DefaultBackBufferHeight;
            //var scale = (float)GraphicsDeviceManager.PreferredBackBufferWidth /
            //            (float)GraphicsDeviceManager.PreferredBackBufferHeight;
            //_scale = (float)GraphicsDevice.Viewport.Width /
            //                  GraphicsDeviceManager.DefaultBackBufferWidth;
            _scale = new Vector2(
                GraphicsDevice.Viewport.Width/(float) GraphicsDeviceManager.DefaultBackBufferWidth,
                GraphicsDevice.Viewport.Height/(float) GraphicsDeviceManager.DefaultBackBufferHeight);
            var matrixScale = Matrix.CreateScale(_scale.X, _scale.Y, 1);

            //SpriteBatch.Begin();
            SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, matrixScale);

            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
            SpriteBatch.End();
        }


        #endregion


        internal void _InvokeKeyboardChange(KeyboardChangeEventArgs args)
        {
            var screens = GetScreens().Where(x => x.IsActive && x.ScreenState == ScreenState.Active).ToList();
            foreach (var screen in screens)
            {
                if (args.Handled)
                    break;
                screen._InvokeKeyboardChange(args);
                break;      // only focused screen should recieve input
            }
        }



        #region Public Methods


        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen)
        {
            screen.ScreenManager = this;
            screen.IsExiting = false;

            screens.Add(screen);
            
            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.LoadContent();
            }

            // update the TouchPanel to respond to gestures this screen is interested in
            //TouchPanel.EnabledGestures = screen.EnabledGestures;
        }


        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);

            // if there is a screen still in the manager, update TouchPanel
            // to respond to gestures that screen is interested in.
            if (screens.Count > 0)
            {
                //TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
            }
        }


        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }


        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             Color.Black * alpha);

            spriteBatch.End();
        }



        public ISprite GetSpriteAt(Point point)
        {
            var matchingSprites = new List<ISprite>();
            var screens = GetScreens().Where(x => x.IsActive && x.ScreenState == ScreenState.Active).ToList();
            foreach (var screen in screens)
            {
                foreach (var s in screen.Sprites)
                {
                    var rect = s.Bounds;
                    rect = new Rectangle(
                        (int) Math.Round(rect.X * _scale.X),
                        (int) Math.Round(rect.Y * _scale.Y),
                        (int) Math.Round(rect.Width * _scale.X),
                        (int) Math.Round(rect.Height * _scale.Y));
                    var contains = rect.Contains(point);
                    if (contains)
                        matchingSprites.Add(s);
                }
                if (matchingSprites.Any())
                    break;
            }
            var sprite = matchingSprites.FirstOrDefault();
            return sprite;
        }

        #endregion
    }
}
