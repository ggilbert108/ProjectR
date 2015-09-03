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

        public void draw(SpriteBatch spriteBatch, SpriteFont font, int screenWidth, int screenHeight)
        {
            drawMsgLog(spriteBatch, font ,screenWidth, screenHeight);
        }

        private void drawMsgLog(SpriteBatch spriteBatch, SpriteFont font, int screenWidth, int screenHeight)
        {
            const int messageDeltaY = 10;
            const int messageLogHeight = messageDeltaY * 4; //always set to the number of messages + 1

            const int x = 10;
            int y = screenHeight - messageLogHeight;

            Rectangle msgLogBounds = new Rectangle(0, y, screenWidth, messageLogHeight);
            spriteBatch.Draw(Game1.hudBox, msgLogBounds, Color.White);

            foreach (string message in MessageLog.messages)
            {
                if (message != null)
                {
                    spriteBatch.DrawString(font, message, new Vector2(x, y), Color.Black);
                }
                y += messageDeltaY;
            }
        }
    }
}
