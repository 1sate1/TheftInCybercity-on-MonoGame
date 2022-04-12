using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheftInCybercity
{
    static class Splashscreen
    {
        public static Texture2D Background { get; set; }
        public static SpriteFont Font { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(Font, "Theft in Cybercity", Vector2.Zero, Color.White);
        }

        public static void Update()
        {
            
        }
    }
}
