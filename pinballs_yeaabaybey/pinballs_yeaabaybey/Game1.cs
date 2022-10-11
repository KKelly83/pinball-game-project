using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace pinballs_yeaabaybey
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D tSquare, tBall, tTable,tPaddle, tPaddle2;
        Rectangle rTable,rLeftPaddle,rRightPaddle;
        
        Ball ball;
        Vector2 velocity, pBall;

        Table table;
        

        Bumper bumper;

        double rightAngle1, rightAngle2, rightAngle, leftAngle1, leftAngle2, leftAngle;

        SpriteFont font;
        List<Paddle> pList;

        //paddles n stuff
        Paddle rightPaddle, leftPaddle;
        Bumper rightBU, rightBD, leftBU, leftBD;
        Wall rightWU, rightWD, leftWU, leftWD;
        Bumper debug;
        
        //Screen Parameters
        int screenWidth;
        int screenHeight;

        SoundEffect boing, ding;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            //assign screen size values
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            pList = new List<Paddle>();


            //starting pos
            pBall = new Vector2(555, 630);

            rTable = new Rectangle(0, 0, screenWidth, screenHeight);

            table = new Table();
           
            //paddle stuff
            rLeftPaddle = new Rectangle(160, 705, 100, 50);
            leftAngle = (Math.PI / 180) * 30;
            leftAngle1 = (Math.PI / 180) * 30;
            leftAngle2 = (Math.PI / 180) * -30;

            rRightPaddle = new Rectangle(370, 705, 100, 50);
            rightAngle = (Math.PI / 180) * -210;
            rightAngle1 = (Math.PI / 180) * -210;
            rightAngle2 = (Math.PI / 180) * 210;

            //more paddle stuff
            leftBU = new Bumper(new Vector2(210, 666), 16f, 50f);
            leftBD = new Bumper(new Vector2(216, 731), 16f, 50f);

            rightBU = new Bumper(new Vector2(318, 666), 16f, 50f);
            rightBD = new Bumper(new Vector2(310, 730), 16f, 50f);

            leftWD = new Wall(new Vector2(150, 680), new Vector2(220, 715), 50f);
            leftWU = new Wall(new Vector2(150, 680), new Vector2(210, 650), 50f);

            rightWD = new Wall(new Vector2(375, 680), new Vector2(295, 715), 50f);
            debug = new Bumper(new Vector2(300, 705), 7f, 50f);
            rightWU = new Wall(new Vector2(310, 650), new Vector2(375, 680), 50f);
            leftPaddle = new Paddle(leftWD, leftWU, leftBD, leftBU, 1, debug);
            rightPaddle = new Paddle(rightWD, rightWU, rightBD, rightBU, 2, debug);

            pList.Add(leftPaddle);
            pList.Add(rightPaddle);


            


            base.Initialize();
        }

  
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            tSquare = Content.Load<Texture2D>("square");
            tBall = Content.Load<Texture2D>("orange");
            tTable = Content.Load<Texture2D>("PinballTable1");
            tPaddle = Content.Load<Texture2D>("paddle");
            tPaddle2 = Content.Load<Texture2D>("paddle2");
            font = Content.Load<SpriteFont>("SpriteFont1");
            boing = Content.Load<SoundEffect>("Boing");
            ding = Content.Load<SoundEffect>("Ding");

            ball = new Ball(velocity, screenWidth, screenHeight, pBall, table, pList, boing, ding);
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

      
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            
            // Allows the game to exit
            if (kb.IsKeyDown(Keys.Escape))
                this.Exit();

            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            ball.move(seconds);

            //flipper sprite rotations
            if (kb.IsKeyDown(Keys.Left))
                leftAngle = leftAngle2;
            else
                leftAngle = leftAngle1;

            if (kb.IsKeyDown(Keys.Right))
                rightAngle = rightAngle2;
            else
                rightAngle = rightAngle1;

            foreach(Paddle p in pList)
            {
                p.update(kb);
            }

            base.Update(gameTime);
        }

 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Azure);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(tTable, rTable, Color.White);
            spriteBatch.Draw(tBall, new Rectangle((int)(ball.position.X-ball.radius), (int)(ball.position.Y - ball.radius),32,32), Color.White);
            foreach(Bumper b in table.bumpers)
            {
                int x = (int)b.center.X - (int)b.radius;
                int y = (int)b.center.Y - (int)b.radius;
                spriteBatch.Draw(tBall, new Rectangle(x, y, (int)b.radius * 2, (int)b.radius * 2), Color.White);
            }
            spriteBatch.Draw(tPaddle, rLeftPaddle, null, Color.White, (float)leftAngle, new Vector2(110, 115), SpriteEffects.None, 0f);
            spriteBatch.Draw(tPaddle2, rRightPaddle, null, Color.White, (float)rightAngle, new Vector2(110, 115), SpriteEffects.None, 0f);

            Vector2 pos = new Vector2(20,20);
            spriteBatch.DrawString(font, ball.score.ToString(),pos, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
