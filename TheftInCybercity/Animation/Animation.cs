using Microsoft.Xna.Framework.Graphics;

namespace TheftInCybercity
{
    public class Animation
    {
        #region Properties

        public int CurrentFrame;
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public float FrameSpeed;
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool IsLooping;
        public Texture2D Texture { get; private set; }

        #endregion

        #region Methods

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.05f;
        }

        #endregion
    }
}
