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
        public bool _hasJumped;
        public bool _hasDead;

        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;

        #endregion

        #region Properties

        public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().Value.FrameWidth, _animations.First().Value.FrameHeight); } }

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

        public Vector2 Velocity { get { return _velocity; } }

        #endregion

        #region Methods

        public Player(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Player(Texture2D texture)
        {
            _texture = texture;
            _hasJumped = true;
            _hasDead = false;
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
            Move();

            SetAnimations();
            _animationManager.Update(gameTime);

            if (Position.Y >= 900)
                _hasDead = true;

            Position += Velocity;
            _velocity = Vector2.Zero;
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A)) _velocity.X = -3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) _velocity.X = 3f;
            else _velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && _hasJumped == false)
            {
                _position.Y -= 150f;
                _velocity.Y -= 10f;
                _hasJumped = true;
            }

            if (_hasJumped == true)
            {
                float i = 1;
                _velocity.Y += 5f * i;
            }

            if (_hasJumped == false)
                _velocity.Y += 0f;
        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0 && Velocity.Y == 0)
                _animationManager.Play(_animations["runRight"]);
            else if (Velocity.X < 0 && Velocity.Y == 0)
                _animationManager.Play(_animations["runLeft"]);
            else if (Velocity.X == 0 && Velocity.Y == 0)
                _animationManager.Play(_animations["idle"]);
            else if (Velocity.Y < 0 && Velocity.X == 0)
                _animationManager.Play(_animations["jump"]);
            else if (Velocity.Y > 0 && Velocity.X == 0)
                _animationManager.Play(_animations["fall"]);
            else _animationManager.Stop();
        }

        #endregion
    }
}
