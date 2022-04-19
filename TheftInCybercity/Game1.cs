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

        #region ForMenu

        Stat Stat = Stat.Menu;
        private List<Component> _buttons;

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
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Player

            _player = new Player()
            {
                _texture = Content.Load<Texture2D>("box"),
                Position = new Vector2(400, 450),
                _startY = Position.Y,
                _jumping = false,
                _jumpspeed = 0,
            };

            #endregion

            #region MenuButtons

            var playButton = new Button()
            {
                _texture = Content.Load<Texture2D>("Controls/playGame"),
                Position = new Vector2(x: 70, y: 525),
            };

            playButton.Click += PlayButton_Click;

            var quitButton = new Button()
            {
                _texture = Content.Load<Texture2D>("Controls/quitGame"),
                Position = new Vector2(x: 70, y: 700),
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

                    _player.Update(gameTime);
                                  
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
                    _player.Draw(gameTime, spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
