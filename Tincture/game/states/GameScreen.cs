using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tincture.engine;
using Tincture.engine.world.characters;
using Tincture.engine.graphics;
using Tincture.engine.world.menu;

namespace Tincture.states
{
    class GameScreen : State
    {
        public Player player;
        private bool openOptions = false;

        Popup options;

        override
        public void init()
        {
            player = new Player(this, "Link", Game.screenCenter.X, Game.screenCenter.Y, 65, 60);
        }

        override
        public void draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.White);
            base.draw(gameTime, graphics, spriteBatch);
        }

        override
        public void update(GameTime gameTime, GraphicsDevice graphics)
        {
            base.update(gameTime, graphics);
            if ((options == null || options.isDisposed()) && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                openOptions = true;
            }
            if (openOptions && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                openOptions = false;
                //Create the options menu
                options = new Popup(this);
                RenderTarget2D optionsBackTexture = new RenderTarget2D(Game.getGraphicsDevice(), Game.getDisplayResolution().X / 4, 
                    Game.getDisplayResolution().Y / 4);
                Game.getGraphicsDevice().SetRenderTarget(optionsBackTexture);
                Game.getSpriteBatch().Begin();
                Game.getSpriteBatch().Draw(optionsBackTexture, new Rectangle(0, 0, optionsBackTexture.Width, 
                    optionsBackTexture.Height), Color.White);
                Game.getSpriteBatch().DrawString(ContentManager.menuFont, "Options", 
                    new Vector2(optionsBackTexture.Width / 2 - ContentManager.menuFont.MeasureString("Options").X / 2,
                        ContentManager.menuFont.MeasureString("Options").Y / 4), Color.White);
                Game.getSpriteBatch().End();
                Game.getGraphicsDevice().SetRenderTarget(null);
                GameObject menuBackground = new GameObject(options, "optionsBackground", Game.screenCenter.X, 
                    Game.screenCenter.Y, optionsBackTexture);
                menuBackground.setCentered(true);
                menuBackground.setVisible(true);
                Button backButton = new Button(options, "backButton", "BACK TO MENU", ContentManager.menuItemFont,
                    Color.Black, Color.White, Color.Black, Color.LightGray, Color.White, Color.DarkGray,
                    Game.screenCenter.X, Game.screenCenter.Y + Game.screenCenter.Y / 8, () => backToMenu());
            }
        }

        private void backToMenu()
        {
            Game.SetGameState(new MenuScreen());
        }

        override
        public GameState getGameState()
        {
            return GameState.OPTIONS_SCREEN;
        }
    }
}
