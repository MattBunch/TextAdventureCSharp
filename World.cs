using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TextAdventure
{
    class World
    {

        int[] StartingPosition = new int[2];
        Dictionary<int[], MapTile> _world;

        public World()
        {
            _world = new Dictionary<int[], MapTile>();
            LoadTiles();
        }

        public Dictionary<int[], MapTile> Get_world()
        {
            return this._world;
        }

        public void LoadTiles()
        {
            string[,] map = { {null, null, null, null, "LeaveCaveRoom" },
                {null, null, "FindGoldRoom", "EmptyRoom", "DragonRoom" },
                {null, null, "EmptyRoom", null, "FindGoldRoom" },
                {null, null, "EmptyRoom", null, null },
                {"GoblinRoom", "EmptyRoom", "StartingRoom","FindDaggerRoom", null },
                {null, null, "EmptyRoom", null, null },
                {null, null, "OgreRoom", "FindHealthPotionRoom", null },
                {null, null, "FindUltimateSwordRoom", null, null },
            };
            //string PathFile = "C:\\Users\\bunchmattsource\\repos\\TextAdventure\\TextAdventure\\resources\\map.txt";
            //StreamReader streamReader = new StreamReader(PathFile);
            // open resource file
            //int counter = NumOfLines(PathFile);
            //string[] rows = new string[counter];

            //for (int i = 0; i < counter; i++)
            //{
            //    rows[i] = streamReader.ReadLine();
            //}

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    string TileName;
                    if (map[x, y] != null)
                    {
                        TileName= map[x, y].Replace("\n", "");
                    }
                    else
                    {
                        TileName = null;
                    }

                    if (TileName != null)
                    {
                        if (TileName.Equals("StartingRoom"))
                        {
                            StartingPosition[0] = x;
                            StartingPosition[1] = y;
                        }
                    }

                    int[] position = { x, y };
                    //Console.WriteLine(TileName + " position: x:" + position[0] + ", y:" + position[1]);


                    if (TileName == null)
                    {
                        _world.Add(position, null);
                    }
                    else if (TileName.Equals("StartingRoom"))
                    {
                        _world.Add(position, new StartingRoom(x, y));
                    }
                    else if (TileName.Equals("LeaveCaveRoom"))
                    {
                        _world.Add(position, new LeaveCaveRoom(x, y));
                    }
                    else if (TileName.Equals("EmptyRoom"))
                    {
                        _world.Add(position, new EmptyRoom(x, y));
                    }
                    else if (TileName.Equals("GoblinRoom"))
                    {
                        _world.Add(position, new GoblinRoom(x, y));
                    }
                    else if (TileName.Equals("OgreRoom"))
                    {
                        _world.Add(position, new OgreRoom(x, y));
                    }
                    else if (TileName.Equals("DragonRoom"))
                    {
                        _world.Add(position, new DragonRoom(x, y));
                    }
                    else if (TileName.Equals("FindDaggerRoom"))
                    {
                        _world.Add(position, new FindDaggerRoom(x, y));
                    }
                    else if (TileName.Equals("FindGoldRoom"))
                    {
                        _world.Add(position, new FindGoldRoom(x, y));
                    }
                    else if (TileName.Equals("FindHealthPotionRoom"))
                    {
                        _world.Add(position, new FindHealthPotionRoom(x, y));
                    }
                    else if (TileName.Equals("FindUltimateSwordRoom"))
                    {
                        _world.Add(position, new FindUltimateSwordRoom(x, y));
                    }

                    // debugging
                    //Console.WriteLine("x: " + x);
                    //Console.WriteLine("y: " + y);
                }
            }
            //Print_World();
        }



        private int NumOfLines(string input)
        {
            using (StreamReader r = new StreamReader(input))
            {
                int i = 0;
                while (r.ReadLine() != null) { i++; }
                return i;
            }
        }

        public MapTile TileExists(int x, int y)
        {
            // FIXME: return the tile position of x and y.
            MapTile output = null;
            foreach(KeyValuePair<int[], MapTile> entry in _world)
            {
                if (entry.Key[0] == x && entry.Key[1] == y)
                {
                    output = entry.Value;
                    break;
                }
            }
            return output;
        }

        public void Print_World()
        {
            string output = "";
            foreach (KeyValuePair<int[], MapTile> entry in _world)
            {
                output += string.Format("Key = {0}, Value = {1}\n", entry.Key, entry.Value);
            }
            Console.WriteLine(output);
        }

        public int[] GetStartingPositions()
        {
            return this.StartingPosition;
        }


    }

}
