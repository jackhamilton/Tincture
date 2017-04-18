using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tincture.engine.graphics;
using Tincture.states;

namespace Tincture.engine.world
{
    class Character : GameObject
    {
        private string characterName;
        //To do: make "attribute" class, make this an array
        private Dictionary<String, String> attributes = new Dictionary<String, String>();

        //Character ZTexture state names:
        //default (idledown)
        //idleup
        //idleright
        //idleleft
        //walkup
        //walkdown
        //walkleft
        //walkright

        public Character(State state, string name, float x, float y, float width, float height, ZTexture texture)
            : base(state, name, x, y, width, height, texture)
        {

        }

    }
}
