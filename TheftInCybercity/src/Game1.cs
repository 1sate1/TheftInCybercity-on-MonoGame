using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

namespace TheftInCybercity
{
#nullable disable
    enum Stat
    {
        Menu,
        Game,
        Pause,
        Dead
    }

    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private List<Sprite> _headers;
        private List<Entity> _entities;
        private List<Sprite> _sprites;
        private Player _player;

        Stat Stat = Stat.Menu;
        private List<Component> _menuButtons;
        private List<Component> _pauseButtons;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;           
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Sprites

            var platformTexture = Content.Load<Texture2D>("Platform/platform");

            _headers = new List<Sprite>()
            {
                new Sprite(Content.Load<Texture2D>("Controls/logo"), new Vector2(76, 20), CollisionTypes.None),
                new Sprite(Content.Load<Texture2D>("Controls/pause"), new Vector2(202, 70), CollisionTypes.None),
            };

            _sprites = new List<Sprite>()
            {
                new Sprite(platformTexture, new Vector2(0, 694), CollisionTypes.Full),
                new Sprite(platformTexture, new Vector2(400, 594), CollisionTypes.Full),
                new Sprite(platformTexture, new Vector2(750, 454), CollisionTypes.Full),
                new Sprite(platformTexture, new Vector2(500, 270), CollisionTypes.Full),
                new Sprite(platformTexture, new Vector2(0, 430), CollisionTypes.Full),
            };

            _entities = new List<Entity>()
            {
                new Player(new AnimatedSprite(Content.Load<SpriteSheet>("Player/player128.sf", new JsonContentLoader())), new Vector2(45, 630), CollisionTypes.Full),
            };

            #endregion

            #region Buttons

            #region MenuButtons

            var playGameButton = new Button(Content.Load<Texture2D>("Controls/play game"), new Vector2(20, 476));
            playGameButton.Click += ResumeGameButton_Click;

            var quitGameButton = new Button(Content.Load<Texture2D>("Controls/quit"), new Vector2(20, 588));
            quitGameButton.Click += QuitGameButton_Click;

            _menuButtons = new List<Component>()
            {
                playGameButton,
                quitGameButton,
            };

            #endregion

            #region PauseButtons

            var resumeButton = new Button(Content.Load<Texture2D>("Controls/resume"), new Vector2(298, 376));
            resumeButton.Click += ResumeGameButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/quit"), new Vector2(434, 488));
            quitButton.Click += QuitGameButton_Click;

            _pauseButtons = new List<Component>()
            {
                resumeButton,
                quitButton,
            };

            #endregion

            #endregion

        }

        #region ClickEvents

        private void ResumeGameButton_Click(object sender, System.EventArgs e)
        {
            Stat = Stat.Game;
        }

        private void QuitGameButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }

        #endregion

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (Stat == Stat.Game) IsMouseVisible = false;
            else IsMouseVisible = true;

            _player = (Player)_entities[0];

            if (_player._hasDead == true)
                Stat = Stat.Dead;

            switch (Stat)
            {
                case Stat.Menu:
                    foreach (var button in _menuButtons)
                        button.Update(gameTime);
                    break;

                case Stat.Pause:
                    foreach (var button in _pauseButtons)
                        button.Update(gameTime);
                    break;

                case Stat.Game:                    
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Stat = Stat.Pause;

                    foreach (var entity in _entities)
                    {
                        entity.Update(gameTime);
                    }

                    CheckCollision(gameTime);

                    foreach (var entity in _entities)
                    {
                        entity.ApplyPhysics(gameTime);
                    }

                    break;

                case Stat.Dead:
                    Exit();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Lavender);
            spriteBatch.Begin();

            switch (Stat)
            {
                case Stat.Menu:
                    _headers[0].Draw(gameTime, spriteBatch);
                    foreach (var buttons in _menuButtons)
                        buttons.Draw(gameTime, spriteBatch);
                    break;

                case Stat.Game:
                    foreach (var sprite in _sprites)
                    {
                        sprite.Draw(gameTime, spriteBatch);
                    }

                    foreach (var entity in _entities)
                    {
                        entity.Draw(gameTime, spriteBatch);
                    }
                    break;

                case Stat.Pause:
                    _headers[1].Draw(gameTime, spriteBatch);
                    foreach (var buttons in _pauseButtons)
                        buttons.Draw(gameTime, spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void CheckCollision(GameTime gameTime)
        {
            var collidableSprites = _sprites.Where(c => c.CollisionType != CollisionTypes.None);
            var collidableEntities = _entities.Where(c => c.CollisionType != CollisionTypes.None);

            foreach (var entity in collidableEntities)
            {
                foreach (var sprite in collidableSprites)
                {
                    if (entity.WillIntersect(sprite))
                        entity.OnCollide(sprite);
                }
            }
        }
    }
}
