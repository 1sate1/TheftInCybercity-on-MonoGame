using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheftInCybercity
{
    public class Background : Component
    {

        protected Color _color;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            _color = Color.FromNonPremultiplied(173, 232, 244, 128);
        }
    }
}
