using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheftInCybercity
{
#nullable disable
    public class Player : Component
    {
        #region Fields

        public Texture2D _texture;
        public Rectangle _rectangle;
        public Vector2 Position;
        public Vector2 Velocity;            
        public bool _jumping;
         
        #endregion

        #region Methods

        public Player(Texture2D newTexture, Vector2 newPosition)
        {
            _texture = newTexture;
            Position = newPosition;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            Position += Velocity;
            _rectangle = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

            if (Keyboard.GetState().IsKeyDown(Keys.A)) Velocity.X = -3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) Velocity.X = 3f;
            else Velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && _jumping == false)
            {
                Position.Y -= 10f;
                Velocity.Y = -5f;
                _jumping = true;
            }

            float i = 1;
            Velocity.Y += 0.15f * i;
        }

        #endregion
    }
}
