using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class Background : Sprite
    {
        public Background(Texture2D texture, Vector2 origin, Vector2 position, Vector2 scale, Color color, SpriteEffects effects, float rotation, float layerDepth) : base(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), scale, color, effects, rotation, layerDepth)
        {
        }
    }
}
