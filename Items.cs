using System;
using System.Collections.Generic;
using System.Text;

namespace Items
{
    class Item
    {
        public string name;
        public string description;
        public int value;

        public Item(string name, string description, int value)
        {
            this.name = name;
            this.description = description;
            this.value = value;
        }

        public string ToString()
        {
            return this.name + "\n=====\n" + this.description +"\nValue: " + this.value + "\n";
        }

    }

    class Gold : Item
    {
        private int amount;

        public Gold(int amount) : base
            ("Gold", 
            "A round coin with " + amount + " stamped on the front.", 
            amount)
        {
            this.amount = amount;
        }
    }

    class HealthPotion : Item
    {
        
        public HealthPotion() : base
            ("Health Potion",
            "A red bubbly potion in a flask.\nWill refill 25hp.",
            20
            )
        {

        }
    }

    class Weapon : Item
    {
        public int damage;
        
        public Weapon(string name, string description, int value, int damage) 
            : base(name, description, value)
        {
            this.damage = damage;
        }

        public string ToString()
        {
            return this.name + "\n=====\n" + this.description + "\nValue: " + this.value + "\nDamage: " + this.damage;
        }
    }

    class Rock : Weapon
    {
        public Rock() : base
            ("Rock",
            "A fist-sized rock, suitable for bludgeoning.",
            0,
            5)
        {

        }
    }

    class Dagger : Weapon
    {
       
        public Dagger() : base(
            "Dagger",
            "Sharp dagger with an emblem, capable of stabbing enemies.",
            5,
            10)
        {

        }
    }

    class UltimateSword : Weapon
    {
        public UltimateSword() : base(
            "Ultimate Sword",
            "Very sharp sword which giving off a powerful aura, trembling in your hands.",
            100,
            25
            )
        {

        }
    }
}

