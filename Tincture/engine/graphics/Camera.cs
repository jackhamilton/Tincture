using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tincture.engine.graphics
{
    public class Camera
    {

        private float x, y, width, height;

        public Camera(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public float getX()
        {
            return x;
        }

        public void setX(float x)
        {
            this.x = x;
        }
        
        public float getY()
        {
            return y;
        }

        public void setY(float y)
        {
            this.y = y;
        }

        public float getWidth()
        {
            return width;
        }

        public void setWidth(float width)
        {
            this.width = width;
        }

        public float getHeight()
        {
            return height;
        }

        public void setHeight(float height)
        {
            this.height = height;
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)x, (int)y, (int)width, (int)height);
        }
    }
}
