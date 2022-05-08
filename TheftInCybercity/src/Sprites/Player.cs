using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace TheftInCybercity
{
#nullable disable
    public class Player : Entity
    {
        #region Fields

        protected bool _onGround;
        public bool _hasJumped;
        public bool _hasDead;

        #endregion

        #region Methods

        public Player(AnimatedSprite sprite, Vector2 position, CollisionTypes collisionType) : base(sprite, position, collisionType)
        {
            sprite.Play("idle");
            _hasDead = false;
        }

        public override void Update(GameTime gameTime)
        {
            Move();

            SetAnimations();
            _entity.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) =>
            spriteBatch.Draw(_entity, Position);
       
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
                _velocity.Y = -11f;

            if (Position.Y >= 900)
                _hasDead = true;

            _onGround = false;
            _hasJumped = false;

            Position += _velocity;
        }

        protected void SetAnimations()
        {
            var animation = "idle";

            if (_velocity.X > 0 && _velocity.Y == 0)
                animation = "runRight";
            else if (_velocity.X < 0 && _velocity.Y == 0)
                animation = "runLeft";
            else if (_velocity.X == 0 && _velocity.Y == 0)
                animation = "idle";
            else if (_velocity.Y < 0)
                animation = "jump";
            else if (_velocity.Y > 0)
                animation = "fall";

            _entity.Play(animation);
        }

        #endregion
    }
}
