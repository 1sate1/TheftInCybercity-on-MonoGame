using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheftInCybercity
{
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch = null!;

        private List<Sprite> _sprites = null!;

        private Color _backgroundColour = Color.CornflowerBlue;


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

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
                sprite.Update();

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape)) Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_backgroundColour);
            spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
