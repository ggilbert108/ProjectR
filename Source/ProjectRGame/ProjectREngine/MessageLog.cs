using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public static class MessageLog
    {
        public static string[] messages;
        private const int NUM_VISIBLE_MESSAGES = 8;
        static MessageLog()
        {
            messages = new string[NUM_VISIBLE_MESSAGES];
        }

        public static void log(string message)
        {
            const int maxLength = 30;
            if (message.Length > maxLength)
            {
                int split = message.LastIndexOf(' ', maxLength);
                log(message.Substring(0, split));
                log(message.Substring(split + 1));
                return;
            }
            for (int i = 0; i < messages.Length - 1; i++)
            {
                messages[i] = messages[i + 1];
            }
            messages[messages.Length - 1] = message;
        }
    }
}
