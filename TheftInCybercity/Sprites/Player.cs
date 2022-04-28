using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheftInCybercity
{
#nullable disable
    public class Player : Sprite
    {
        #region Fields

        public bool _hasJumped;
        public bool _hasDead;

        #endregion

        #region Methods

        public Player(Dictionary<string, Animation> animations) : base(animations)
        {           
        }

        public Player(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _hasJumped = true;
            _hasDead = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else 
                throw new Exception("This ain't right..!");
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

        protected void Move()
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
