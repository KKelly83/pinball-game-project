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
    class Bumper
    {
        public Vector2 center;
        public float radius, bounce;
        public Bumper(Vector2 c, float r, float b)
        {
            center = c;
            radius = r;
            bounce = b;
        }

        public Vector2 normal(Vector2 point)
        {
            Vector2 n = point - center;
            n.Normalize();
            return n;
        }

        public float distanceToPoint(Vector2 point)
        {
            return (center - point).Length();
        }

        public Vector2 reflect(Vector2 vector, Vector2 point)
        {
            Vector2 n = normal(point);
            return vector - 2 * n * Vector2.Dot(vector, n);
        }
    }
}
