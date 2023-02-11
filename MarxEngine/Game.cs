using Nexus.Framework.Content;
using Nexus.Framework.Graphics;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace Nexus.Framework
{
    public class Game
    {
        private bool _isContentLoaded;
        private bool _isRunning;
        private string _title = "Nexus SimpleSample";
        private int _positionX = 100;
        private int _positionY = 100;
        private int _width = 800;
        private int _height = 480;
        private bool _fullScreen;
        private int _prevWidth = 800;
        private int _prevHeight = 480;
        private Color _backGroundColor = Color.CornflowerBlue;
        public SpriteBatch SpriteBatch { get; private set; }

        public string Title
        {
            get => _title;
            protected set
            {
                _title = value;
                Glut.glutSetWindowTitle(value);
            }
        }

        public int Width
        {
            get => _width;
            protected set
            {
                _width = value;
                Glut.glutReshapeWindow(value, _height);
            }
        }

        public int X
        {
            get => _positionX;
            protected set
            {
                _positionX = value;
                Glut.glutPositionWindow(value, _positionY);
            }
        }

        public int Y
        {
            get => _positionY;
            protected set
            {
                _positionY = value;
                Glut.glutPositionWindow(_positionX, value);
            }
        }

        public int Height
        {
            get => _height;
            protected set
            {
                _height = value;
                Glut.glutReshapeWindow(_width, value);
            }
        }

        public Color BackGroundColor
        {
            get => _backGroundColor;
            protected set => _backGroundColor = value;
        }

        /// <summary>
        ///     Allows user to load game assets
        ///     like textures and sounds
        /// </summary>
        public ContentManager Content { get; private set; }

        public GameComponentsCollection Components { get; private set; }

        public void Run()
        {
            if (_isRunning == false)
            {
                Glut.glutInit();
                Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE);
                Glut.glutInitWindowPosition(100, 100);
                Glut.glutInitWindowSize(800, 480);
                Glut.glutCreateWindow(_title);
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
            if (_fullScreen)
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
            Content = new ContentManager();
            SpriteBatch = new SpriteBatch(this);
            Components = new GameComponentsCollection();
            LoadContent();
        }

        private void PrivateReshape(int width, int height)
        {
            Gl.glViewport(0, 0, width, height);
            OrthographicCamSet(width, height);
            _width = width;
            _height = height;
            SpriteBatch = new SpriteBatch(this);
        }

        private void OrthographicCamSet(int width, int height)
        {
            Glu.gluOrtho2D(0, width, height, 0);
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
            Glut.glutSwapBuffers();
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
            Glut.glutPostRedisplay();
        }

        protected virtual void Initialize()
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            OrthographicCamSet(Width, Height);
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
            Content.Unload();
        }

        protected virtual void Update()
        {
            foreach (GameComponent component in Components)
                if (component.Enabled)
                    component.Update();
        }
    }
}