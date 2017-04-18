using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tincture.engine.graphics;
using Tincture.states;

namespace Tincture.engine.world.characters
{
    class Player : Character
    {
        public ZTexture playerTexture;
        private int lastXDir = 0, lastYDir = 0;

        public Player(State state, string name, float x, float y, float width, float height)
            : base(state, name, x, y, width, height, ZTexture.empty)
        {
            playerTexture = new CharacterSpritesheet(Game.getGraphicsDevice(), 130, 120, ContentManager.playerSpritesheet,
                Tuple.Create("default", 1), Tuple.Create("idleup", 1), Tuple.Create("idleleft", 1), Tuple.Create("idleright", 1),
                Tuple.Create("walkdown", 10), Tuple.Create("walkleft", 10), Tuple.Create("walkup", 10), Tuple.Create("walkright", 10))
                .getTexture();
            setTexture(playerTexture);
            setVisible(true);
            setCentered(true);
        }

        override
        public void update(GameTime time) 
        {
            base.update(time);
            int xDir = 0, yDir = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                xDir = -1;
            } else if (Keyboard.GetState().IsKeyDown(Keys.Right) && !Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                xDir = 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && !Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                yDir = 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && !Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                yDir = -1;
            }
            if (xDir == 0 && yDir == 0)
            {
                xDir = lastXDir;
                yDir = lastYDir;
                getTexture().setFreezeAnimation(true);
                setMovement(new Vector2(0, 0));
            } else
            {
                lastXDir = xDir;
                lastYDir = yDir;
                getTexture().setFreezeAnimation(false);
                setMovement(new Vector2(xDir * 2, yDir * 2));
            }
            //If moving only left
            if (xDir < 0 && yDir == 0)
            {
                if (!getTexture().getCurrentStateName().Equals("walkleft"))
                {
                    getTexture().changeState("walkleft");
                }
            }
            //only right
            if (xDir > 0 && yDir == 0)
            {
                if (!getTexture().getCurrentStateName().Equals("walkright"))
                {
                    getTexture().changeState("walkright");
                }
            }
            //only up
            if (xDir == 0 && yDir < 0)
            {
                if (!getTexture().getCurrentStateName().Equals("walkup"))
                {
                    getTexture().changeState("walkup");
                }
            }
            //only down
            if (xDir == 0 && yDir > 0)
            {
                if (!getTexture().getCurrentStateName().Equals("walkdown"))
                {
                    getTexture().changeState("walkdown");
                }
            }
        }
    }
}
