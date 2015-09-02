using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public static class Util
    {
        public static Random random;

        static Util()
        {
            random = new Random();
        }

        public static T chooseRandomElement<T>(T[] array)
        {
            int index = random.Next(array.Length);
            return array[index];
        }
    }
}
