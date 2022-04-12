using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheftInCybercity
{
    internal class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D logo;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Splashscreen.Font = Content.Load<SpriteFont>("splashfont");
            logo = Content.Load<Texture2D>("logo");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(147, 202, 246, 100));
            spriteBatch.Begin();
            Splashscreen.Draw(spriteBatch);
            spriteBatch.Draw(logo, new Vector2(, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
