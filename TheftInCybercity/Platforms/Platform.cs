using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheftInCybercity
{
#nullable disable
    public class Platform : Component
    {
        public Texture2D _texture;
        public Vector2 Position;
        public Rectangle _rectangle;

        public Platform(Texture2D newTexture, Vector2 newPosition) 
        {
            _texture = newTexture;
            Position = newPosition;
            _rectangle = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); 
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, _rectangle, Color.White);

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
