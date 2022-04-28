using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        private Player _player;

        private readonly List<Sprite> _platforms = new();
        private readonly List<Sprite> _headers = new();

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
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Headers

            _headers.Add(new Sprite(Content.Load<Texture2D>("Controls/logo"), new Vector2(242, 26)));
            _headers.Add(new Sprite(Content.Load<Texture2D>("Controls/pause"), new Vector2(368, 100)));

            #endregion

            #region Animations

            var animations = new Dictionary<string, Animation>()
            {
              { "runLeft", new Animation(Content.Load<Texture2D>("Player/runLeft"), 12) },
              { "runRight", new Animation(Content.Load<Texture2D>("Player/runRight"), 12) },
              { "jump", new Animation(Content.Load<Texture2D>("Player/jump"), 1) },
              { "fall", new Animation(Content.Load<Texture2D>("Player/fall"), 1) },
              { "idle", new Animation(Content.Load<Texture2D>("Player/idle"), 11) },
            };

            #endregion

            #region Player

            _player = new Player(new Dictionary<string, Animation>()
            {
              { "runLeft", new Animation(Content.Load<Texture2D>("Player/runLeft"), 12) },
              { "runRight", new Animation(Content.Load<Texture2D>("Player/runRight"), 12) },
              { "jump", new Animation(Content.Load<Texture2D>("Player/jump"), 1) },
              { "fall", new Animation(Content.Load<Texture2D>("Player/fall"), 1) },
              { "idle", new Animation(Content.Load<Texture2D>("Player/idle"), 11) },
            })
            {
                Position = new Vector2(300 - 20, 500 - 127),
            };

            #endregion

            #region Platforms

            _platforms.Add(new Sprite(Content.Load<Texture2D>("Platform/platform"), new Vector2(300, 500)));
            _platforms.Add(new Sprite(Content.Load<Texture2D>("Platform/platform"), new Vector2(600, 400)));
            _platforms.Add(new Sprite(Content.Load<Texture2D>("Platform/platform"), new Vector2(900, 500)));

            #endregion

            #region MenuButtons

            var playGameButton = new Button(Content.Load<Texture2D>("Controls/play game"), new Vector2(70, 575));
            playGameButton.Click += ResumeGameButton_Click;

            var quitGameButton = new Button(Content.Load<Texture2D>("Controls/quit"), new Vector2(70, 700));
            quitGameButton.Click += QuitGameButton_Click;

            _menuButtons = new List<Component>()
            {
              playGameButton,
              quitGameButton,
            };

            #endregion

            #region PauseButtons

            var resumeButton = new Button(Content.Load<Texture2D>("Controls/resume"), new Vector2(460, 475));
            resumeButton.Click += ResumeGameButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/quit"), new Vector2(600, 575));
            quitButton.Click += QuitGameButton_Click;

            _pauseButtons = new List<Component>()
            {
              resumeButton,
              quitButton,
            };

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

                    foreach (var _platform in _platforms)
                        if (_player.Rectangle.Intersects(_platform.Rectangle))
                        {
                        _player._velocity.Y = 0f;
                        _player._hasJumped = false;
                        }

                    _player.Update(gameTime);
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
                    foreach (var component in _menuButtons)
                        component.Draw(gameTime, spriteBatch);
                    break;
                case Stat.Game:
                    foreach (var _platform in _platforms)
                        _platform.Draw(gameTime, spriteBatch);

                    _player.Draw(gameTime, spriteBatch);
                    break;
                case Stat.Pause:
                    _headers[1].Draw(gameTime, spriteBatch);
                    foreach (var component in _pauseButtons)
                        component.Draw(gameTime, spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
