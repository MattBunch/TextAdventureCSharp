using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure
{
    class Enemy
    {
        public string name;
        public int hp;
        public int damage;

        public Enemy(string name, int hp, int damage)
        {
            this.name = name;
            this.hp = hp;
            this.damage = damage;
        }

        public bool IsAlive()
        {
            return this.hp > 0;
        }

        override
        public string ToString()
        {
            return this.name + "\n =====\n" + this.name + "\nhp: " + this.hp + "\nDamage: " + this.damage;
        }
    }

    class Goblin : Enemy
    {
        public Goblin() : base(
            "Goblin",
            10,
            2)
        {

        }
    }
    class Ogre : Enemy
    {
        bool berserk;

        public Ogre() : base(
            "Ogre",
            30,
            15)
        {
            berserk = false;

        }

        void goBerserk()
        {
            this.berserk = true;
            this.damage = 25;
        }

        void goNormal()
        {
            this.berserk = false;
            this.damage = 15;
        }

        public bool getBerserk()
        {
            return this.berserk;
        }

        public void OgreBerserkController()
        {
            Random random = new Random();
            double n = random.NextDouble();

            if (this.berserk)
            {
                if (n > 0.5)
                {
                    this.goBerserk();
                    Console.WriteLine("The ogre has gone berserk!");
                }
            }
            else
            {
                if (n > 0.5)
                {
                    this.goNormal();
                    Console.WriteLine("The ogre goes to a normal state.");
                }
            }
        }

    }

    class Dragon : Enemy
    {
        public Dragon() : base(
            "Dragon",
            75,
            30)
        {

        }

        public void breathFire()
        {
            this.damage *= 2;
        }
    }


}
