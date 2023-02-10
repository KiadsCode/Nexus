using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;
using Tao.OpenGl;

namespace Nexus.Framework.Graphics
{
    public class Texture2D
    {
        private int _id;
        public int ID { get { return _id; } }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Texture2D(int id, int height, int width)
        {
            _id = id;
            Height = height;
            Width = width;
        }

        public void Unload()
        {
            Gl.glDeleteTextures(1, ref _id);
        }
    }
}
