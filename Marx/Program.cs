using Nexus.Framework;
using Nexus.Framework.Audio;
using Nexus.Framework.Graphics;
using Nexus.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NexusFrameWorkTest
{
    internal class GGame : Game
    {
        Texture2D texture;
        private float y;
        private float x;
        HitBox hitBox = new HitBox(-32, 32, 50, 50);
        HitBox hitBoxB = new HitBox(0, 0, 50, 50);
        WavSound snd;
        Color colro = Color.Red;

        protected override void Draw()
        {
            SpriteBatch.Draw(texture, Vector2.Zero, Color.White, 0, 1);
            SpriteBatch.DrawHitBox(hitBox, colro, false);
            base.Draw();
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            texture = Content.Load<Texture2D>(@"USA.bmp");
            hitBoxB = new HitBox(0, 0, texture.Width, texture.Height);
            snd = Content.Load<WavSound>(@"typeSnd.wav");
            snd.Play();
            base.LoadContent();
        }

        protected override void Update()
        {
            if (hitBox.Intersects(hitBoxB))
                colro = Color.White;
            else
                colro = Color.Red;
            if (Keyboard.GetState().IsKeyDown(Keys.O))
                ToggleFullscreen();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                hitBox.X -= 0.5f;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                hitBox.X += 0.5f;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                hitBox.Y -= 0.5f;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                hitBox.Y += 0.5f;
            if (Keyboard.GetState().IsKeyDown(Keys.V))
            {
                snd.Play();
            }
            base.Update();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            GGame game = new GGame();
            game.Run();
        }
    }
}
