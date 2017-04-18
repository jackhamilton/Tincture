using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tincture.engine;

namespace Tincture.states
{
    class MenuScreen : State
    {

        override
        public void init()
        {
            Button startButton = new Button(this, "startButton", "START", ContentManager.menuItemFont,
                Color.Black, Color.White, Color.Black, Color.LightGray, Color.White, Color.DarkGray, 
                Game.screenCenter.X, Game.screenCenter.Y, () => startGame());
            Button optionsButton = new Button(this, "optionsButton", "OPTIONS", ContentManager.menuItemFont,
                Color.Black, Color.White, Color.Black, Color.LightGray, Color.White, Color.DarkGray,
                Game.screenCenter.X, Game.screenCenter.Y + startButton.getTexture().getCurrentTexture().Height * 3f / 2f, () => gameOptions());
            Button quitButton = new Button(this, "quitButton", "QUIT", ContentManager.menuItemFont,
                Color.Black, Color.White, Color.Black, Color.LightGray, Color.White, Color.DarkGray,
                Game.screenCenter.X, Game.screenCenter.Y + startButton.getTexture().getCurrentTexture().Height * 6f / 2f, () => Game.quitGame());
        }

        override
        public void draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.Black);
            base.draw(gameTime, graphics, spriteBatch);
            spriteBatch.Begin();
            //Title
            Vector2 xModTitle = ContentManager.menuFont.MeasureString("TINCTURE") / 2;
            Vector2 yModTitle = new Vector2(0, Game.screenCenter.Y / 2);
            spriteBatch.DrawString(ContentManager.menuFont, "TINCTURE", Game.screenCenter - xModTitle - yModTitle, Color.White);
            spriteBatch.End();

        }

        override
        public GameState getGameState()
        {
            return GameState.MENU_SCREEN;
        }

        private void startGame()
        {
            Game.SetGameState(new GameScreen());
        }

        private void gameOptions()
        {
            Game.SetGameState(new OptionsMenu());
        }
    }
}
