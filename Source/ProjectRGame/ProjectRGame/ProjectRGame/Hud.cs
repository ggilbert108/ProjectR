using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectREngine;

namespace ProjectRGame
{
    public class Hud
    {
        private Hero _hero;

        public Hud(Hero hero)
        {
            _hero = hero;
        }

        public void draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(Game1.hud, screen, Color.White);
            drawMsgLog(spriteBatch, screenWidth, screenHeight);
        }

        private void drawMsgLog(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            const int messageDeltaY = 10;
            const int messageLogHeight = messageDeltaY * 9; //always set to the number of messages + 1

            const int x = 2;
            int y = screenHeight - messageLogHeight;

            foreach (string message in MessageLog.messages)
            {
                if (message != null)
                {
                    spriteBatch.DrawString(Game1.smallFont, message, new Vector2(x, y), Color.White);
                }
                y += messageDeltaY;
            }
        }
    }
}
