using Nexus.Framework.Audio;
using Nexus.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Nexus.Framework.Content
{
    public class ContentManager
    {
        private string _rootDirectory = string.Empty;
        private Dictionary<string, object> _assetsDictionary;
        private Dictionary<string, Texture2D> _texturesDictionary;
        private Dictionary<string, WavSound> _soundsDictionary;

        public ContentManager()
        {
            _assetsDictionary = new Dictionary<string, object>();
            _texturesDictionary = new Dictionary<string, Texture2D>();
            _soundsDictionary = new Dictionary<string, WavSound>();
        }

        public string RootDirectory 
        {
            get { return _rootDirectory; }
            set 
            {
                if (value != string.Empty)
                    _rootDirectory = value;
            }
        }

        public void Unload()
        {
            foreach (Texture2D item in _texturesDictionary.Values)
                item.Unload();
            foreach (WavSound item in _soundsDictionary.Values)
                item.Stop();
            _assetsDictionary.Clear();
            _soundsDictionary.Clear();
            _texturesDictionary.Clear();
        }

        public T Load<T>(string assetName)
        {
            T file = default(T);
            object data = default(object);
            string assetPath = $"{_rootDirectory}\\{assetName}";

            if(!_assetsDictionary.ContainsKey(assetPath))
            {

                if(typeof(T) == typeof(Texture2D))
                {
                    Texture2D texture = LoadTexture2D(assetPath);
                    data = texture;
                    _texturesDictionary.Add(assetPath, texture);
                    _assetsDictionary.Add(assetPath, data);
                }
                if (typeof(T) == typeof(WavSound))
                {
                    WavSound sound = new WavSound(TitleContainer.OpenStream(assetPath));
                    data = sound;
                    _soundsDictionary.Add(assetPath, sound);
                    _assetsDictionary.Add(assetPath, data);
                }
            }
            else
                _assetsDictionary.TryGetValue(assetPath, out data);

            file = (T)data;
            return file;
        }
        private Texture2D LoadTexture2D(string assetName)
        {
            Bitmap bitmap = new Bitmap(Image.FromStream(TitleContainer.OpenStream(assetName)));
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            int Width = bitmap.Width;
            int Height = bitmap.Height;
            int _id;
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Gl.glGenTextures(1, out _id);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, _id);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_CLAMP_TO_EDGE);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_CLAMP_TO_EDGE);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, data.Width, data.Height, 0, Gl.GL_BGRA, Gl.GL_UNSIGNED_BYTE, data.Scan0);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
            bitmap.UnlockBits(data);
            return new Texture2D(_id, Height, Width);
        }
    }
}
