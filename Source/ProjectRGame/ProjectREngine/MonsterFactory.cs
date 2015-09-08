using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectREngine.Monsters;

namespace ProjectREngine
{
    public static class MonsterFactory
    {
        public static List<Monster> getDungeonMonsters(int dungeonDifficulty)
        {
            List<Monster> monsters = new List<Monster>();

            //TEMPORARY CODE

            for (int i = 0; i < 5; i++)
            {
                monsters.Add(new Skeleton());
            }

            for (int i = 0; i < 15; i++)
            {
                monsters.Add(new Bat());
            }

            //END TEMPORARY CODE

            return monsters;
        }
    }
}
