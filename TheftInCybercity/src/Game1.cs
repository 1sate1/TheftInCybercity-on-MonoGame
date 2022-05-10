using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        protected SpriteBatch spriteBatch;

        protected List<Object> _headers;
        protected List<Object> _sprites;
        protected Player _player;

        protected Song _music;

        Stat Stat = Stat.Menu;
        protected List<Component> _menuButtons;
        protected List<Component> _pauseButtons;

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

            #region Music

            _music = Content.Load<Song>("music");
            MediaPlayer.Play(_music);
            MediaPlayer.Volume = (float)0.05;
            MediaPlayer.IsRepeating = true;

            #endregion

            #region Objects

            #region LoadObjects

            var logo = Content.Load<Texture2D>("Controls/logo");
            var pause = Content.Load<Texture2D>("Controls/pause");

            var spawnC = Content.Load<Texture2D>("Objects/spawnC");
            var spawnNC = Content.Load<Texture2D>("Objects/spawnNC");
            var p1x1 = Content.Load<Texture2D>("Objects/1x1");
            var p2x1 = Content.Load<Texture2D>("Objects/2x1");
            var p3x1 = Content.Load<Texture2D>("Objects/3x1");
            var p4x1 = Content.Load<Texture2D>("Objects/4x1");
            var p5x1 = Content.Load<Texture2D>("Objects/5x1");
            var p6x1 = Content.Load<Texture2D>("Objects/6x1");
            var w1x2 = Content.Load<Texture2D>("Objects/1x2");
            var w1x3 = Content.Load<Texture2D>("Objects/1x3");
            var w1x4 = Content.Load<Texture2D>("Objects/1x4");
            var w1x5 = Content.Load<Texture2D>("Objects/1x5");
            var o2x2 = Content.Load<Texture2D>("Objects/2x2");

            var player = new AnimatedSprite(Content.Load<SpriteSheet>("Player/player128.sf", new JsonContentLoader()));

            #endregion

            _headers = new List<Object>()
            {
                new Object(logo, new Vector2(76, 20), CollisionTypes.None),
                new Object(pause, new Vector2(202, 70), CollisionTypes.None),
            };

            _sprites = new List<Object>()
            {
                new Object(spawnNC, new Vector2(3, 527), CollisionTypes.None),
                new Object(spawnC, new Vector2(0, 669), CollisionTypes.Full),
                new Object(p5x1, new Vector2(300, 594), CollisionTypes.Full),
                new Object(p3x1, new Vector2(700, 454), CollisionTypes.Full),
                new Object(p5x1, new Vector2(300, 300), CollisionTypes.Full),
            };

            _player = new Player(player, new Vector2(45, 605), CollisionTypes.Full);

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

                    _player.Update(gameTime);
                    CheckCollision(gameTime);
                    _player.ApplyPhysics();
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
                    _player.Draw(gameTime, spriteBatch);
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

            foreach (var sprite in collidableSprites)
            {
                if (_player.WillIntersect(sprite))
                    _player.OnCollide(sprite);
            }
        }
    }
}
