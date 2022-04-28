using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheftInCybercity
{
#nullable disable
    public enum CollisionTypes
    {
        None,
        Full,
        Top,
    }

    public class Sprite : Component
    {
        #region Fields

        public Texture2D _texture;
        public Vector2 _position;
        public Vector2 _velocity;
        protected Vector2 _origin;
        protected float _rotation;
        protected float _layer;
        public CollisionTypes CollisionType;
        public Color Colour;

        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;

        #endregion

        #region Properties

        public Vector2 Velocity { get { return _velocity; } }

        public Vector2 Origin { get { return _origin; } set { _origin = value; } }

        public float Layer { get { return _layer; } set { _layer = value; } }
       
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

        public Rectangle CollisionBox
        {
            get
            {
                if (_animations != null)
                    return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _animations.First().Value.FrameWidth, _animations.First().Value.FrameHeight);
                else
                    return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
            }
        }

        public Vector2 Centre { get { return new Vector2(CollisionBox.X + Origin.X, CollisionBox.Y + Origin.Y); } }

        public float Rotation { get { return _rotation; } set { _rotation = value; } }

        #endregion

        #region Methods

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public Sprite(Texture2D texture, Vector2 position, CollisionTypes collisionType)
        {
            _texture = texture;
            _position = position;
            CollisionType = collisionType;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);           
            Colour = Color.White;
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, null, Colour, _rotation, Origin, 1f, SpriteEffects.None, Layer);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("This ain't right..!");
        }

        public bool WillIntersect(Sprite sprite)
        {
            return this.WillIntersectBottom(sprite) ||
              this.WillIntersectLeft(sprite) ||
              this.WillIntersectRight(sprite) ||
              this.WillIntersectTop(sprite);
        }        

        #region Collision

        public virtual void OnCollide(Sprite sprite) { }

        public bool WillIntersectLeft(Sprite sprite)
        {
            return this.CollisionBox.Right + this._velocity.X >= sprite.CollisionBox.Left &&
              this.CollisionBox.Left + this._velocity.X < sprite.CollisionBox.Left &&
              this.CollisionBox.Top   /*+ this._velocity.Y */< sprite.CollisionBox.Bottom &&
              this.CollisionBox.Bottom/* + this._velocity.Y*/ > sprite.CollisionBox.Top;
        }

        public bool WillIntersectRight(Sprite sprite)
        {
            return this.CollisionBox.Left + this._velocity.X <= sprite.CollisionBox.Right &&
              this.CollisionBox.Right > sprite.CollisionBox.Right &&
              this.CollisionBox.Top   /*+ this._velocity.Y */< sprite.CollisionBox.Bottom &&
              this.CollisionBox.Bottom/* + this._velocity.Y*/ > sprite.CollisionBox.Top;
        }

        public bool WillIntersectTop(Sprite sprite)
        {
            return this.CollisionBox.Bottom + this._velocity.Y >= sprite.CollisionBox.Top &&
              this.CollisionBox.Top < sprite.CollisionBox.Top &&
              this.CollisionBox.Right/* + this._velocity.X*/ > sprite.CollisionBox.Left &&
              this.CollisionBox.Left /*+ this._velocity.Y */< sprite.CollisionBox.Right;
        }

        public bool WillIntersectBottom(Sprite sprite)
        {
            return this.CollisionBox.Top + this._velocity.Y <= sprite.CollisionBox.Bottom &&
              this.CollisionBox.Bottom > sprite.CollisionBox.Bottom &&
              this.CollisionBox.Right/* + this._velocity.X*/ > sprite.CollisionBox.Left &&
              this.CollisionBox.Left /*+ this._velocity.Y */< sprite.CollisionBox.Right;
        }

        #endregion

        public virtual void ApplyPhysics(GameTime gameTime) { }

        #endregion
    }
}
