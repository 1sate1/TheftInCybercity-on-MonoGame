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

        public readonly List<Platform> _platforms = new();

        Stat Stat = Stat.Menu;
        private List<Component> _buttons;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            IsMouseVisible = true;
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

            var animations = new Dictionary<string, Animation>()
            {
              { "runLeft", new Animation(Content.Load<Texture2D>("Player/runLeft"), 12) },
              { "runRight", new Animation(Content.Load<Texture2D>("Player/runRight"), 12) },
              { "jump", new Animation(Content.Load<Texture2D>("Player/jump"), 1) },
              { "fall", new Animation(Content.Load<Texture2D>("Player/fall"), 1) },
              { "idle", new Animation(Content.Load<Texture2D>("Player/idle"), 11) },
            };

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
                Position = new Vector2(450, 450),
            };

            #endregion

            #region Platforms

            _platforms.Add(new Platform(Content.Load<Texture2D>("Platform/platform"), new Vector2(300, 500)));
            _platforms.Add(new Platform(Content.Load<Texture2D>("Platform/platform"), new Vector2(600, 400)));
            _platforms.Add(new Platform(Content.Load<Texture2D>("Platform/platform"), new Vector2(900, 500)));

            #endregion

            #region MenuButtons

            var playButton = new Button(Content.Load<Texture2D>("Controls/playGame"), new Vector2(70, 525));
            playButton.Click += PlayButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/quitGame"), new Vector2(70, 700));
            quitButton.Click += QuitButton_Click;

            _buttons = new List<Component>()
            {
              playButton,
              quitButton,
            };

            #endregion
            
        }

        #region ClickEvents

        private void PlayButton_Click(object sender, System.EventArgs e)
        {
            Stat = Stat.Game;
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }

        #endregion

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            switch (Stat)
            {
                case Stat.Menu:
                    foreach (var button in _buttons)
                        button.Update(gameTime);
                    break;
                case Stat.Pause:
                    Exit();
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
            GraphicsDevice.Clear(Color.MintCream);
            spriteBatch.Begin();

            switch (Stat)
            {
                case Stat.Menu:
                    foreach (var component in _buttons)
                        component.Draw(gameTime, spriteBatch);
                    break;
                case Stat.Game:
                    foreach (var _platform in _platforms)
                        _platform.Draw(gameTime, spriteBatch);
                    _player.Draw(gameTime, spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
