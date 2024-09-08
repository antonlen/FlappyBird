using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Bird : AnimatingSprite
    {
        public Vector2 VerticalSpeed;
        private FrameHelper FrameHelper;
        public Keys JumpKey;
        const float gravity = 0.3f;
        public float verticalVelocity = 0;
        bool pressed = false;
        //const gravity
        //float vertical velocity
        //update should subtract gravity from velocity
        //jump increases velocity
        public override FrameHelper CurrentFrames { get =>  FrameHelper; }
        public Bird(Texture2D texture, Vector2 origin, Vector2 position, Vector2 scale, Color color, SpriteEffects spriteEffects, float rotation, float layerDepth, Vector2 verticalSpeed, FrameHelper frameHelper)
            : base(texture, origin, position, scale, color, spriteEffects, rotation, layerDepth)
        {
            VerticalSpeed = verticalSpeed;
            FrameHelper = frameHelper;
            JumpKey = Keys.Space;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            verticalVelocity += gravity;
            Position.Y += verticalVelocity;
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(JumpKey) && !pressed)
            {
                pressed = true;
                verticalVelocity = -7;
            }
            else if(keyboardState.IsKeyUp(JumpKey))
            { 
                pressed = false;
            }
        }

        public void MoveAfterDead(GameTime gameTime)
        {
            verticalVelocity += gravity;
            Position.Y += verticalVelocity;
            //rrrrverticalVelocity = 7;
        }
    }
}
