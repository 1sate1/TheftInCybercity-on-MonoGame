using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace TheftInCybercity
{
    public class Sprite : Component
    {
        public Texture2D _texture;
        public Vector2 _position;
        public CollisionTypes CollisionType;
        protected Vector2 _origin;
        public Vector2 _velocity;

        public RectangleF CollisionBox { get { return new RectangleF(Position.X + 35, Position.Y, _texture.Width - 75, _texture.Height); } }

        public Vector2 Velocity { get { return _velocity; } }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Sprite(Texture2D texture, Vector2 position, CollisionTypes collisionType)
        {
            _texture = texture;
            _position = position;
            CollisionType = collisionType;
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) =>
            spriteBatch.Draw(_texture, _position, Color.White);
    }
}
