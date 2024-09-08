using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird
{
    internal class FrameHelper
    {
        public Rectangle[] Frames;
        public int VIPFrame;

        public FrameHelper(Rectangle[] frames, int vipFrame)
        {
            Frames = frames;
            VIPFrame = vipFrame;
        }
    }
}
