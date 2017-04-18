using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tincture.engine.graphics;
using Tincture.states;

namespace Tincture.engine
{
    public class Button : GameObject
    {
        Action onTrigger;
        //0: default, 1: mouseover, 2: selected
        private Texture2D[] textures = new Texture2D[3];
        
        /**
         * @param defaultTexture The texture to be displayed by default.
         * @param mouseOverTexture The texture to be displayed when the mouse is positioned over the button.
         * @param selectTexture The texture to displayed while the mouse is in a pressed state over the button while not yet released.
         **/
        public Button(State state, string name, Texture2D defaultTexture, Texture2D mouseOverTexture, Texture2D selectTexture, int buttonX, int buttonY, Action onTrigger)
            : base(state, name, buttonX, buttonY, defaultTexture)
        {
            textures[0] = defaultTexture;
            textures[1] = mouseOverTexture;
            textures[2] = selectTexture;
            this.onTrigger = onTrigger;
            float modX = (float)(textures[0].Width / 2f);
            setX(getX() - modX);
            float modY = (float)(textures[0].Height / 2f);
            setY(getY() - modY);
            setVisible(true);
            setInteractable(true);
        }

        /*
         * Same as above, but without a mouseover texture. 
         **/
        public Button(State state, string name, Texture2D defaultTexture, Texture2D selectTexture, int buttonX, int buttonY, Action onTrigger) 
            : this(state, name, defaultTexture, defaultTexture, selectTexture, buttonX, buttonY, onTrigger) {}

        /*
         * Same as above, but with only one texture. 
         **/
        public Button(State state, string name, Texture2D defaultTexture, int buttonX, int buttonY, Action onTrigger)
            : this(state, name, defaultTexture, defaultTexture, defaultTexture, buttonX, buttonY, onTrigger) { }

        /**
         * A constructor for a button using text and an assortment of colors for hover and click options instead of an array of Texture2Ds.
         **/
        public Button(State state, string name, string text, SpriteFont font, Color textColor, Color backgroundColor, Color textColorHovered, Color backgroundColorHovered, 
            Color textColorClicked, Color backgroundColorClicked, float buttonX, float buttonY, Action onTrigger)
            : base(state, name, buttonX, buttonY, ZTexture.empty)
        {
            //Render textures for all three images
            float width = font.MeasureString(text).X, height = font.MeasureString(text).Y;
            RenderTarget2D texture1 = new RenderTarget2D(Game.getGraphicsDevice(), (int)width, (int)height);
            Game.getGraphicsDevice().SetRenderTarget(texture1);
            Game.getSpriteBatch().Begin();
            Texture2D texture1Background = new Texture2D(Game.getGraphicsDevice(), 1, 1, false, SurfaceFormat.Color);
            texture1Background.SetData<Color>(new Color[] { backgroundColor });
            Game.getSpriteBatch().Draw(texture1Background, new Rectangle(0, 0, (int)width, (int)height), backgroundColor);
            Game.getSpriteBatch().DrawString(font, text, new Vector2(0, 0), textColor);
            Game.getSpriteBatch().End();
            textures[0] = texture1;

            RenderTarget2D texture2 = new RenderTarget2D(Game.getGraphicsDevice(), (int)width, (int)height);
            Game.getGraphicsDevice().SetRenderTarget(texture2);
            Game.getSpriteBatch().Begin();
            Texture2D texture2Background = new Texture2D(Game.getGraphicsDevice(), 1, 1, false, SurfaceFormat.Color);
            texture2Background.SetData<Color>(new Color[] { backgroundColorHovered });
            Game.getSpriteBatch().Draw(texture1Background, new Rectangle(0, 0, (int)width, (int)height), backgroundColorHovered);
            Game.getSpriteBatch().DrawString(font, text, new Vector2(0, 0), textColorHovered);
            Game.getSpriteBatch().End();
            textures[1] = texture2;

            RenderTarget2D texture3 = new RenderTarget2D(Game.getGraphicsDevice(), (int)width, (int)height);
            Game.getGraphicsDevice().SetRenderTarget(texture3);
            Game.getSpriteBatch().Begin();
            Texture2D texture3Background = new Texture2D(Game.getGraphicsDevice(), 1, 1, false, SurfaceFormat.Color);
            texture3Background.SetData<Color>(new Color[] { backgroundColorClicked });
            Game.getSpriteBatch().Draw(texture1Background, new Rectangle(0, 0, (int)width, (int)height), backgroundColorClicked);
            Game.getSpriteBatch().DrawString(font, text, new Vector2(0, 0), textColorClicked);
            Game.getSpriteBatch().End();
            Game.getGraphicsDevice().SetRenderTarget(null);
            textures[2] = texture3;

            this.onTrigger = onTrigger;
            setTexture(textures[0]);
            
            float modX = (float)(textures[0].Width / 2f);
            setX(getX() - modX);
            float modY = (float)(textures[0].Height / 2f);
            setY(getY() - modY);
            setVisible(true);
            setInteractable(true);
        }

        /**
         * @return true: If a player enters the button with mouse
         */
        private bool enterButton()
        {
            if (Mouse.GetState().X < getX() + getTexture().getCurrentTexture().Width &&
                    Mouse.GetState().X > getX() &&
                    Mouse.GetState().Y < getY() + getTexture().getCurrentTexture().Height &&
                    Mouse.GetState().Y > getY())
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    setTexture(textures[2]);
                } else
                {
                    setTexture(textures[1]);
                }
                return true;
            }
            if (!getTexture().Equals(textures[0]))
            {
                setTexture(textures[0]);
            }
            return false;
        }
        
        override
        public void update(GameTime gameTime)
        {
            if (getInteractable() && enterButton() && Game.lastMouseState.LeftButton == ButtonState.Pressed && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                onTrigger.Invoke();
            }
        }

        override
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(getTexture().getCurrentTexture(), new Rectangle((int)getX(), (int)getY(), 
                getTexture().getCurrentTexture().Width, getTexture().getCurrentTexture().Height), Color.White);
            spriteBatch.End();
        }
    }
}
