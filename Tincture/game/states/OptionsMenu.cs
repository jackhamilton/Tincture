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
    class OptionsMenu : State
    {

        override
        public void init()
        {
            Button backButton = new Button(this, "backButton", "BACK", ContentManager.menuItemFont,
                Color.Black, Color.White, Color.Black, Color.LightGray, Color.White, Color.DarkGray,
                Game.screenCenter.X / 4, Game.screenCenter.Y * 2f - Game.screenCenter.Y / 4, () => backToMenu());
            if (Game.getIsFullscreen())
            {
                Button fullscreenButton = new Button(this, "fullscreenButton", "FULLSCREEN", ContentManager.menuItemFont,
                    Color.Black, Color.LightGray, Color.Black, Color.Gray, Color.White, Color.DarkGray,
                    Game.screenCenter.X - Game.screenCenter.X / 4, Game.screenCenter.Y, () => Game.setFullscreen(true));
                Button windowedButton = new Button(this, "windowedButton", "WINDOWED", ContentManager.menuItemFont,
                    Color.Black, Color.White, Color.Black, Color.LightGray, Color.White, Color.DarkGray,
                    Game.screenCenter.X / 4 + Game.screenCenter.X, Game.screenCenter.Y, () => Game.setFullscreen(false));
            } else
            {
                Button fullscreenButton = new Button(this, "fullscreenButton", "FULLSCREEN", ContentManager.menuItemFont,
                    Color.Black, Color.White, Color.Black, Color.LightGray, Color.White, Color.DarkGray,
                    Game.screenCenter.X - Game.screenCenter.X / 4, Game.screenCenter.Y, () => Game.setFullscreen(true));
                Button windowedButton = new Button(this, "windowedButton", "WINDOWED", ContentManager.menuItemFont,
                    Color.Black, Color.LightGray, Color.Black, Color.Gray, Color.White, Color.DarkGray,
                    Game.screenCenter.X / 4 + Game.screenCenter.X, Game.screenCenter.Y, () => Game.setFullscreen(false));
            }
        }

        override
        public void draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            graphics.Clear(Color.White);
            base.draw(gameTime, graphics, spriteBatch);

            spriteBatch.Begin();
            Vector2 xModTitle = ContentManager.menuItemFont.MeasureString("OPTIONS") / 2;
            Vector2 yModTitle = new Vector2(0, Game.screenCenter.Y - Game.screenCenter.Y / 4);
            spriteBatch.DrawString(ContentManager.menuItemFont, "OPTIONS", Game.screenCenter - xModTitle - yModTitle, Color.Black);
            spriteBatch.End();
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
