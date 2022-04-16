using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheftInCybercity
{
    public class Sprite
    {
        protected Texture2D _texture;

        public Vector2 Position;

        public float Speed = 2f;

        public Input? Input;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public void Update()
        {
            Move();            
        }

        private void Move()
        {
            if (Input == null)
                return;

            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Position.X -= Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Position.X += Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                Position.Y -= Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Down))
            {
                Position.Y += Speed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}
