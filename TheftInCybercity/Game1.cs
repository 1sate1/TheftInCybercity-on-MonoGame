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
        Dead,
    }

    internal class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch = default!;
        Stat Stat = Stat.Menu;

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
            Menu.Background = Content.Load<Texture2D>("background");
            Menu.Logo = Content.Load<SpriteFont>("logo");
            Menu.MenuButtons = Content.Load<SpriteFont>("menuButton");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            switch (Stat)
            {
                case Stat.Menu:
                    Menu.Update();
                    if (keyboardState.IsKeyDown(Keys.Space)) Stat = Stat.Game;
                    break;
                case Stat.Game:
                    if (keyboardState.IsKeyDown(Keys.Escape)) Stat = Stat.Menu;
                    break;                    
            }

            if (keyboardState.IsKeyDown(Keys.E)) Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(147, 202, 246, 100));
            spriteBatch.Begin();
            switch(Stat)
            {
                case Stat.Menu:
                    Menu.Draw(spriteBatch);
                    break;
                case Stat.Game:
                    //Game.Draw(spriteBatch);
                    break;
            }
                       
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
