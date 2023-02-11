using System;
using Tao.OpenGl;

namespace Nexus.Framework.Graphics
{
    public class SpriteBatch
    {
        private readonly Game _game;

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
            if (hitBox == null) throw new ArgumentNullException(nameof(hitBox));
            var gameSize = new Vector2(_game.Width, _game.Height);
            var Left = hitBox.Left;
            var Right = hitBox.Right;
            var Top = hitBox.Top;
            var Bottom = hitBox.Bottom;
            var left = Left / gameSize.X;
            var top = Top / gameSize.Y;
            var bottom = Bottom / gameSize.Y;
            var right = Right / gameSize.X;
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
            if (position == null) throw new ArgumentNullException(nameof(position));
            var xPos = position.X;
            var yPos = position.Y;
            var xx = xPos / _game.Width;
            var yy = yPos / _game.Width;
            var cx = xx;
            var cy = yy;
            const int polygons = 25;
            radius = radius / 100f;
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glLoadIdentity();
            if (fill)
                Gl.glBegin(Gl.GL_POLYGON);
            else
                Gl.glBegin(Gl.GL_LINE_LOOP);
            for (var ii = 0; ii < polygons; ii++)
            {
                var pi = Convert.ToSingle(Math.PI);
                var theta = 2.0f * pi * ii / polygons;
                var x = radius * (float)Math.Sin(theta);
                var y = radius * (float)Math.Cos(theta);
                Gl.glVertex2f(x + cx, y + cy);
            }

            Gl.glEnd();
            Gl.glColor4d(1.0f, 1.0f, 1.0f, 1.0f);
            Gl.glLoadIdentity();
        }

        public void DrawRectangle(Vector2 position, float width, float height, Color color, bool fill = true)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));
            var x = position.X;
            var y = position.Y;
            var xx = x / _game.Width;
            var yy = y / _game.Height;
            var yyy = (y - height) / _game.Height;
            var xxx = (x + width) / _game.Width;

            if (fill == false)
            {
                Gl.glLineWidth(3);
                Gl.glBegin(Gl.GL_LINE_LOOP);
            }
            else
            {
                Gl.glBegin(Gl.GL_QUADS);
            }

            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0);
            Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0);
            Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0);
            Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
        }

        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            if (texture == null) throw new ArgumentNullException(nameof(texture));
            if (position == null) throw new ArgumentNullException(nameof(position));
            var x = position.X;
            var y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            var xx = x / _game.Width;
            var yy = y / _game.Height;
            var yyy = (y - height) / _game.Height;
            var xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0);
            Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0);
            Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0);
            Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }

        public void Draw(Texture2D texture, Vector2 position, Color color, float rotation)
        {
            if (texture == null) throw new ArgumentNullException(nameof(texture));
            if (position == null) throw new ArgumentNullException(nameof(position));
            var x = position.X;
            var y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            var xx = x / _game.Width;
            var yy = y / _game.Height;
            var yyy = (y - height) / _game.Height;
            var xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glRotatef(rotation, 0.0f, 0.0f, 1.0f);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0);
            Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0);
            Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0);
            Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glRotatef(0, 0.0f, 0.0f, 0.0f);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }

        public void Draw(Texture2D texture, Vector2 position, Color color, float rotation, float scale)
        {
            if (texture == null) throw new ArgumentNullException(nameof(texture));
            if (position == null) throw new ArgumentNullException(nameof(position));
            var x = position.X;
            var y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            var xx = x / _game.Width;
            var yy = y / _game.Height;
            var yyy = (y - height) / _game.Height;
            var xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glRotatef(rotation, 0.0f, 0.0f, 1.0f);
            Gl.glScalef(scale, scale, 1.0f);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0);
            Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0);
            Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0);
            Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2f(xx, yyy);
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
            if (texture == null) throw new ArgumentNullException(nameof(texture));
            if (position == null) throw new ArgumentNullException(nameof(position));
            if (scale == null) throw new ArgumentNullException(nameof(scale));
            var x = position.X;
            var y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            var xx = x / _game.Width;
            var yy = y / _game.Height;
            var yyy = (y - height) / _game.Height;
            var xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glRotatef(rotation, 0.0f, 0.0f, 1.0f);
            Gl.glScalef(scale.X, scale.Y, 1.0f);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0);
            Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0);
            Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0);
            Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glRotatef(0, 0.0f, 0.0f, 0.0f);
            Gl.glScalef(1.0f, 1.0f, 1.0f);
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }

        public void Draw(Texture2D texture, Vector2 position, Color color, float rotation, Vector2 scale,
            Vector2 origin)
        {
            if (texture == null) throw new ArgumentNullException(nameof(texture));
            if (position == null) throw new ArgumentNullException(nameof(position));
            if (scale == null) throw new ArgumentNullException(nameof(scale));
            if (origin == null) throw new ArgumentNullException(nameof(origin));
            var x = position.X;
            var y = position.Y;

            if (origin.X > 0)
                x -= origin.X;
            else
                x += origin.X;
            if (origin.Y > 0)
                y += origin.X;
            else
                y -= origin.Y;

            float width = texture.Width * 2;
            float height = texture.Height * 2;
            var xx = x / _game.Width;
            var yy = y / _game.Height;
            var yyy = (y - height) / _game.Height;
            var xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glRotatef(rotation, 0.0f, 0.0f, 1.0f);
            Gl.glScalef(scale.X, scale.Y, 1.0f);

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0);
            Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0);
            Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0);
            Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2f(xx, yyy);
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
            if (texture == null) throw new ArgumentNullException(nameof(texture));
            if (position == null) throw new ArgumentNullException(nameof(position));
            var x = position.X;
            var y = position.Y;
            float width = texture.Width * 2;
            float height = texture.Height * 2;
            var xx = x / _game.Width;
            var yy = y / _game.Height;
            var yyy = (y - height) / _game.Height;
            var xxx = (x + width) / _game.Width;

            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture.ID);

            Gl.glBegin(Gl.GL_POLYGON);
            var color = Color.White;
            Gl.glColor4f(color.R, color.G, color.B, color.A);
            Gl.glTexCoord2d(0.0, 1.0);
            Gl.glVertex2f(xx, yy);
            Gl.glTexCoord2d(1.0, 1.0);
            Gl.glVertex2f(xxx, yy);
            Gl.glTexCoord2d(1.0, 0.0);
            Gl.glVertex2f(xxx, yyy);
            Gl.glTexCoord2d(0.0, 0.0);
            Gl.glVertex2f(xx, yyy);
            Gl.glColor4f(Color.White.R, Color.White.G, Color.White.B, Color.White.A);
            Gl.glLineWidth(1);
            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisable(Gl.GL_BLEND);
        }
    }
}