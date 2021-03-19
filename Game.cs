using System;
using System.Collections.Generic;
using Items;

namespace TextAdventure
{
    class Game
    {

        string introtext =
        "TEXT ADVENTURE GAME\n" +
        "You awake in a darkly lit room. You have no memory of how you got there.\n" +
        "There are four dark and foreboding paths out of the room.";

        public Game()
        {
            World world = new World();
            Player player = new Player(world);
            MapTile mapTile = world.TileExists(player.locationX, player.locationY);

            // print intro text
            Console.WriteLine(introtext);
            // while game loop
            while (player.IsAlive() && !player.victory)
            {

                mapTile = world.TileExists(player.locationX, player.locationY);
                mapTile.ModifyPlayer(player);
                if (player.IsAlive() && !player.victory)
                {
                    if (player.victory)
                    {
                        break;
                    }
                    Console.WriteLine("Choose an action:\n");
                    List<Action> availableActions = mapTile.AvailableActions(world);
                    foreach (Action action in availableActions)
                    {
                        Console.WriteLine(action.ToString());
                    }
                    string actionInput = Console.ReadLine();
                    char actionInputChar = char.Parse(actionInput);
                    foreach (Action action in availableActions)
                    {
                        if (actionInputChar == action.getHotkey())
                        {
                            // TODO:
                            // if player is inside of enemy room, pass in room's enemy
                            if (mapTile.GetType() == typeof(EnemyRoom))
                            {
                                EnemyRoom enemyRoom = (EnemyRoom) mapTile;
                                player.DoAction(action, enemyRoom.enemy, availableActions);
                            }
                            // else pass null as not used
                            else {
                                player.DoAction(action, null, availableActions);
                            }
                            break;
                        }
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            new Game();
        }
    }
}
