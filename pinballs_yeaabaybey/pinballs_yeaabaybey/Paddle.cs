using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace pinballs_yeaabaybey
{
    class Paddle
    {
        public List<Wall> flats;
        public List<Bumper> ends;
        Wall dF, uF;
        Bumper dE, uE;

        public Boolean active;
        int t;
        Bumper debug;
        public Paddle(Wall dFlat, Wall uFlat, Bumper dEnd, Bumper uEnd, int type, Bumper dB)
        {
            flats = new List<Wall>();
            ends = new List<Bumper>();

            dF = dFlat;
            uF = uFlat;
            dE = dEnd;
            uE = uEnd;
            debug = dB;
            //1 is left, 2 is right
            t = type;

            active = false;
        }

        //clear and create walls/bumpers where the paddle is
        public void up()
        {
            flats.Clear();
            ends.Clear();
            flats.Add(uF);
            ends.Add(uE);
            ends.Add(debug);
        }
        public void down()
        {
            flats.Clear();
            ends.Clear();
            flats.Add(dF);
            ends.Add(dE);
            ends.Add(debug);
        }
        public void update(KeyboardState kb)
        {
            if(t == 1)
            {
                if (kb.IsKeyDown(Keys.Left))
                {
                    if (!active)
                    {
                        up();
                        active = true;
                    }
                }
                else
                {
                    down();
                    active = false;
                }
            }
            else if(t == 2)
            {
                if (kb.IsKeyDown(Keys.Right))
                {
                    if (!active)
                    {
                        up();
                        active = true;
                    }
                }
                else
                {
                    down();
                    active = false;
                }
            }
        }
    }
}
