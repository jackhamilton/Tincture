using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tincture.engine;
using Tincture.engine.graphics;
using Tincture.engine.world.menu;

namespace Tincture.states
{
    public abstract class State
    {
        private State substate;

        private Camera camera;

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
            if (hasCamera())
            {
                foreach (GameObject g in screenObjects)
                {
                    if (g.getVisible() && new Rectangle((int)g.getPosition().X, (int)g.getPosition().Y, 
                        (int)g.getWidth(), (int)g.getHeight()).Intersects(getCamera().getBounds()))
                    {
                        g.draw(spriteBatch);
                    }
                }
                if (substate != null)
                {
                    //Move substate position by as much as the camera's moved since last time.
                    //You may want to delete the below method and add a State.position type thing.
                    substate.adjustObjectPositions();
                    substate.draw(gameTime, graphics, spriteBatch);
                }
            } else
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

        /**
         * May return null.
         **/
        public Camera getCamera()
        {
            return camera;
        }

        public void setCamera(Camera camera)
        {
            this.camera = camera;
        }

        public bool hasCamera()
        {
            return camera == null;
        }
        
        /**
         * Adjusts all objects containted by the state by the movement vector input. 
         * Useful particularly in popups.
         **/
        public void adjustObjectPositions(Vector2 movement)
        {
            foreach (GameObject g in screenObjects)
            {
                g.setPosition(g.getPosition() + movement);
            }
        }

        public abstract GameState getGameState();

        public enum GameState
        {
            MENU_SCREEN, OPTIONS_SCREEN, GAME, POPUP
        };
    }
}
