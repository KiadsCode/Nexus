using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using Tao.Platform;
using Nexus.Framework.Graphics;
using Nexus.Framework.Content;

namespace Nexus.Framework
{
    public class Game
    {
        private ContentManager _contentManager;
        private bool _isContentLoaded = false;
        private int _windowValuePtr = 0;
        private SpriteBatch _spriteBatch;
        private bool _isRunning = false;
        private string _title = "Nexus framework Window";
        private int _positionX = 100;
        private int _positionY = 100;
        private int _width = 800;
        private int _height = 480;
        private bool _fullScreen;
        private int _prevWidth = 800;
        private int _prevHeight = 480;
        private Color _backGroundColor = Color.CornflowerBlue;
        public SpriteBatch SpriteBatch
        {
            get
            {
                return _spriteBatch;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            protected set
            {
                _title = value;
                Glut.glutSetWindowTitle(value);
            }
        }
        public int Width
        {
            get
            {
                return _width;
            }
            protected set
            {
                _width = value;
                Glut.glutReshapeWindow(value, _height);
            }
        }
        public int X
        {
            get
            {
                return _positionX;
            }
            protected set
            {
                _positionX = value;
                Glut.glutPositionWindow(value, _positionY);
            }
        }
        public int Y
        {
            get
            {
                return _positionY;
            }
            protected set
            {
                _positionY = value;
                Glut.glutPositionWindow(_positionX, value);
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }
            protected set
            {
                _height = value;
                Glut.glutReshapeWindow(_width, value);
            }
        }
        public Color BackGroundColor
        {
            get
            {
                return _backGroundColor;
            }
            protected set
            {
                _backGroundColor = value;
            }
        }

        public ContentManager Content { get { return _contentManager; } }

        public void Run()
        {
            if (_isRunning == false)
            {
                Glut.glutInit();
                Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE);
                Glut.glutInitWindowPosition(100, 100);
                Glut.glutInitWindowSize(800, 480);
                _windowValuePtr = Glut.glutCreateWindow(_title);
                Glut.glutDisplayFunc(PrivateDraw);
                Glut.glutIdleFunc(IdleFunction);
                Glut.glutReshapeFunc(PrivateReshape);
                Initialize();
                _isRunning = true;
                Glut.glutMainLoop();
            }
        }

        protected void ToggleFullscreen()
        {
            _fullScreen = !_fullScreen;
            if(_fullScreen)
            {
                _fullScreen = true;
                _prevWidth = _width;
                _prevHeight = _height;
                Glut.glutFullScreen();
            }
            else
            {
                Glut.glutReshapeWindow(_prevWidth, _prevHeight);
                _fullScreen = false;
            }
        }

        private void ComponentsInitialize()
        {
            _contentManager = new ContentManager();
            _spriteBatch = new SpriteBatch(this);
            LoadContent();
        }

        private void PrivateReshape(int width, int height)
        {
            Gl.glViewport(0, 0, width, height);
            _width = width;
            _height = height;
            _spriteBatch = new SpriteBatch(this);
        }

        private void IdleFunction()
        {
            Update();
        }

        private void PrivateDraw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();
            BeginDraw();
            Gl.glPushMatrix();
            if (_isContentLoaded)
                Draw();
            Gl.glPopMatrix(); 
            Gl.glFlush();
            EndDraw();
        }

        protected virtual void BeginDraw()
        {
            Gl.glClearColor(
                _backGroundColor.R,
                _backGroundColor.G,
                _backGroundColor.B,
                _backGroundColor.A);
        }

        protected virtual void Draw()
        {
        }

        protected virtual void EndDraw()
        {
        }
        protected virtual void Initialize()
        {
            ComponentsInitialize();
        }

        protected virtual void Exit()
        {
            if (_isRunning)
            {
                UnloadContent();
                Glut.glutLeaveMainLoop();
                _isRunning = false;
            }
        }

        protected virtual void LoadContent()
        {
            _isContentLoaded = true;
        }

        protected virtual void UnloadContent()
        {
            _contentManager.Unload();
        }

        protected virtual void Update()
        {
            Glut.glutPostRedisplay();
        }
    }
}
