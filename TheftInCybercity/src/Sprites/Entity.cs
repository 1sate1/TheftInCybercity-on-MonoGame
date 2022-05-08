using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;

namespace TheftInCybercity
{
    public class Entity : Component
    {
        #region Fields

        public Vector2 _position;
        public Vector2 _velocity;
        protected Vector2 _origin;
        public CollisionTypes CollisionType;

        public AnimatedSprite _entity;

        #endregion

        #region Properties

        public Vector2 Velocity { get { return _velocity; } }

        public Vector2 Origin { get { return _origin; } set { _origin = value; } }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public RectangleF CollisionBox
        {
            get
            {
                return new RectangleF(Position.X - Origin.X, Position.Y - Origin.Y, 128, 128);
            }
        }

        #endregion

        #region Methods

        public Entity(AnimatedSprite entity, Vector2 position, CollisionTypes collisionType)
        {
            _entity = entity;
            _position = position;
            CollisionType = collisionType;
            Origin = new Vector2(_entity.TextureRegion.Width / 2, _entity.TextureRegion.Height / 2);
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) =>
            spriteBatch.Draw(_entity, Position);

        public virtual void ApplyPhysics(GameTime gameTime) { }

        #region Collision

        public virtual void OnCollide(Sprite sprite) { }

        public bool WillIntersect(Sprite sprite)
        {
            return this.WillIntersectBottom(sprite) ||
              this.WillIntersectLeft(sprite) ||
              this.WillIntersectRight(sprite) ||
              this.WillIntersectTop(sprite);
        }

        public bool WillIntersectLeft(Sprite sprite)
        {
            return this.CollisionBox.Right + this._velocity.X >= sprite.CollisionBox.Left &&
              this.CollisionBox.Left + this._velocity.X < sprite.CollisionBox.Left &&
              this.CollisionBox.Top < sprite.CollisionBox.Bottom &&
              this.CollisionBox.Bottom > sprite.CollisionBox.Top;
        }

        public bool WillIntersectRight(Sprite sprite)
        {
            return this.CollisionBox.Left + this._velocity.X <= sprite.CollisionBox.Right &&
              this.CollisionBox.Right > sprite.CollisionBox.Right &&
              this.CollisionBox.Top < sprite.CollisionBox.Bottom &&
              this.CollisionBox.Bottom > sprite.CollisionBox.Top;
        }

        public bool WillIntersectTop(Sprite sprite)
        {
            return this.CollisionBox.Bottom + this._velocity.Y >= sprite.CollisionBox.Top &&
              this.CollisionBox.Top < sprite.CollisionBox.Top &&
              this.CollisionBox.Right > sprite.CollisionBox.Left &&
              this.CollisionBox.Left < sprite.CollisionBox.Right;
        }

        public bool WillIntersectBottom(Sprite sprite)
        {
            return this.CollisionBox.Top + this._velocity.Y <= sprite.CollisionBox.Bottom &&
              this.CollisionBox.Bottom > sprite.CollisionBox.Bottom &&
              this.CollisionBox.Right > sprite.CollisionBox.Left &&
              this.CollisionBox.Left < sprite.CollisionBox.Right;
        }

        #endregion

        #endregion
    }
}
