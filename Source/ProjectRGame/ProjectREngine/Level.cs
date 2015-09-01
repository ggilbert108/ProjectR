using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectREngine
{
    public class Level
    {
        private Dictionary<Location, Tile> _tiles;
        private Dictionary<Location, Actor> _actors; 

        private Hero _hero;

        public Level()
        {
            _tiles = new Dictionary<Location, Tile>();
            _actors = new Dictionary<Location, Actor>();

            //TEST CODE
            _hero = new Hero();
            
            addTile(new Tile(false), new Location(1, 1));
            addActor(_hero, new Location(0, 0));
            //END TEST CODE
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
            return _actors.ContainsKey(location) ? _actors[location] : null;
        }

        private void addTile(Tile tile, Location location)
        {
            tile.location = location;
            _tiles.Add(location, tile);
        }

        private void addActor(Actor actor, Location location)
        {
            actor.location = location;
            _actors.Add(location, actor);
        }
    }
}
