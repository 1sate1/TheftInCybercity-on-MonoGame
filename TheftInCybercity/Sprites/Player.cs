using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheftInCybercity
{
#nullable disable
    public class Player : Sprite
    {
        #region Fields

        protected bool _onGround;
        public bool _hasJumped;
        public bool _hasDead;

        #endregion

        #region Methods

        public Player(Texture2D texture, Vector2 position, CollisionTypes collisionType) : base(texture, position, collisionType) { _hasDead = false; }

        public Player(Dictionary<string, Animation> animations) : base(animations) { }       

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
        }

        protected void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))            
                _velocity.X = -3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))            
                _velocity.X = 3f;           
            else 
                _velocity.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && _hasJumped == false)
                _hasJumped = true;
        }

        public override void OnCollide(Sprite sprite)
        {
            var onTop = this.WillIntersectTop(sprite);
            var onLeft = this.WillIntersectLeft(sprite);
            var onRight = this.WillIntersectRight(sprite);
            var onBotton = this.WillIntersectBottom(sprite);

            if (onTop)
            {
                _onGround = true;
                _velocity.Y = sprite.CollisionBox.Top - this.CollisionBox.Bottom;
            }
            else if (onLeft && _velocity.X > 0)
                _velocity.X = 0;
            else if (onRight && _velocity.X < 0)
                _velocity.X = 0;
            else if (onBotton)
                _velocity.Y = 1;
        }

        public override void ApplyPhysics(GameTime gameTime)
        {
            if (!_onGround)
                _velocity.Y += 0.3f;

            if (_onGround && _hasJumped)
                _velocity.Y = -10f;

            _onGround = false;
            _hasJumped = false;

            Position += _velocity;
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
