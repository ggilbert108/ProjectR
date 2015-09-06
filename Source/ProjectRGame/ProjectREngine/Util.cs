using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public static void shuffle<T>(T[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        public static T chooseRandomElement<T>(T[] array)
        {
            int index = random.Next(array.Length);
            return array[index];
        }
    }
}
