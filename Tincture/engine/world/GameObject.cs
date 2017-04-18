using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Tincture.engine.graphics;
using Tincture.states;

namespace Tincture.engine
{
    public class GameObject
    {
        private State state;
        private string name;
        private ZTexture texture;
        private float x, y, height, width;

        private Action action;
        private bool interactable;
        private bool visible = false;
        private bool centered = false;
        private Vector2 movement = new Vector2(0, 0);

        public GameObject(State state, string name, float x, float y, Texture2D texture)
        {
            this.state = state;
            this.name = name;
            this.x = x;
            this.y = y;
            this.texture = new ZTexture(texture);
            interactable = false;
            if (texture != null)
            {
                height = texture.Height;
                width = texture.Width;
            }
            state.screenObjects.Add(this);
        }

        public GameObject(State state, string name, float x, float y, ZTexture texture)
        {
            this.state = state;
            this.name = name;
            this.x = x;
            this.y = y;
            this.texture = texture;
            interactable = false;
            if (texture.getCurrentTexture() != null)
            {
                height = texture.getCurrentTexture().Height;
                width = texture.getCurrentTexture().Width;
            }
            state.screenObjects.Add(this);
        }

        public GameObject(State state, string name, float x, float y, float width, float height, Texture2D texture)
        {
            this.state = state;
            this.name = name;
            this.x = x;
            this.y = y;
            this.texture = new ZTexture(texture);
            interactable = false;
            this.height = height;
            this.width = width;
            state.screenObjects.Add(this);
        }

        public GameObject(State state, string name, float x, float y, float width, float height, ZTexture texture)
        {
            this.state = state;
            this.name = name;
            this.x = x;
            this.y = y;
            this.texture = texture;
            interactable = false;
            this.height = height;
            this.width = width;
            state.screenObjects.Add(this);
        }

        public bool getCentered()
        {
            return centered;
        }

        public void setCentered(bool centered)
        {
            this.centered = centered;
        }

        public bool getVisible()
        {
            return visible;
        }

        public void setVisible(bool visible)
        {
            this.visible = visible;
        }

        public ZTexture getTexture()
        {
            return texture;
        }

        public bool getInteractable()
        {
            return interactable;
        }

        public void setInteractable(bool interactable)
        {
            this.interactable = interactable;
        }

        public void setAction(Action action)
        {
            this.action = action;
        }

        public void invokeAction()
        {
            if (action != null)
            {
                action.Invoke();
            } else
            {
                Console.WriteLine("Error invoking GameObject action: no action specified");
            }
        }

        public void setTexture(Texture2D newTexture)
        {
            texture = new ZTexture(newTexture);
        }

        public void setTexture(ZTexture newTexture)
        {
            texture = newTexture;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string newName)
        {
            name = newName;
        }

        /**
         * Accounts for centering.
         **/
        public float getX()
        {
            if (!centered)
            {
                return x;
            } else
            {
                return x - width / 2;
            }
        }

        public void setX(float x)
        {
            if (!centered)
            {
                this.x = x;
            }
            else
            {
                this.x = x + width / 2;
            }
        }

        /**
         * Accounts for centering.
         **/
        public float getY()
        {
            if (!centered)
            {
                return y;
            }
            else
            {
                return y - height / 2;
            }
        }

        public void setY(float y)
        {
            if (!centered)
            {
                this.y = y;
            } else
            {
                this.y = y + height / 2;
            }
        }

        public float getHeight()
        {
            return height;
        }

        public void setHeight(float height)
        {
            this.height = height;
        }

        public float getWidth()
        {
            return width;
        }

        public void setWidth(float width)
        {
            this.width = width;
        }

        public Vector2 getPosition()
        {
            return new Vector2(getX(), getY());
        }

        public void setPosition(Vector2 newPos)
        {
            setX(newPos.X);
            setY(newPos.Y);
        }

        public void setMovement(Vector2 movement)
        {
            this.movement = movement;
        }
        
        virtual public void update(GameTime time)
        {
            texture.update();
            setPosition(getPosition() + movement);
        }

        virtual public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (centered)
            {
                spriteBatch.Draw(texture.getCurrentTexture(), new Rectangle((int)(x - width / 2), (int)(y - height / 2), (int)width, (int)height), Color.White);
            } else
            {
                spriteBatch.Draw(texture.getCurrentTexture(), new Rectangle((int)x, (int)y, (int)width, (int)height), Color.White);
            }
            spriteBatch.End();
        }

        public Rectangle getHitbox()
        {
            return new Rectangle((int) getX(), (int) getY(), (int) width, (int) height);
        }
    }
}