using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;

namespace TheftInCybercity
{
#nullable disable
    public class Platform : Component
    {
        #region Fields

        protected Texture2D _texture;
        public Vector2 _position;

        #endregion

        #region Properties

        public Rectangle Rectangle { get { return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height); } }

        #endregion

        #region Methods

        public Platform(Texture2D texture, Vector2 position) 
        {
            _texture = texture;
            _position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, Rectangle, Color.White);

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
