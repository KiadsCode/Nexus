#region Copyright
/*
 * Copyright KiadsCode
 * Nexus framework engine v1.3.6
 */
#endregion
using Tao.OpenGl;

namespace Nexus.Framework.Graphics
{
    public class Texture2D
    {
        private int _id;
        public int ID => _id;
        public int Height { get; }
        public int Width { get; }

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