using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheftInCybercity.Controls;

namespace TheftInCybercity
{
    enum Stat
    {
        Menu,
        Game,
        Pause,
        Dead
    }

    public class Game1 : Game
    {
        #region Fields

        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch = null!;

        Stat Stat = Stat.Menu;

        private List<Sprite> _sprites = null!;

        private List<Component> _buttons = null!;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Players

            var texture = Content.Load<Texture2D>("box");

            _sprites = new List<Sprite>()
            {
                new Sprite(texture)
                {
                  Position = new Vector2(100, 100),
                  Input = new Input()
                  {
                    Up = Keys.W,
                    Left = Keys.A,
                    Down = Keys.S,
                    Right = Keys.D,
                  },
                },
                new Sprite(texture)
                {
                  Position = new Vector2(200, 100),
                  Input = new Input()
                  {
                    Up = Keys.Up,
                    Left = Keys.Left,
                    Down = Keys.Down,
                    Right = Keys.Right,
                  },
                },
            };

            #endregion

            #region MenuButtons

            var playButton = new Button(Content.Load<Texture2D>("Controls/playGame"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(x: 70, y: 605),
                Text = "",
            };

            playButton.Click += PlayButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/quitGame"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(x: 70, y: 805),
                Text = "",
            };

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

                    foreach (var sprite in _sprites)
                        sprite.Update();                   
                    break;
            }            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (Stat)
            {
                case Stat.Menu:
                    foreach (var component in _buttons)
                        component.Draw(gameTime, spriteBatch);
                    break;
                case Stat.Game:
                    foreach (var sprite in _sprites)
                        sprite.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
