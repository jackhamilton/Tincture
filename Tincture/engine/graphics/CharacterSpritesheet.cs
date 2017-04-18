using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tincture.engine.graphics
{
    class CharacterSpritesheet
    {
        private ZTexture texture = null;
        private bool textureInitialized = false;
        /**
         * @param vSize The number of pixels vertically in each image
         * @param hSize The number of pixels horizontally in each image
         * @param texture The spritesheet image
         * @param stateFrameCountAndNames A collection of tuples as long as the number of distinct rows of animations, with the string
         * representing the name of the state the animation is to be stored in and the int representing the number of frames in that animation.
         **/
        public CharacterSpritesheet(GraphicsDevice device, int vSize, int hSize, Texture2D texture, params Tuple<string, int>[] stateFrameCountAndNames)
        {
            int x = 0, y = 0;
            foreach (Tuple<string, int> t in stateFrameCountAndNames)
            {
                Texture2D[] temp = new Texture2D[t.Item2];
                //load into texture2d[]
                while (x < hSize * t.Item2)
                {
                    Texture2D croppedTexture = new Texture2D(device, hSize, vSize);
                    // Copy the data from the cropped region into a buffer, then into the new texture
                    Color[] data = new Color[hSize * vSize];
                    texture.GetData(0, new Rectangle(x, y, hSize, vSize), data, 0, hSize * vSize);
                    croppedTexture.SetData(data);
                    temp[x / hSize] = croppedTexture;
                    x += hSize;
                }
                if (!textureInitialized)
                {
                    this.texture = new ZTexture(new Tuple<string, Texture2D[]>(t.Item1, temp));
                    textureInitialized = true;
                } else
                {
                    this.texture.addAnimatedState(new Tuple<string, Texture2D[]>(t.Item1, temp));
                }
                x = 0;
                y += vSize;
            }
        }

        public ZTexture getTexture()
        {
            return texture;
        }
    }
}
