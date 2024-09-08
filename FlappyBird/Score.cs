using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Score : Sprite
    {
        public Score(Texture2D texture, Vector2 position, Rectangle sourceRectangle, Vector2 scale, Color color, SpriteEffects effects, float rotation, float layerDepth)
           : base(texture, position, sourceRectangle, scale, color, effects, rotation, layerDepth)
        {

        }
    }
}
