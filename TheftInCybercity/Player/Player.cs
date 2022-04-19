using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheftInCybercity
{
    public class Player : Component
    {
        #region Fields

        public Texture2D _texture = null!;
        public Vector2 Position;
        public bool _jumping;
        public float _startY = 0f;
        public float _jumpspeed = 0f;

        #endregion

        #region Methods

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (_jumping)
            {
                Position.X += _jumpspeed;
                _jumpspeed += 1;
                if (Position.Y >= _startY)
                {
                    Position.Y = _startY;
                    _jumping = false;
                }
            }

            else
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    _jumping = true;
                    _jumpspeed = -14;
                }
            }
        }

        #endregion
    }
}
