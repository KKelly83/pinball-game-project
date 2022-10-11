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
    class Wall
    {
        public Ball ball;
        public Rectangle hitbox;
        public String type;

        Vector2 start, end, v, n;
        float length, bounce;
        public float Bounce;
        public Wall(Vector2 s, Vector2 e, float b)
        {
            start = s;
            end = e;
            bounce = b;
            v = end - start;
            n = new Vector2(v.Y, -v.X);
            n.Normalize();
            length = v.Length();
        }
        
        public float distanceToPoint(Vector2 point)
        {
            Vector2 p1 = end - point;
            Vector2 p2 = start - point;
            if (Vector2.Dot(p1, v) < 0)
                return p1.Length();
            if (Vector2.Dot(p2, v) > 0)
                return p2.Length();
            return Math.Abs(Vector2.Dot(p1, n));
        }

        public Vector2 reflect(Vector2 vector)
        {
            Vector2 v = end - start;
            return vector - 2 * n * Vector2.Dot(vector, n);
        }

        public Vector2 normal()
        {
            return n;
        }
    }
}
