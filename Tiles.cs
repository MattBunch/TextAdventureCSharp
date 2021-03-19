using System;
using System.Collections.Generic;
using System.Text;
using Items;

namespace TextAdventure
{
    class MapTile
    {
        int x;
        int y;

        public MapTile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public virtual string IntroText()
        {
            return "";
        }

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        // TODO: adjacent moves
        public List<Action> AdjacentMoves(World world)
        {
            List<Action> moves = new List<Action>();

            // if move available east
            if (world.TileExists(this.x + 1, this.y) != null)
                moves.Add(new MoveEast());

            // if move available west
            if (world.TileExists(this.x - 1, this.y) != null)
                moves.Add(new MoveWest());

            // if move available north
            if (world.TileExists(this.x, this.y - 1) != null)
                moves.Add(new MoveNorth());

            // if move available south
            if (world.TileExists(this.x, this.y + 1) != null)
            {
                moves.Add(new MoveSouth());
            }

            return moves;
        }

        // TODO: adjacent actions

        public List<Action> AvailableActions(World world)
        {
            List<Action> moves = this.AdjacentMoves(world);

            // add view inventory
            moves.Add(new ViewInventory());

            // add drink health potion
            moves.Add(new DrinkHealthPotion());

            return moves;
        }

        public virtual void ModifyPlayer(Player player)
        {

        }
    }



    class StartingRoom : MapTile
    {
        public StartingRoom(int x, int y) : base (x, y)
        {
            
        }

        public override string IntroText()
        {
            return "\nThis is the room you woke up in.";
        }

        public override void ModifyPlayer(Player player)
        {
            
        }
    }

    class LeaveCaveRoom : MapTile
    {
        static string intro = "\nYou see a bright light in front of you. Congratulations on escaping the cave!\n";
    public LeaveCaveRoom(int x, int y) : base(x, y)
        {

        }

        public string IntroText()
        {
            return "\nYou see a bright light in front of you. Congratulations on escaping the cave!\n";
        }

        public void ModifyPlayer(Player player)
        {
            player.victory = true;
        }
    }

    class LootRoom : MapTile
    {
        Item item;
        public int counter;

        public LootRoom(int x, int y, Item item) : base(x, y)
        {
            this.item = item;
            counter = 1;
        }

        public void AddLoot(Player player)
        {
            player.inventory.Add(item);
        }

        public override void ModifyPlayer(Player player)
        {
            if (this.counter == 1)
            {
                this.AddLoot(player);
                this.counter -= 1;
            }
        }

    }

    class EnemyRoom : MapTile
    {
        public Enemy enemy;

        public EnemyRoom(int x, int y, Enemy enemy) : base(x, y)
        {
            this.enemy = enemy;
        }

        public override void ModifyPlayer(Player player)
        {
            if (this.enemy.IsAlive())
            {
                player.hp -= this.enemy.damage;

                Console.WriteLine(this.enemy.name + " does " + this.enemy.damage + " damage. You have " + player.hp + " HP remaining.");
            if (player.hp <= 0)
                {
                    Console.WriteLine("{} has defeated you. You perish in the dungeon!\nGame over!", this.enemy.name);
                }
                
            }
        }

        public Action[] AvailableActions()
        {
            if (this.enemy.IsAlive())
            {
                //return [Action.Flee(), Action.Attack(this.enemy), Action.UseHealthPotion()];
            }
            else
            {
                //return this.AdjacentMoves();
            }
            return null;
        }
    }

    class EmptyRoom : MapTile
    {
        public EmptyRoom(int x, int y) : base(x, y)
        {

        }

        public override string IntroText()
        {
            return "\nUnremarkable room.\nYou must go on!\n";
        }

        public void ModifyPlayer()
        {

        }
    }

    // ENEMY ROOMS

    class GoblinRoom : EnemyRoom
    {
        public GoblinRoom(int x, int y) : base (x, y, new Goblin())
        {

        }

        public override string IntroText()
        {
            if (this.enemy.IsAlive())
            {
                return "\nA goblin creeps in front of you!\nIt has " + this.enemy.hp + " hp.";
            }
            else
            {
                return "\nThe corpse of a dead goblin rots on the ground.\n";
            }
        }
    }

    class OgreRoom : EnemyRoom
    {
        public OgreRoom(int x, int y) : base(x, y, new Ogre())
        {

        }

        public override string IntroText()
        {
            Ogre ogre = (Ogre) this.enemy;
            if (this.enemy.IsAlive() && ogre.getBerserk() == true)
            {
                return "\nA giant ogre runs out of the darkness in front of you in rage!\n It has " + this.enemy.hp + " hp.";
            }
            else if (this.enemy.IsAlive() && ogre.getBerserk() == false)
            {
                return "\nA giant ogre lumbers out of the darkness slowly in front of you!\n It has " + this.enemy.hp + " hp.";
            }
            else
            {
                return "\nThe corpse of a dead ogre rots on the ground.";
            }
        }
    }

    class DragonRoom : EnemyRoom
    {
        public DragonRoom(int x, int y) : base(x, y, new Dragon())
        {

        }

        public String IntroText()
        {
            if (this.enemy.IsAlive())
            {
                return "\nA giant dragon roars in front of you!\nIt has " + this.enemy.hp + " hp.\n";
            }
            else
            {
                return "\nThe body of a dead dragon lies before you.\n";
            }
        }
    }

    // ITEM ROOMS

    class FindDaggerRoom : LootRoom
    {
        public FindDaggerRoom(int x, int y) : base(x, y, new Dagger())
        {

        }
        
        public override string IntroText()
        {
            if (this.counter == 1)
            {
                return "\nYou notice something shiny in the corner.\n" +
                    "It's a dagger! You pick it up.\n";
            }
            else
            {
                return "\nThis is a room you found a dagger\n" +
                    "Its empty now";
            }
        }
    }

    class FindUltimateSwordRoom : LootRoom
    {
        public FindUltimateSwordRoom(int x, int y) : base(x, y, new UltimateSword())
        {

        }

        public override string IntroText()
        {

            if (this.counter == 1)
            {
                return "\nAn intimidating sword sticks of out a slab of stone in front of you. Its the ultimate sword!.\n" +
                    "You use all your might to pull it out. It feels heavy and weighty in your hands.\n"
                    + "You feel reassurred about your chances to escape";
            }
            else
            {
                return "\nThe room where you found the ultimate sword.\n" +
                    "The slab of stone is empty now.";
            }
        }
    }

    class FindGoldRoom : LootRoom
    {
        public FindGoldRoom(int x, int y) : base(x, y, new Gold(10))
        {

        }

        public override string IntroText()
        {

            if (this.counter == 1)
            {
                return "\nYou notice something shiny in the corner.\n"+
                    "Its a piece of gold with 10 emboldened on it! You pick it up.\n";
            }
            else
            {
                return "\nThis is a room where you found a piece of gold.\n" +
                    "Its empty now.\n";
            }
        }
    }

    class FindHealthPotionRoom : LootRoom
    {
        public FindHealthPotionRoom(int x, int y) : base(x, y, new HealthPotion())
        {

        }

        public override string IntroText()
        {

            if (this.counter == 1)
            {
                return "\nYou notice something red in the corner.\n" +
                    "Its a health potion! You pick it up.\n";
            }
            else
            {
                return "\nThis is a room where you found a health potion.\n" +
                    "Its empty now.\n";
            }
        }
    }

}
