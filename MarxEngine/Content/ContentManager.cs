#region Copyright
/*
 * Copyright KiadsCode
 * Nexus framework engine v1.3.6
 */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Nexus.Framework.Audio;
using Nexus.Framework.Graphics;
using Tao.OpenGl;

namespace Nexus.Framework.Content
{
    public class ContentManager
    {
        private string _rootDirectory = string.Empty;
        private readonly Dictionary<string, object> _assetsDictionary;
        private readonly Dictionary<string, Texture2D> _texturesDictionary;
        private readonly Dictionary<string, WavSound> _soundsDictionary;

        public ContentManager()
        {
            _assetsDictionary = new Dictionary<string, object>();
            _texturesDictionary = new Dictionary<string, Texture2D>();
            _soundsDictionary = new Dictionary<string, WavSound>();
        }

        public string RootDirectory
        {
            get => _rootDirectory;
            set
            {
                if (value != string.Empty)
                    _rootDirectory = value;
            }
        }

        public void Unload()
        {
            foreach (var item in _texturesDictionary.Values)
                item.Unload();
            foreach (var item in _soundsDictionary.Values)
                item.Stop();
            _assetsDictionary.Clear();
            _soundsDictionary.Clear();
            _texturesDictionary.Clear();
        }

        public T Load<T>(string assetName)
        {
            var asset = default(T);
            var data = default(object);
            var assetPath = $"{_rootDirectory}\\{assetName}";

            if (!_assetsDictionary.ContainsKey(assetPath))
            {
                if (typeof(T) == typeof(Texture2D))
                {
                    var texture = LoadTexture2D(assetPath);
                    data = texture;
                    _texturesDictionary.Add(assetPath, texture);
                    _assetsDictionary.Add(assetPath, data);
                }

                if (typeof(T) == typeof(WavSound))
                {
                    var sound = new WavSound(TitleContainer.OpenStream(assetPath));
                    data = sound;
                    _soundsDictionary.Add(assetPath, sound);
                    _assetsDictionary.Add(assetPath, data);
                }
            }
            else
            {
                _assetsDictionary.TryGetValue(assetPath, out data);
            }

            if (!(data is T))
                throw new Exception("Asset isn't same type as requested type");

            asset = (T)data;
            return asset;
        }

        private Texture2D LoadTexture2D(string assetName)
        {
            var bitmap = new Bitmap(Image.FromStream(TitleContainer.OpenStream(assetName)));
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            var Width = bitmap.Width;
            var Height = bitmap.Height;
            int _id;
            var data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            Gl.glGenTextures(1, out _id);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, _id);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_CLAMP_TO_EDGE);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_CLAMP_TO_EDGE);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, data.Width, data.Height, 0, Gl.GL_BGRA,
                Gl.GL_UNSIGNED_BYTE, data.Scan0);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);
            bitmap.UnlockBits(data);
            return new Texture2D(_id, Height, Width);
        }
    }
}