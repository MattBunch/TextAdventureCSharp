using System;
using System.Collections.Generic;
using System.Text;
using Items;

namespace TextAdventure
{
    class Player
    {
        public List<Item> inventory = new List<Item>();
        public int hp = 100;
        public int locationX;
        public int locationY;
        public bool victory = false;
        public World world;

        public Player(World world)
        {
            this.world = world;
            inventory.Add(new Gold(20));
            inventory.Add(new Rock());
            inventory.Add(new HealthPotion());

            int[] startingPositions = world.GetStartingPositions();
            this.locationX = startingPositions[0];
            this.locationY = startingPositions[1];
        }

        public bool IsAlive()
        {
            return this.hp > 0;
        }

        public void PrintInventory()
        {
            for (int i = 0; i < this.inventory.Count; i++)
            {
                Console.WriteLine(i.ToString() + "\n");
            }
        }
        
        public void Move(int dx, int dy)
        {
            this.locationX += dx;
            this.locationY += dy;
            MapTile tile = this.world.TileExists(locationX, locationY);
            string introtext = tile.IntroText();
            Console.WriteLine(introtext);
        }

        public void MoveNorth()
        {
            this.Move(0, -1);
        }

        public void MoveSouth()
        {
            this.Move(0, 1);
        }

        public void MoveEast()
        {
            this.Move(1, 0);
        }

        public void MoveWest()
        {
            this.Move(-1, 0);
        }

        public void Attack(Enemy enemy)
        {
            Weapon bestWeapon = null;
            foreach (Item i in inventory)
            {
                // if item is instanceof weapon
                if (i.GetType() == typeof(HealthPotion))
                {
                    Weapon tempWeapon = (Weapon) i;
                    if (tempWeapon.damage > bestWeapon.damage)
                    {
                        bestWeapon = (Weapon)i;
                    }
                }
            }

            Console.WriteLine("You use " + bestWeapon.name + " against " + enemy.name + "!");
            enemy.hp -= bestWeapon.damage;
            Console.WriteLine("You did " + bestWeapon.damage + " to " + enemy.name);

            if (!enemy.IsAlive()){
                Console.WriteLine("You killed " + enemy.name + "!");
            }
            else
            {
                Console.WriteLine(enemy.name + " hp is " + enemy.hp);
            }
        }

        public void UseHealthPotion()
        {
            bool hasHealthPotion = false;
            foreach (Item i in inventory)
            {
                // If Inventory contains HealthPotion
                if (i.GetType() == typeof(HealthPotion))
                {
                    hasHealthPotion = true;
                }
            }

            if (hasHealthPotion)
            {
                this.hp += 25;
                if (this.hp > 100)
                    this.hp = 100;

                foreach(Item i in inventory)
                {
                    if (i.name.Equals("Health Potion"))
                        inventory.Remove(i);
                }
                Console.WriteLine("25 HP restored. Current HP: " + this.hp + "\nHealth potion removed from inventory");
            }
            else
            {
                Console.WriteLine("No health potion found in inventory.");
            }
        }

        public void Flee()
        {
            // TODO: flee

            // find current map tile
            MapTile currentTile = null;
            foreach (MapTile tile in this.world.Get_world().Values)
            {
                // if tile x and y match player x and y, this is your tile
                if (tile.getX() == this.locationX && tile.getY() == this.locationY)
                {
                    currentTile = tile;
                    break;
                }
            }

            // create list of available moves
            List<Action> moves = currentTile.AdjacentMoves(this.world);
            // select random move from list
            Random random = new Random();

            int num = random.Next(moves.Count);

            Action move = moves[num];

            // do that action
            this.DoAction(move, null, moves);
        }



        public void DoAction(Action action, Enemy enemy, params Object[] objects)
        {
            // get attribute of name
            char actionController = action.getHotkey();

            // do action
            if (actionController.Equals('n'))
            {
                this.MoveNorth();
            }
            else if (actionController.Equals('s'))
            {
                this.MoveSouth();
            }
            else if (actionController.Equals('e'))
            {
                this.MoveEast();
            }
            else if (actionController.Equals('w'))
            {
                this.MoveWest();
            }
            else if (actionController.Equals('i'))
            {
                this.PrintInventory();
            }
            else if (actionController.Equals('h'))
            {
                this.UseHealthPotion();
            }
            else if (actionController.Equals('a'))
            {
                this.Attack(enemy);
            }
            else if (actionController.Equals('f'))
            {
                this.Flee();
            }
        }

    }

    
}
