using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheftInCybercity
{
    public class Enemy : Sprite
    {
        protected bool _onGround;

        public Enemy(Texture2D texture, Vector2 position, CollisionTypes collisionType) : base(texture, position, collisionType) { }

        public Enemy(Dictionary<string, Animation> animations) : base(animations) { }
       
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else
                throw new Exception("This ain't right..!");
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

        public override void Update(GameTime gameTime)
        {
            SetAnimations();
            _animationManager.Update(gameTime);
        }

        public override void ApplyPhysics(GameTime gameTime)
        {
            if (!_onGround)
                _velocity.Y += 0.3f;
            _onGround = false;
            Position += _velocity;
        }

        protected virtual void SetAnimations()
        {
            if (_velocity.X < 0 && _velocity.Y == 0)
                _animationManager.Play(_animations["runPig"]);
            else if (_velocity.X == 0 && _velocity.Y == 0)
                _animationManager.Play(_animations["idlePig"]);
            else if (_velocity.Y > 0 && _velocity.X == 0)
                _animationManager.Play(_animations["fallPig"]);
            else _animationManager.Stop();
        }
    }
}
