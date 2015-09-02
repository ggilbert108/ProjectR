using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;

namespace ProjectREngine
{
    public class Level
    {
        private Dictionary<Location, Tile> _tiles;
        private List<Actor> _actors;

        private int curActor;

        private Hero _hero;

        public Level()
        {
            _tiles = new Dictionary<Location, Tile>();
            _actors = new List<Actor>();

            curActor = 0;

            //TEST CODE
            _hero = new Hero();

            addTile(new Tile(false, TileType.Ground), new Location(1, 1));
            addTile(new Tile(false, TileType.Ground), new Location(1, 2));
            addTile(new Tile(false, TileType.Ground), new Location(2, 1));
            addTile(new Tile(false, TileType.Ground), new Location(2, 2));
            addActor(_hero, new Location(0, 0));

            //END TEST CODE
        }

        public void update()
        {
            Actor actor = _actors[curActor];

            bool gotAction = false;
            Action action = actor.getNextAction(ref gotAction);

            if (!gotAction)
                return;

            action.bindLevel(this);
            action.doAction();

            curActor = (curActor + 1)%_actors.Count;
        }

        public Location getHeroLocation()
        {
            return _hero.location;
        }

        public List<Entity> getEntities(Location location)
        {
            //TODO add the other entites from the other layers to the list
            List<Entity> entities = new List<Entity>()
            {
                getTile(location),
                getActor(location)
            };

            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i] == null)
                {
                    entities.RemoveAt(i);
                    i--;
                }
            }

            return entities;
        }

        public Tile getTile(Location location)
        {
            return _tiles.ContainsKey(location) ? _tiles[location] : null;
        }

        public Actor getActor(Location location)
        {
            foreach (Actor actor in _actors)
            {
                if (actor.location.Equals(location))
                    return actor;
            }
            return null;
        }

        public Hero hero
        {
            get { return _hero; }
        }

        private void addTile(Tile tile, Location location)
        {
            tile.location = location;
            _tiles.Add(tile.location, tile);
        }

        private void addActor(Actor actor, Location location)
        {
            actor.location = location;
            _actors.Add( actor);
        }
    }
}
