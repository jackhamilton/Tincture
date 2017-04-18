using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tincture.engine.graphics
{
    public class ZTexture
    {

        public static ZTexture empty = new ZTexture();
        private List<Tuple<string, Texture2D[]>> states = new List<Tuple<string, Texture2D[]>>();
        private Tuple<string, Texture2D[]> currentState;
        private float currentFrame = 0;
        private float animationSpeed = 0.3f;
        private bool animationFrozen = false;

        //For the 'empty' zTexture
        private ZTexture()
        {
            currentState = null;
            states = null;
        }

        /**
         * Creates a new texture based on the given parameters and sets it to the default state.
         **/
        public ZTexture(GraphicsDevice device, int width, int height) 
        {
            currentState = new Tuple<string, Texture2D[]>("default", new Texture2D[]{ new Texture2D(device, width, height) });
            states.Add(currentState);
        }

        /**
         * Takes the given texture and sets it to the default state.
         **/
        public ZTexture(Texture2D texture)
        {
            currentState = new Tuple<string, Texture2D[]>("default", new Texture2D[] { texture });
            states.Add(currentState);
        }

        /**
         * Takes an indefinite list of tuples. Each tuple has the name of a state and a corresponding texture for that state.
         **/
        public ZTexture(params Tuple<string, Texture2D>[] statesList)
        {
            foreach (Tuple<string, Texture2D> tuple in statesList) {
                this.states.Add(new Tuple<string, Texture2D[]>(tuple.Item1, new Texture2D[] { tuple.Item2 }));
            }
            if (this.states.Count > 0)
            {
                currentState = this.states.First();
            }
        }

        public ZTexture(Texture2D[] animation)
        {
            currentState = new Tuple<string, Texture2D[]>("default", animation);
            states.Add(currentState);
            animationFrozen = false;
        }

        public ZTexture(params Tuple<string, Texture2D[]>[] animatedStatesList)
        {
            foreach (Tuple<string, Texture2D[]> tuple in animatedStatesList)
            {
                this.states.Add(new Tuple<string, Texture2D[]>(tuple.Item1, tuple.Item2));
            }
            if (this.states.Count > 0)
            {
                currentState = this.states.First();
            }
        }

        public void update()
        {
            if (!animationFrozen)
            {
                currentFrame += animationSpeed;
                if ((int)currentFrame + 1 > currentState.Item2.Length)
                {
                    currentFrame = 0;
                }
            }
        }

        /** 
         * Returns whether the state change was successful.
         * This method will set the state to the default state if the method runs unsuccessfully and a default exists.
         * If a default does not exist, it'll set it to the first available state.
         **/
        public bool changeState(String stateName)
        {
            Tuple<string, Texture2D[]> defaultTuple = null;
            foreach (Tuple<String, Texture2D[]> state in states)
            {
                if (state.Item1.Equals(stateName))
                {
                    currentState = state;
                    currentFrame = 0;
                    return true;
                } else if (state.Item1.Equals("default"))
                {
                    defaultTuple = state;
                }
            }
            if (defaultTuple != null)
            {
                currentState = defaultTuple;
                currentFrame = 0;
            } else if (states.Count > 0)
            {
                currentState = states.First();
                currentFrame = 0;
            }
            return false;
        }

        public void addState(params Tuple<String, Texture2D>[] statesList)
        {
            foreach (Tuple<string, Texture2D> tuple in statesList)
            {
                this.states.Add(new Tuple<string, Texture2D[]>(tuple.Item1, new Texture2D[] { tuple.Item2 }));
            }
        }

        public void addAnimatedState(params Tuple<String, Texture2D[]>[] animatedStatesList)
        {
            foreach (Tuple<string, Texture2D[]> tuple in animatedStatesList)
            {
                this.states.Add(new Tuple<string, Texture2D[]>(tuple.Item1, tuple.Item2));
            }
        }
        
        public Texture2D getCurrentTexture()
        {
            if (currentState != null)
            {
                return currentState.Item2[(int) currentFrame];
            } else
            {
                return null;
            }
        }

        public String getCurrentStateName()
        {
            return currentState.Item1;
        }

        public void toggleFreezeAnimation()
        {
            animationFrozen = !animationFrozen;
        }

        public void setFreezeAnimation(bool frozen)
        {
            animationFrozen = frozen;
        }
    }
}
