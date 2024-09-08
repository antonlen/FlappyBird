using FlappyBird.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO.Pipes;

namespace FlappyBird
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;

        Bird flappyBird;
        Background background;
        List<Pipes> pipes = new List<Pipes>();
        Texture2D pipesTexture;
        Texture2D gameTexture;
        Texture2D backgroundTexture;
        TimeSpan pipeTimer;
        int PipeDistance = 150;
        Random ran = new Random(4);
        //     List<Score> scores = new List<Score>();
        SpriteFont spriteFont;
        bool hasHitLine = false;
        bool gameLost = false;

        int scoreCount = 0;
        int bottomOfTopPipe = 0;
        int topOfBottomPipe = 0;
        int rightOfPipe = 1000;
        

        Texture2D pixel;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            gameTexture = Content.Load<Texture2D>("flappyBirdSpriteSheet");
            flappyBird = new Bird(gameTexture, Vector2.One, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), new Vector2(3f, 3f), Color.White, SpriteEffects.None, 0, 0, new Vector2(3, 3), new FrameHelper(new Rectangle[] { new Rectangle(31, 491, 17, 12), new Rectangle(3, 491, 17, 12), new Rectangle(59, 491, 17, 12) }, -1));
            //flappyBird.currentFrames = new FrameHelper(new Rectangle[] { new Rectangle(31, 491, 17, 12), new Rectangle(3, 491, 17, 12), new Rectangle(59, 491, 17, 12) }, -1);
            backgroundTexture = Content.Load<Texture2D>("flappyBirdSpriteSheet");
            pipesTexture = Content.Load<Texture2D>("flappyBirdSpriteSheet");
            spriteFont = Content.Load<SpriteFont>("Font");
            background = new Background(backgroundTexture, Vector2.One, Vector2.Zero, new Vector2(2f, 2f), Color.White, default, default, default);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            // TODO: use this.Content to load your game content here
        }



        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.GetPressedKeyCount() > 5) scoreCount = 1000;

            if (gameLost == false)
            {
                pipeTimer += gameTime.ElapsedGameTime;

              flappyBird.Update(gameTime);

                if (pipeTimer.TotalMilliseconds >= 2000)
                {
                    pipes.Add(new Pipes(pipesTexture, new Vector2(GraphicsDevice.Viewport.Width, ran.Next(0, 200)), new Rectangle(56, 323, 26, 160), new Vector2(2f, 2f), Color.White, SpriteEffects.FlipVertically, 0, 0, 3));
                    pipes.Add(new Pipes(pipesTexture, new Vector2(GraphicsDevice.Viewport.Width, ran.Next(300, GraphicsDevice.Viewport.Height)), new Rectangle(56, 323, 26, 160), new Vector2(2f, 2f), Color.White, SpriteEffects.None, 0, 0, 3));
                    hasHitLine = false;


                    while (((pipes[pipes.Count - 2].Position.Y - pipes[pipes.Count - 2].ScaledSize.Y / 2) - (pipes[pipes.Count - 1].Position.Y + pipes[pipes.Count - 1].ScaledSize.Y / 2)) < PipeDistance)
                    {
                        pipes[pipes.Count - 1].Position.Y -= 1;
                        pipes[pipes.Count - 2].Position.Y += 1;
                    }
                    bottomOfTopPipe = (int)(pipes[pipes.Count - 2].Position.Y - pipes[pipes.Count - 2].ScaledSize.Y / 2);
                    topOfBottomPipe = (int)(pipes[pipes.Count - 1].Position.Y + pipes[pipes.Count - 1].ScaledSize.Y / 2);

                    pipeTimer = TimeSpan.Zero;
                }
                if (pipes.Count > 0)
                {

                    rightOfPipe = (int)(pipes[pipes.Count - 1].Position.X - (pipes[pipes.Count - 1].ScaledSize.X / 2));
                }

                if ((flappyBird.Position.X + (flappyBird.ScaledSize.X / 2) >= rightOfPipe) && hasHitLine == false)
                {
                    scoreCount++;
                    rightOfPipe = GraphicsDevice.Viewport.Width;
                    hasHitLine = true;
                }

                for (int i = 0; i < pipes.Count; i++)
                {
                    if (pipes.Count >= 1 && flappyBird.Hitbox.Intersects(pipes[i].Hitbox))
                    {
                        gameLost = true;
                    }

                }

                if(flappyBird.Position.Y > GraphicsDevice.Viewport.Height || flappyBird.Position.Y < 0)
                {
                    gameLost = true;
                }

               


                for (int i = 0; i < pipes.Count; i++)
                {

                    pipes[i].Update(gameTime);
                }
            }

            if (gameLost == true)
            { 
                flappyBird.MoveAfterDead(gameTime);
                if (keyboardState.IsKeyDown(Keys.R))
                 {
                    scoreCount = 0;
                    pipeTimer = TimeSpan.Zero;
                    pipes.Clear();
                    flappyBird.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

                    flappyBird.verticalVelocity = -7;
                    
                    
                    gameLost = false;
                }
            } 
           

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
          
            background.Draw(spriteBatch);
            flappyBird.Draw(spriteBatch);


            spriteBatch.Draw(pixel, new Rectangle(0, bottomOfTopPipe, GraphicsDevice.Viewport.Width, 2), Color.Red); //bottom of the top pipe
            spriteBatch.Draw(pixel, new Rectangle(0, topOfBottomPipe, GraphicsDevice.Viewport.Width, 2), Color.Blue); //top of the bottom pipe
            spriteBatch.Draw(pixel, new Rectangle(rightOfPipe, 0, 2, GraphicsDevice.Viewport.Height), Color.Purple); //right of the pipes




            if (gameLost == true)
            {
                spriteBatch.DrawString(spriteFont, "you lost | press R to restart", new Vector2(GraphicsDevice.Viewport.Width / 2 - 340, GraphicsDevice.Viewport.Height / 2), Color.Black);
            }

            for (int i = 0; i < pipes.Count; i++)
            {
                pipes[i].Draw(spriteBatch);
                //spriteBatch.Draw(pixel, pipes[i].Hitbox, Color.Red);
            }
  spriteBatch.DrawString(spriteFont, $"{scoreCount}", new Vector2(GraphicsDevice.Viewport.Width / 2, 100), Color.Black);
         //    spriteBatch.Draw(pixel, flappyBird.Hitbox, Color.Red);
            // pipes.Draw(_spriteBatch);

            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}