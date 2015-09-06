using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Dictionary<Location, Entity> _walkables;
        private Hero _hero;

        private HashSet<Location> _actorLocations; 
        private Location _entrance;
        private Location _exit;
        private Rect _bounds;

        public bool gameOver;

        private int curActor;
        
        private LinkedList<EffectDescription> _effectQueue;

        public Level(Hero hero)
        {
            _tiles = new Dictionary<Location, Tile>();
            _actors = new List<Actor>();
            _items = new Dictionary<Location, Item>();
            _walkables = new Dictionary<Location, Entity>();
            _effectQueue = new LinkedList<EffectDescription>();

            _actorLocations = new HashSet<Location>();

            curActor = 0;

            //TEST CODE
            _hero = hero;
            
            //END TEST CODE
        }

        public void setHeroInLevel()
        {
            addActor(_hero, entrance);
            updatePlayerVision();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns false if the player did not make a move</returns>
        public bool update()
        {
            Actor actor = _actors[curActor];

            if (actor.hp <= 0)
            {
                if (actor is Hero)
                {
                    gameOver = true;
                }
                else
                {
                    //actor is dead, and should be removed
                    _actors.Remove(actor);
                    return false;
                }
            }

            ActionResult result = ActionResult.FetchedAction;
            Action action = actor.getNextAction(ref result);

            if (result == ActionResult.Error || result == ActionResult.Wait)
            {
                curActor = (curActor + 1)%_actors.Count;
                return false;
            }

            if (result == ActionResult.FetchedAction)
            {
                action.bindLevel(this);


                bool success = action.doAction();

                while (!success)
                {
                    action = action.alternate;
                    if (action == null)
                    {
                        return false;
                    }
                    action.bindActor(actor);
                    action.bindLevel(this);
                    success = action.doAction();
                }

                curActor = (curActor + 1) % _actors.Count;

            }


            if (hero.justMoved)
            {
                hero.justMoved = false;
                updatePlayerVision();
            }

            return result == ActionResult.PlayerWait;
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
                getWalkable(location),
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

        public Entity getWalkable(Location location)
        {
            return _walkables.ContainsKey(location) ? _walkables[location] : null;
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

        public bool blocksSight(Location location)
        {
            Tile tile = getTile(location);
            Entity walkable = getWalkable(location);

            if (tile != null && tile.blocksMovement)
                return true;
            if (walkable != null)
            {
                if (walkable is Door && ((Door) walkable).closed)
                    return true;
                if (walkable is Chest)
                    return true;
            }
                

            return false;
        }

        private void resetLit()
        {
            for (int x = hero.visibleRect.x1; x <= hero.visibleRect.x2; x++)
            {
                for (int y = hero.visibleRect.y1; y <= hero.visibleRect.y2; y++)
                {
                    if (getLit(new Location(x, y)) == Entity.LIT_FULL_DARK)
                    {
                        continue;
                    }
                    setLit(new Location(x, y), 126);
                }
            }
        }

        public void setLit(Location location, int lit)
        {
            List<Entity> entities = getEntities(location);
            foreach (Entity entity in entities)
            {
                entity.lit = lit;
            }
        }

        private int getLit(Location location)
        {
            Tile tile = getTile(location);
            if (tile == null) return -1;

            return tile.lit;
        }

        public void updatePlayerVision()
        {
            resetLit();
            FOVCalculator.calculateFOV(this);
        }

        public void addWalkable(Entity entity, Location location)
        {
            entity.location = location;
            if (_walkables.ContainsKey(location))
            {
                _walkables.Remove(location);
            }
            _walkables.Add(location, entity);
        }

        public void addItem(Item item, Location location)
        {
            item.location = location;
            _items.Add(item.location, item);
        }

        public void addTile(Tile tile, Location location)
        {
            tile.location = location;
            if (_tiles.ContainsKey(location))
            {
                _tiles.Remove(location);
            }
            _tiles.Add(tile.location, tile);
        }

        public void addActor(Actor actor, Location location)
        {
            actor.location = location;
            _actors.Add( actor);
        }

        public Location entrance
        {
            get { return _entrance; }
            set { _entrance = value; }
        }

        public Location exit
        {
            get { return _exit; }
            set { _exit = value; }
        }

        public Rect bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }
    }
}
