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

        private List<Sprite> _headers;
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
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Animations

            var animations = new Dictionary<string, Animation>()
            {
              { "runLeft", new Animation(Content.Load<Texture2D>("Player/runLeft"), 12) },
              { "runRight", new Animation(Content.Load<Texture2D>("Player/runRight"), 12) },
              { "jump", new Animation(Content.Load<Texture2D>("Player/jump"), 1) },
              { "fall", new Animation(Content.Load<Texture2D>("Player/fall"), 1) },
              { "idle", new Animation(Content.Load<Texture2D>("Player/idle"), 11) },
              { "idlePig", new Animation(Content.Load<Texture2D>("Enemies/pig/Idle (34x28)"), 11) },
              { "fallPig", new Animation(Content.Load<Texture2D>("Enemies/pig/Fall (34x28)"), 1) },
              { "runPig", new Animation(Content.Load<Texture2D>("Enemies/pig/Run (34x28)"), 6) },
            };

            #endregion

            #region Sprites

            _headers = new List<Sprite>()
            {
                new Sprite(Content.Load<Texture2D>("Controls/logo"), new Vector2(800, 250), CollisionTypes.None),
                new Sprite(Content.Load<Texture2D>("Controls/pause"), new Vector2(800, 250), CollisionTypes.None),
            };

            var platformTexture = Content.Load<Texture2D>("Platform/platform");

            _sprites = new List<Sprite>()
            {
                new Player(new Dictionary<string, Animation>()
                {
                    { "runLeft", new Animation(Content.Load<Texture2D>("Player/runLeft"), 12) },
                    { "runRight", new Animation(Content.Load<Texture2D>("Player/runRight"), 12) },
                    { "jump", new Animation(Content.Load<Texture2D>("Player/jump"), 1) },
                    { "fall", new Animation(Content.Load<Texture2D>("Player/fall"), 1) },
                    { "idle", new Animation(Content.Load<Texture2D>("Player/idle"), 11) },
                }) { Position = new Vector2(120, 500-47), CollisionType = CollisionTypes.Full },
                new Enemy(new Dictionary<string, Animation>()
                {
                    { "idlePig", new Animation(Content.Load<Texture2D>("Enemies/pig/Idle (34x28)"), 11) },
                    { "fallPig", new Animation(Content.Load<Texture2D>("Enemies/pig/Fall (34x28)"), 1) },
                    { "runPig", new Animation(Content.Load<Texture2D>("Enemies/pig/Run (34x28)"), 6) },
                }) { Position = new Vector2(600, 450-165), CollisionType = CollisionTypes.Full },
                new Sprite(platformTexture, new Vector2(300, 600), CollisionTypes.Full), 
                new Sprite(platformTexture, new Vector2(700, 450), CollisionTypes.Full),
                new Sprite(platformTexture, new Vector2(1200, 300), CollisionTypes.Full),
                new Sprite(platformTexture, new Vector2(1200, 700), CollisionTypes.Full),          
            };

            #endregion

            #region Buttons

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

            _player = (Player)_sprites[0];

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

                    foreach (var sprite in _sprites)
                        sprite.Update(gameTime);
                    CheckCollision(gameTime);
                    foreach (var sprite in _sprites)
                        sprite.ApplyPhysics(gameTime);

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
                        sprite.Draw(gameTime, spriteBatch);
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

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in collidableSprites)
                {
                    if (spriteA == spriteB)
                        continue;

                    if (spriteA.WillIntersect(spriteB))
                        spriteA.OnCollide(spriteB);
                }
            }
        }

        //public static bool IntersectsPixel(Rectangle rect1, Color[] data1, Rectangle rect2, Color[] data2)
        //{
        //    int top = Math.Max(rect1.Top, rect2.Top);
        //    int bottom = Math.Max(rect1.Bottom, rect2.Bottom);
        //    int left = Math.Max(rect1.Left, rect2.Left);
        //    int right = Math.Max(rect1.Right, rect2.Right);

        //    for (int y = top; y < bottom; y++)
        //        for (int x = left; x < right; x++)
        //        {
        //            Color colour1 = data1[(x - rect1.Left) + (y - rect1.Top) * rect1.Width];
        //            Color colour2 = data2[(x - rect2.Left) + (y - rect2.Top) * rect1.Width];

        //            if (colour1.A != 0 && colour2.B != 0)
        //                return true;
        //        }
        //    return false;
        //}
    }
}
