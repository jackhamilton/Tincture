using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tincture.engine;
using Tincture.engine.world.menu;

namespace Tincture.states
{
    public abstract class State
    {
        private State substate;

        public List<GameObject> screenObjects = new List<GameObject>();

        public abstract void init();

        virtual
        public void update(GameTime gameTime, GraphicsDevice graphics)
        {
            foreach (GameObject g in screenObjects)
            {
                g.update(gameTime);
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    if (g.getHitbox().Contains(Mouse.GetState().Position))
                    {
                        if (g.getInteractable())
                        {

                        }
                    }
                }
            }
            if (substate != null)
            {
                substate.update(gameTime, graphics);
            }
        }

        virtual
        public void draw(GameTime gameTime, GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            foreach (GameObject g in screenObjects)
            {
                if (g.getVisible())
                {
                    g.draw(spriteBatch);
                }
            }
            if (substate != null)
            {
                substate.draw(gameTime, graphics, spriteBatch);
            }
        }

        public void setSubstate(State substate)
        {
            this.substate = substate;
        }

        public void disposeSubstate()
        {
            this.substate = null;
        }

        /**
         * May return null.
         **/
        public State getSubstate()
        {
            return substate;
        }

        public abstract GameState getGameState();

        public enum GameState
        {
            MENU_SCREEN, OPTIONS_SCREEN, GAME, POPUP
        };
    }
}
