using Nexus.Framework;
using Nexus.Framework.Graphics;
using Nexus.Framework.Input;

namespace NexusFrameWorkTest
{
    public class Player : GameComponent
    {
        private readonly Texture2D _texture;
        private readonly Vector2 _position = Vector2.Zero;

        public Player(Texture2D texture, Game game) : base(game)
        {
            _texture = texture;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Draw()
        {
            Game.SpriteBatch.Draw(_texture, _position);
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _position.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _position.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _position.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _position.Y += 1;
            base.Update();
        }
    }
}