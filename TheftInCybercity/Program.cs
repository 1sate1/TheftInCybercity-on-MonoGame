﻿namespace TheftInCybercity
{
    public class Program
    {
        public static void Main()
        {
            using(Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}