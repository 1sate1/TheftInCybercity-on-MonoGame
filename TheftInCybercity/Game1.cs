using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch = null!;

        #region ForPlayer

        public Texture2D _texture = null!;
        public Vector2 Position;
        public bool jumping;
        public float startY = 0f;
        public float jumpspeed = 0f;

        #endregion

        #region ForMenu

        Stat Stat = Stat.Menu;
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

            #region Player

            _texture = Content.Load<Texture2D>("box");
            Position = new Vector2(230, 450);
            startY = Position.Y;
            jumping = false;
            jumpspeed = 0;

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

                    Jump();
                                  
                    break;
            }

            base.Update(gameTime);
        }

        #region PlayerMove

        private void Jump()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (jumping)
            {
                Position.Y += jumpspeed;
                jumpspeed += 1;
                if (Position.Y >= startY)
                {
                    Position.Y = startY;
                    jumping = false;
                }
            }

            else
            {
                if (keyState.IsKeyDown(Keys.W))
                {
                    jumping = true;
                    jumpspeed = -14;
                }
            }
        }

        #endregion

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
                    spriteBatch.Draw(_texture, Position, Color.White);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
