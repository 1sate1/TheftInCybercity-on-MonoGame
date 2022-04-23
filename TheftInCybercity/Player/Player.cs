using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheftInCybercity
{
#nullable disable
    public class Player : Component
    {
        #region Fields

        protected Texture2D _texture;
        protected Vector2 _position;
        public Vector2 _velocity;            
        public bool _jumping;

        #endregion

        #region Properties

        public Rectangle Rectangle { get { return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height); } }

        #endregion

        #region Methods

        public Player(Texture2D newTexture, Vector2 newPosition)
        {
            _texture = newTexture;
            _position = newPosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, _position, Color.White);

        public override void Update(GameTime gameTime)
        {
            _position += _velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.A)) _velocity.X = -3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) _velocity.X = 3f;
            else _velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && _jumping == false)
            {
                _position.Y -= 10f;
                _velocity.Y = -5f;
                _jumping = true;
            }

            float i = 1;
            _velocity.Y += 0.15f * i;
        }

        #endregion
    }
}
