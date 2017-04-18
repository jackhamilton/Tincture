using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Tincture.engine;
using Tincture.states;

namespace Tincture
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private State currentState;
        private ContentManager contentManager = new ContentManager();
        //Stores the current game object for referencing
        private static Game cGame;
        private static Point resolution = new Point(1280, 720);
        public static Vector2 screenCenter;
        public static MouseState lastMouseState;

        public Game()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = resolution.Y;
            graphics.PreferredBackBufferWidth = resolution.X;
            //Center the window
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            this.Window.Position = new Point(displayMode.Width / 2 - graphics.PreferredBackBufferWidth / 2,
                displayMode.Height / 2 - graphics.PreferredBackBufferHeight / 2);
            Content.RootDirectory = "Content";
            cGame = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            currentState = new MenuScreen();
            screenCenter = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            contentManager.loadContent(Content);
            // TODO: use this.Content to load your game content here

            currentState.init();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            currentState.update(gameTime, GraphicsDevice);
            lastMouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            currentState.draw(gameTime, GraphicsDevice, spriteBatch);

            base.Draw(gameTime);
        }

        public static void quitGame()
        {
            cGame.Exit();
        }

        //For image generation purposes, dont use this without setting the render target to something else
        public static GraphicsDevice getGraphicsDevice()
        {
            return cGame.GraphicsDevice;
        }

        //Ditto
        public static SpriteBatch getSpriteBatch()
        {
            return cGame.spriteBatch;
        }

        public static void SetGameState(State newState)
        {
            cGame.currentState.screenObjects = new List<GameObject>();
            cGame.currentState = newState;
            cGame.currentState.init();
        }

        public static void setResolution(int x, int y)
        {
            cGame.graphics.PreferredBackBufferHeight = y;
            cGame.graphics.PreferredBackBufferWidth = x;
            resolution.Y = y;
            resolution.X = x;
            screenCenter = new Vector2(x / 2, y / 2);
        }

        public static Point getDisplayResolution()
        {
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            return new Point(displayMode.Width, displayMode.Height);
        }

        public static void setFullscreen(bool mode)
        {
            //Fullscreen will set the resolution to display resolution -- FIX ONCE RESOLUTION SELECTION IS IMPLEMENTED
            if (mode)
            {
                setResolution(getDisplayResolution().X, getDisplayResolution().Y);
                if (!cGame.graphics.IsFullScreen)
                {
                    cGame.graphics.ToggleFullScreen();
                    Game.SetGameState(new OptionsMenu());
                }
            } else
            {
                if (cGame.graphics.IsFullScreen)
                {
                    cGame.graphics.ToggleFullScreen();
                    Game.SetGameState(new OptionsMenu());
                }
            }
        }

        public static bool getIsFullscreen()
        {
            return cGame.graphics.IsFullScreen;
        }
    }
}
