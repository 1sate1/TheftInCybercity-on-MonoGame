//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using MonoGame.Extended.Sprites;

//namespace TheftInCybercity
//{
//    public class Enemy : Sprite
//    {
//        protected bool _onGround;

//        public Enemy(AnimatedSprite sprite, Vector2 position, CollisionTypes collisionType) : base(sprite, position, collisionType)
//        {
//            sprite.Play("idle");
//        }

//        public override void Update(GameTime gameTime)
//        {
//            SetAnimations();
//            _sprite.Update(gameTime);
//        }

//        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) =>
//            spriteBatch.Draw(_sprite, Position);

//        public override void OnCollide(Sprite sprite)
//        {
//            var onTop = this.WillIntersectTop(sprite);
//            var onLeft = this.WillIntersectLeft(sprite);
//            var onRight = this.WillIntersectRight(sprite);
//            var onBotton = this.WillIntersectBottom(sprite);

//            if (onTop)
//            {
//                _onGround = true;
//                _velocity.Y = sprite.CollisionBox.Top - this.CollisionBox.Bottom;
//            }
//            else if (onLeft && _velocity.X > 0)
//                _velocity.X = 0;
//            else if (onRight && _velocity.X < 0)
//                _velocity.X = 0;
//            else if (onBotton)
//                _velocity.Y = 1;
//        }
        
//        public override void ApplyPhysics(GameTime gameTime)
//        {
//            if (!_onGround)
//                _velocity.Y += 0.3f;
//            _onGround = false;
//            Position += _velocity;
//        }

//        protected void SetAnimations()
//        {
//            var animation = "idle";

//            if (_velocity.X > 0 && _velocity.Y == 0)
//                animation = "runRight";
//            else if (_velocity.X < 0 && _velocity.Y == 0)
//                animation = "runLeft";
//            else if (_velocity.X == 0 && _velocity.Y == 0)
//                animation = "idle";
//            else if (_velocity.Y < 0)
//                animation = "jump";
//            else if (_velocity.Y > 0)
//                animation = "fall";

//            _sprite.Play(animation);
//        }
//    }
//}
