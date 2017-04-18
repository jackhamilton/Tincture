using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tincture.engine
{
    class ContentManager
    {
        public static SpriteFont menuFont;
        public static SpriteFont menuItemFont;
        public static Texture2D testTexture;
        public static Texture2D playerSpritesheet;

        public void loadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            try
            {
                menuFont = content.Load<SpriteFont>("Fonts/Menu");
                menuItemFont = content.Load<SpriteFont>("Fonts/MenuItem");
                testTexture = content.Load<Texture2D>("Images/testImage");
                playerSpritesheet = content.Load<Texture2D>("Images/link");
            } catch (Exception e)
            {
                Console.WriteLine("Error: Could not load resource! Message from framework:\n" + e.Message + "\n" + e.StackTrace);
                Console.WriteLine("Terminating.");
                Game.quitGame();
            }
        }

    }
}
