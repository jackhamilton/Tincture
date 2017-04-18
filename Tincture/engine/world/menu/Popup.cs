using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tincture.states;

namespace Tincture.engine.world.menu
{
    class Popup : State
    {
        List<GameObject> reenableOnDispose = new List<GameObject>();

        State parentState;

        private bool prepareToDispose = false;
        private bool prepareForClick = false;
        private bool disposed = false;

        /**
         * A menu that shows up over everything else on the screen and disables clicking elsewhere.
         * @param reenable The GameObjects that were [interactable] that will have to be
         * reset to an interactable state when the popup disposes.
         **/
        public Popup(State parentState)
        {
            this.parentState = parentState;
            parentState.setSubstate(this);
            foreach (GameObject g in parentState.screenObjects)
            {
                if (g.getInteractable())
                {
                    reenableOnDispose.Add(g);
                    g.setInteractable(false);
                }
            }
        }

        override
        public void update(GameTime time, GraphicsDevice graphics)
        {
            base.update(time, graphics);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                prepareForClick = true;
            }
            if (prepareForClick && Mouse.GetState().LeftButton == ButtonState.Released)
            {
                bool terminate = true;
                foreach (GameObject g in screenObjects)
                {
                    if (g.getHitbox().Contains(Mouse.GetState().Position))
                    {
                        terminate = false;
                    }
                }
                if (terminate && getSubstate() == null)
                {
                    dispose();
                    terminate = false;
                }
                prepareForClick = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && getSubstate() == null)
            {
                prepareToDispose = true;
            }
            if (prepareToDispose && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                dispose();
                prepareToDispose = false;
            }
        }

        override
        public void draw(GameTime time, GraphicsDevice device, SpriteBatch batch)
        {
            base.draw(time, device, batch);
        }

        override
        public void init() {}

        public void dispose()
        {
            parentState.disposeSubstate();
            foreach (GameObject g in reenableOnDispose)
            {
                g.setInteractable(true);
            }
            disposed = true;
        }

        public bool isDisposed()
        {
            return disposed;
        }

        override
        public GameState getGameState()
        {
            return GameState.POPUP;
        }
    }
}
