using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheftInCybercity
{
#nullable disable
    public class Sprite : Component
    {
        #region Fields

        protected Texture2D _texture;
        public Vector2 _position;
        public Vector2 _velocity;

        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;

        #endregion

        #region Properties

        public Vector2 Velocity { get { return _velocity; } }

        public Rectangle CollisionBox { get {
                if (_animations != null)
                    return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().Value.FrameWidth, _animations.First().Value.FrameHeight);
                else
                    return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); } }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        #endregion

        #region Methods

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Texture2D texture, Vector2 position) 
        {
            _texture = texture;
            _position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right..!");
        }

        public override void Update(GameTime gameTime)
        {            
        }

        #endregion
    }
}
