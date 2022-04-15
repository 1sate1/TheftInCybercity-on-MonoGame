using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheftInCybercity
{
    static class Menu
    {
        public static Texture2D Background { get; set; } = default!;

        public static SpriteFont Logo { get; set; } = default!;
        static Vector2 logoPos = new(1920 - 1440, 40);

        public static SpriteFont MenuButtons{ get; set; } = default!;
        static Vector2 startButtonPos = new((1920 - 600) / 2, 1080 / 2 - 80);
        static Vector2 exitButtonPos = new((1920 - 540) / 2, 1080 / 2 + 40);

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(Logo, "theft in cybercity", logoPos, Color.White);
            
            spriteBatch.DrawString(MenuButtons, "start game", startButtonPos, Color.White);
            spriteBatch.DrawString(MenuButtons, "exit game", exitButtonPos, Color.White);
        }

        public static void Update()
        {

        }
    }
}
