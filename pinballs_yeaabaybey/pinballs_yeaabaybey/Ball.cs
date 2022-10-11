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
    class Ball
    {
        public int screenWidth, screenHeight;
        public Vector2 velocity, position, starterPosition;
        public float radius;

        Table table;

        Vector2 gravity;
        Boolean reset, wallAdded;

        List<Paddle> pList;
        public int score;
        int pTouchtimer, pReset;
        int soundCooldown, sReset;

        SoundEffect boing, ding;
        public Ball(Vector2 v, int sW, int sH, Vector2 p, Table t, List<Paddle> pL, SoundEffect b, SoundEffect d)
        {
            screenWidth = sW;
            screenHeight = sH;

            velocity = v;
            position = p;

            radius = 16f;

            table = t;

            starterPosition = new Vector2(555, 630);

            gravity = new Vector2(0, 600);
            //gravity.Y = 0.1635f;
            //gravity.Y = 0.2f;

            reset = true;
            wallAdded = false;
            pList = pL;
            score = 0;
            pTouchtimer = 10;
            pReset = 50;
            boing = b;
            ding = d;
            soundCooldown = 20;
            sReset = 20;
        }

        public void resetPosition()
        {
            position = starterPosition;
            velocity = new Vector2(0, 0);
            reset = true;
            table.boundary.Clear();
            wallAdded = false;
            score = 0;
        }

        float largestDelta = 0.0f;
        public void move(float seconds)
        {
            KeyboardState kb = Keyboard.GetState();

            for(int i = 0; i < 250; i++)
            {

                Vector2 before = position;
                collisions(seconds / 250);
                Vector2 after = position;
                Vector2 diff = after - before;
                float length = diff.Length();

                if(length > largestDelta)
                {
                    largestDelta = length;
                }
            }
            
            if(reset && kb.IsKeyDown(Keys.Down))
            {
                Random r = new Random();
                velocity.Y = -1000f + r.Next(0,200);
                reset = false;
            }
       
            if (position.Y > 780)
                resetPosition();
            if(!wallAdded && position.X < 450)
            {
                table.boundary.Add(new pinballs_yeaabaybey.Wall(new Vector2(510, 125), new Vector2(510, 0), -0.5f));
                wallAdded = true;
            }
        }
        public void collisions(float seconds)
        {
            float speed = velocity.Length();
            if (speed > 1000)
            {
                Vector2 drag = velocity;
                drag.Normalize();
                drag = drag * (1000 - speed) / 5;
                velocity = velocity + drag;
            }
            position += velocity * seconds;

            if (!reset)
                velocity += gravity * seconds;

            soundCooldown--;
            foreach (Wall w in table.walls)
            {
                if (w.distanceToPoint(position) < radius && Vector2.Dot(w.normal(), velocity) < 0)
                {
                    velocity = w.reflect(velocity);
                    velocity = velocity + w.normal() * w.Bounce;
                    if(soundCooldown < 0)
                    {
                        boing.Play();
                        soundCooldown = sReset;
                    }
                }
            }

            foreach (Wall w in table.boundary)
            {
                if (w.distanceToPoint(position) < radius && Vector2.Dot(w.normal(), velocity) < 0)
                {
                    velocity = w.reflect(velocity);
                    velocity = velocity + w.normal() * w.Bounce;
                    if (soundCooldown < 0)
                    {
                        boing.Play();
                        soundCooldown = sReset;
                    }
                }
            }
            //extra boundary collision (seperate from table)
            foreach (Bumper b in table.bumpers)
            {

                if (b.distanceToPoint(position) < b.radius + radius && Vector2.Dot(b.normal(position), velocity) < 0)
                {
                    velocity = b.reflect(velocity, position);

                    velocity = velocity + b.normal(position) * b.bounce;
                    score += 100;
                    if (soundCooldown < 0)
                    {
                        ding.Play();
                        soundCooldown = sReset;
                    }
                }
            }

            //paddles

            pTouchtimer--;
            if (pTouchtimer < 0)
            {
                foreach (Paddle p in pList)
                {
                    foreach (Bumper b in p.ends)
                    {
                        if (b.distanceToPoint(position) < b.radius + radius && Vector2.Dot(b.normal(position), velocity) < 0)
                        {
                            velocity = b.reflect(velocity, position);

                            velocity = velocity + b.normal(position) * b.bounce;
                            pTouchtimer = pReset;
                        }
                    }
                    foreach (Wall w in p.flats)
                    {
                        if (w.distanceToPoint(position) < radius && Vector2.Dot(w.normal(), velocity) < 0)
                        {
                            velocity = w.reflect(velocity);
                            velocity = velocity + w.normal() * w.Bounce;
                            pTouchtimer = pReset;
                        }
                    }
                }
                return;
            }
        }
    }
}
