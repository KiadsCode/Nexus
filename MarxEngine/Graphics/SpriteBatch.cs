using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Nexus.Framework.Graphics
{
    public class SpriteBatch
    {
        private Game _game;

        public SpriteBatch(Game game)
        {
            _game = game;
        }
        private void DrawHitBoxBase(Color color, float left, float right, float top, float bottom)
        {
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glVertex2f(left, top);
            Gl.glVertex2f(left, bottom);
            Gl.glVertex2f(right, bottom);
            Gl.glVertex2f(right, top);
            Gl.glColor4f(1.0f, 1.0f, 1.0f, 1.0f);
        }
        public void DrawHitBox(HitBox hitBox, Color color, bool linesType)
        {
            Vector2 gameSize = new Vector2(_game.Width, _game.Height);
            float Left = hitBox.Left;
            float Right = hitBox.Right;
            float Top = hitBox.Top;
            float Bottom = hitBox.Bottom;
            float left = Left / gameSize.X;
            float top = Top / gameSize.Y;
            float bottom = Bottom / gameSize.Y;
            float right = Right / gameSize.X;
            Gl.glLoadIdentity();
            if (linesType)
            {
                Gl.glLineWidth(3);
                Gl.glBegin(Gl.GL_LINE_LOOP);
                DrawHitBoxBase(color, left, right, top, bottom);
                Gl.glEnd();
                Gl.glLineWidth(1);
            }
            else
            {
                Gl.glBegin(Gl.GL_QUADS);
                DrawHitBoxBase(color, left, right, top, bottom);
                Gl.glEnd();
            }
            Gl.glLoadIdentity();
        }
        public void DrawCircle(Vector2 position, float radius, bool fill, Color color)
        {
            float xPos = position.X;
            float yPos = position.Y;
            float xx = xPos / _game.Width;
            float yy = yPos / _game.Width;
            float cx = xx;
            float cy = yy;
            const int polygons = 25;
            radius = radius / 100f;
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glLoadIdentity();
            if (fill)
                Gl.glBegin(Gl.GL_POLYGON);
            else
                Gl.glBegin(Gl.GL_LINE_LOOP);
            for (int ii = 0; ii < polygons; ii++)
            {
                float pi = Convert.ToSingle(Math.PI);
                float theta = 2.0f * pi * (float)ii / (float)polygons;
                float x = radius * (float)Math.Sin(theta);
                float y = radius * (float)Math.Cos(theta);
                Gl.glVertex2f(x + cx, y + cy);
            }
            Gl.glEnd();
            Gl.glColor4d(1.0f, 1.0f, 1.0f, 1.0f);
            Gl.glLoadIdentity();
        }
        public void DrawRectangle(Vector2 position, float width, float height, Color color, bool fill = true)
        {
            float x = position.X;
            float y = position.Y;
            float xx = x / _game.Width;
            float yy = y / _game.Height;
            float yyy = (y - height) / _game.Height;
            float xxx = (x + width) / _game.Width;

            if (fill == false)
            {
                Gl.glLineWidth(3);
                Gl.glBegin(Gl.GL_LINE_LOOP);
            }
            else
                Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0);  Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0); Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0); Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0); Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
        }
        
        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            float x = position.X;
            float y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            float xx = x / _game.Width;
            float yy = y / _game.Height;
            float yyy = (y - height) / _game.Height;
            float xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0); Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0); Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0); Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0); Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }
        public void Draw(Texture2D texture, Vector2 position, Color color, float rotation)
        {
            float x = position.X;
            float y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            float xx = x / _game.Width;
            float yy = y / _game.Height;
            float yyy = (y - height) / _game.Height;
            float xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glRotatef(rotation, 0.0f, 0.0f, 1.0f);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0); Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0); Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0); Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0); Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glRotatef(0, 0.0f, 0.0f, 0.0f);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }
        public void Draw(Texture2D texture, Vector2 position, Color color, float rotation, float scale)
        {
            float x = position.X;
            float y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            float xx = x / _game.Width;
            float yy = y / _game.Height;
            float yyy = (y - height) / _game.Height;
            float xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glRotatef(rotation, 0.0f, 0.0f, 1.0f);
            Gl.glScalef(scale, scale, 1.0f);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0); Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0); Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0); Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0); Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glRotatef(0, 0.0f, 0.0f, 0.0f);
            Gl.glScalef(1.0f, 1.0f, 1.0f);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }
        public void Draw(Texture2D texture, Vector2 position, Color color, float rotation, Vector2 scale)
        {
            float x = position.X;
            float y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            float xx = x / _game.Width;
            float yy = y / _game.Height;
            float yyy = (y - height) / _game.Height;
            float xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glRotatef(rotation, 0.0f, 0.0f, 1.0f);
            Gl.glScalef(scale.X, scale.Y, 1.0f);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0); Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0); Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0); Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0); Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glRotatef(0, 0.0f, 0.0f, 0.0f);
            Gl.glScalef(1.0f, 1.0f, 1.0f);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }
        public void Draw(Texture2D texture, Vector2 position)
        {
            float x = position.X;
            float y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            float xx = x / _game.Width;
            float yy = y / _game.Height;
            float yyy = (y - height) / _game.Height;
            float xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glBegin(Gl.GL_POLYGON);
            Color color = Color.White;
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0); Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0); Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0); Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0); Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }
    }
}
