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
    class Table
    {
        public List<Wall> walls;
        public List<Bumper> bumpers;
        public List<Wall> boundary;


        public Table()
        {
            walls = new List<Wall>();
            bumpers = new List<Bumper>();
            boundary = new List<Wall>();

            //outside edge walls
            walls.Add(new Wall(new Vector2(600, 20), new Vector2(20, 20), -0.5f)); //top
            walls.Add(new Wall(new Vector2(20, 0), new Vector2(20, 800), -0.5f)); //left
            walls.Add(new Wall(new Vector2(580, 0), new Vector2(580, 800), -0.5f)); //right

            //launch area
            walls.Add(new Wall(new Vector2(530, 650), new Vector2(580, 650), 0f)); //launch floor
            walls.Add(new Wall(new Vector2(510, 800), new Vector2(510, 125), 0f)); //launch wall
            walls.Add(new Wall(new Vector2(600, 100), new Vector2(500, 0), -0.5f)); //launch slope

            //top left slope
            walls.Add(new Wall(new Vector2(120, 20), new Vector2(20, 120), -0.5f));

            //bottom area
            walls.Add(new Wall(new Vector2(20, 600), new Vector2(152, 675), -0.5f)); //bottom left slope
            walls.Add(new Wall(new Vector2(378, 675), new Vector2(510, 600), -0.5f)); //bottom right slope

            //void walls
            walls.Add(new Wall(new Vector2(180, 800), new Vector2(180, 710), -0.5f));
            walls.Add(new Wall(new Vector2(345, 800), new Vector2(345, 710), -0.5f));








            bumpers.Add(new Bumper(new Vector2(200, 400), 49.5f, 100.0f));

            bumpers.Add(new Bumper(new Vector2(150, 200), 25f, 100.0f));
            bumpers.Add(new Bumper(new Vector2(400, 300), 25f, 100.0f));

            ////roof bumpers
        }       
    }
}
