using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using ProjectREngine.Items;

namespace ProjectREngine
{
    public class Level
    {
        private Dictionary<Location, Tile> _tiles;
        private List<Actor> _actors;
        private Dictionary<Location, Item> _items; 
        private Dictionary<Location, Door> _doors; 

        private int curActor;

        private Hero _hero;

        private LinkedList<EffectDescription> _effectQueue;

        public Level()
        {
            _tiles = new Dictionary<Location, Tile>();
            _actors = new List<Actor>();
            _items = new Dictionary<Location, Item>();
            _doors = new Dictionary<Location, Door>();
            _effectQueue = new LinkedList<EffectDescription>();

            curActor = 0;

            //TEST CODE
            _hero = new Hero();

            addTile(new Tile(false, TileType.Ground), new Location(1, 1));
            addTile(new Tile(false, TileType.Ground), new Location(1, 2));
            addTile(new Tile(false, TileType.Ground), new Location(2, 1));
            addTile(new Tile(true, TileType.Stone), new Location(2, 2));

            addDoor(new Door(), new Location(1, 1));

            addActor(_hero, new Location(0, 0));

            addItem(new StrengthPotion(), new Location(1, 2));
            //END TEST CODE
        }

        public void update()
        {
            Actor actor = _actors[curActor];

            ActionResult result = ActionResult.FetchedAction;
            Action action = actor.getNextAction(ref result);

            if (result == ActionResult.PlayerWait || result == ActionResult.Error)
                return;

            if (result == ActionResult.FetchedAction)
            {
                action.bindLevel(this);
                action.doAction();
            }

            curActor = (curActor + 1)%_actors.Count;
        }

        public void queueEffect(EffectDescription effect)
        {
            _effectQueue.AddFirst(effect);
        }

        public EffectDescription? getEffect()
        {
            if (_effectQueue.Count == 0)
            {
                return null;
            }

            EffectDescription effect = _effectQueue.Last.Value;
            _effectQueue.RemoveLast();

            return effect;
        }
        
        public Location getHeroLocation()
        {
            return _hero.location;
        }

        public List<Entity> getEntities(Location location)
        {
            //TODO add the other entites from the other layers to the list
            Entity[] entities =
            {
                getTile(location),
                getDoor(location),
                getItem(location),
                getActor(location)
            };

            List<Entity> result = new List<Entity>();
            foreach (Entity entity in entities)
            {
                if (entity != null)
                {
                    result.Add(entity);
                }
            }

            return result;
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

        public Door getDoor(Location location)
        {
            return _doors.ContainsKey(location) ? _doors[location] : null;
        }

        public Item getItem(Location location)
        {
            return _items.ContainsKey(location) ? _items[location] : null;
        }

        public void removeItem(Location location)
        {
            if (_items.ContainsKey(location))
            {
                _items.Remove(location);
            }
        }

        public Hero hero
        {
            get { return _hero; }
        }

        private void addDoor(Door door, Location location)
        {
            door.location = location;
            _doors.Add(location, door);
        }

        private void addItem(Item item, Location location)
        {
            item.location = location;
            _items.Add(item.location, item);
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
