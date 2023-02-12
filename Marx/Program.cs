using Nexus.Framework;
using Nexus.Framework.Audio;
using Nexus.Framework.Graphics;
using Nexus.Framework.Input;

namespace NexusFrameWorkTest
{
    internal class GGame : Game
    {
        private Player _player;
        private Texture2D _textureB;
        private WavSound _sound;

        protected override void Draw()
        {
            SpriteBatch.Draw(_textureB, new Vector2(100, 100));
            _player.Draw();
            base.Draw();
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            _textureB = Content.Load<Texture2D>(@"illuminati.png");
            _sound = Content.Load<WavSound>(@"typeSnd.wav");
            _sound.IsLooped = false;
            _player = new Player(_textureB, this);
            Components.Add(_player);
            base.LoadContent();
        }

        private readonly KeyboardState[] _keyboardStates = new KeyboardState[2];

        protected override void Update()
        {
            _keyboardStates[0] = Keyboard.GetState();
            if (_keyboardStates[0].IsKeyDown(Keys.F) && _keyboardStates[1].IsKeyUp(Keys.F))
                ToggleFullscreen();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(_keyboardStates[0].IsKeyDown(Keys.V) && _keyboardStates[1].IsKeyUp(Keys.V))
                _sound.Play();
            _keyboardStates[1] = Keyboard.GetState();
            base.Update();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var game = new GGame();
            game.Run();
        }
    }
}